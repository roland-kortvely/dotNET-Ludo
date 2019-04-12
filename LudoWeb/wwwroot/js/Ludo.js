let Common = {
    newComponent(componentName) {
        return document.querySelector('.components').querySelector('.' + componentName).cloneNode(true)
    },
    getAttribute(node, attributeName) {
        if (typeof node.getAttribute === 'function') {
            return node.getAttribute('data-' + attributeName)
        }
        console.error('No getAttribute function exist for', node);
        return node
    },
    setAttribute(node, attributeName, attributeValue) {
        if (typeof node.setAttribute === 'function') {
            return node.setAttribute('data-' + attributeName, attributeValue)
        }
        console.error('No setAttribute function exist for', node);
        return node
    },
    setCSS(node, properties) {
        const pxProperties = ['left', 'top', 'right', 'bottom', 'width', 'height'];
        for (let key in properties) {
            if (pxProperties.indexOf(key) > -1) {
                if (typeof properties[key] === 'number') {
                    properties[key] += 'px'
                } else if (typeof properties[key] === 'string') {
                    if (properties[key].indexOf('px') === -1) {
                        properties[key] += 'px'
                    }
                }
            }
        }
        Object.assign(node.style, properties)
    }
};

let Ludo = {
    context: document.querySelector('.game'),
    game: {
        radius: 480,
        boardStyle: 'default',
        gameMode: '1-1',
        playersCount: 0,
        board: '',
        pieces: [],
        pieceTemplates: [],
        piecesCorner: [],
        players: [],
        dice: {
            selector: "",
            value: 6
        },
        diceLock: true,
        defaultPositions: {
            slotCenter: [
                {x: 2.5, y: 11.5},
                {x: 11.5, y: 2.5},
                {x: 11.5, y: 11.5},
                {x: 2.5, y: 2.5}
            ],
            slotCenterOffset: [
                [-1, -1],
                [-1, 1],
                [1, -1],
                [1, 1]
            ],
            firstCell: [
                {x: 6, y: 13},
                {x: 8, y: 1},
                {x: 13, y: 8},
                {x: 1, y: 6}
            ],
            diceCenter: [],
            slotBackground: [
                {x: 0, y: 9},
                {x: 9, y: 0},
                {x: 9, y: 9},
                {x: 0, y: 0}
            ]
        },
        turns: [],
        onField: []
    },

    init() {
        this.unit = this.game.radius / 15;
        this.diceRadius = this.unit * 1.5;
        this.game.defaultPositions.diceCenter = (this.unit * 7.5) - (this.diceRadius / 2);

        this.resetBoard();
        this.setupDice();
    },

    resetBoard() {
        this.context.innerHTML = '';
        this.game.board = this.context.appendChild(Common.newComponent(this.game.boardStyle));

        Array.from(this.game.board.querySelector('.pieces').children).forEach((piece) => {
            this.game.pieceTemplates.push(piece.cloneNode(false));
        });

        this.game.board.removeChild(this.game.board.querySelector('.pieces'));
        this.overlay = this.game.board.querySelector('.overlay');
        this.background = this.game.board.querySelector('.background');
        this.background.style.width = this.overlay.style.width =
            this.background.style.height = this.overlay.style.height = this.game.radius + 'px';

        let humanCount = Number(this.game.gameMode[0]), cpuCount = Number(this.game.gameMode[2]);

        this.game.playersCount = humanCount + cpuCount;
        console.assert(1 <= this.game.playersCount && this.game.playersCount <= 4);

        for (let p = 0; p < humanCount + cpuCount; p++) {
            this.game.players.push({isCpu: p < humanCount})
        }

        for (let playerIndex = 0; playerIndex < this.game.playersCount; playerIndex++) {
            for (let pieceIndex = 0; pieceIndex < 4; pieceIndex++) {
                let x = this.game.defaultPositions.slotCenter[playerIndex].x,
                    y = this.game.defaultPositions.slotCenter[playerIndex].y,
                    pieceOffsetX = this.game.defaultPositions.slotCenterOffset[pieceIndex][0],
                    pieceOffsetY = this.game.defaultPositions.slotCenterOffset[pieceIndex][1],

                    piece = new Piece({
                        playerIndex: playerIndex,
                        pieceIndex: pieceIndex,
                        location: {
                            x: x - pieceOffsetX,
                            y: y - pieceOffsetY
                        },
                        firstStep: this.game.defaultPositions.firstCell[playerIndex],
                        unit: this.unit,
                        shrinked: false,
                        view: this.game.pieceTemplates[playerIndex].cloneNode(false)
                    });

                //TODO:: API
                piece.view.addEventListener('click', () => piece.step(this.game.dice.value));

                this.overlay.appendChild(piece.view);
                this.game.pieces.push(piece);
            }
            this.game.onField.push(false);
        }

        this.game.block = this.context.querySelector('.block');

        Common.setCSS(this.game.block, {
            position: 'absolute',
            width: this.unit * 6,
            height: this.unit * 6,
            opacity: 0
        });

        anime({
            targets: this.game.block,
            opacity: 0.3,
            direction: 'alternate',
            loop: -1,
            elasticity: 500,
            duration: 500
        });
    },

    setupDice() {
        this.game.dice.selector = this.overlay.appendChild(this.game.board.querySelector('.dice'));

        Common.setCSS(this.game.dice.selector, {
            position: 'absolute',
            height: this.diceRadius,
            width: this.diceRadius,
            left: this.game.defaultPositions.diceCenter,
            top: this.game.defaultPositions.diceCenter,
        });

        this.game.dice.selector.setAttribute('src', Common.getAttribute(this.game.dice.selector, 'face-6-src'));
        this.game.dice.selector.addEventListener('click', this.rollDice.bind(this, this.game, null));
    },

    random6() {
        //TODO:: API
        return 1 + Math.round(Math.random() * 5)
    },

    rollDice(game, roll) {

        //TODO:: API
        roll = roll || this.random6();

        game.dice.value = roll;

        Common.setCSS(this.game.dice.selector, {
            left: this.game.defaultPositions.diceCenter,
            top: this.game.defaultPositions.diceCenter
        });

        let diceAnim = anime.timeline();

        let signA = this.random6() % 2 === 0 ? '-' : '+';
        let signB = this.random6() % 2 === 0 ? '-' : '+';

        for (let keyFrames = 0; keyFrames < 7; keyFrames++) {
            diceAnim.add({
                targets: this.game.dice.selector,
                rotate: "+=" + this.random6() * 10,
                duration: 10 + keyFrames * 10,
                complete: keyFrames < 6 ? changeFace : endFace,
                scale: 0.4 + (keyFrames * 0.1),
                left: signA + '=' + (Math.random() * 5),
                top: signB + '=' + (Math.random() * 5),
            })
        }

        function changeFace(anim) {
            let faceAttr = 'face-' + (1 + Math.round(Math.random() * 5)) + '-src';
            let target = anim.animatables[0].target;
            target.setAttribute('src', Common.getAttribute(target, faceAttr));
        }

        function endFace(anim) {
            let faceAttr = 'face-' + roll + '-src';
            let target = anim.animatables[0].target;
            target.setAttribute('src', Common.getAttribute(target, faceAttr));
        }
    }
};

class Piece {
    constructor(profile) {

        Object.assign(this, profile);

        this.size = {
            normal: Common.getAttribute(this.view, 'src'),
            shrinked: Common.getAttribute(this.view, 'shrinked-src')
        };

        Common.setCSS(this.view, {
            position: 'absolute',
            left: this.location.x * this.unit,
            top: this.location.y * this.unit,
            height: this.unit,
            width: this.unit
        });

        this.defaultLocation = profile.location;
        this.firstStep = profile.firstStep;
        this.reflectView();
        this.createPath();
    }

    createPath() {
        this.path = [this.defaultLocation, this.firstStep];
        this.pathPointer = 0;
        let stepLoopArray = [4, 1, 5, 2, 5, 1, 5, 2, 5, 1, 5, 2, 5, 1, 5, 1, 6];
        let xSteps = [0, -1, -1, 0, 1, 1, 0, 1, 0, 1, 1, 0, -1, -1, 0, -1, 0, 0];
        let ySteps = [-1, -1, 0, -1, 0, -1, -1, 0, 1, 1, 0, 1, 0, 1, 1, 0, -1];
        switch (this.playerIndex) {
            case 1:
                xSteps = xSteps.map((x) => (x * -1));
                ySteps = ySteps.map((y) => (y * -1));
                break;
            case 2:
                [xSteps, ySteps] = [ySteps, xSteps.map((x) => (x * -1))];
                break;
            case 3:
                [xSteps, ySteps] = [ySteps.map((y) => (y * -1)), xSteps]
                break;
        }
        for (let i = 0; i < stepLoopArray.length; i++) {
            for (let j = 0; j < stepLoopArray[i]; j++) {
                this.path.push({
                    x: this.previousStep.x + xSteps[i],
                    y: this.previousStep.y + ySteps[i]
                })
            }
        }
    }

    get previousStep() {
        return this.path[this.path.length - 1];
    }

    walkTo(from, to) {
        if (from < to) {
            to = to > 57 ? 57 : to
            from = from < 0 ? 0 : from
        } else {
            from = from > 57 ? 57 : from
            to = to < 0 ? 0 : to
        }

        let steps = to - from;
        let walkTimeline = anime.timeline();

        if (steps > 0) {
            for (let y = from + 1; y <= to; y++) {
                walkTimeline.add({
                    targets: this.view,
                    left: this.path[y].x * this.unit,
                    top: this.path[y].y * this.unit,
                    duration: 100,
                    easing: 'linear'
                })
            }
        } else {
            for (let y = from - 1; y >= to; y--) {
                walkTimeline.add({
                    targets: this.view,
                    left: this.path[y].x * this.unit,
                    top: this.path[y].y * this.unit,
                    duration: 50,
                    easing: 'linear'
                })
            }
        }
    }

    step(n) {
        this.walkTo(this.pathPointer, this.pathPointer + n);
        this.pathPointer += n;
        if (this.pathPointer < 0) {
            this.pathPointer = 0;
        }
        if (this.pathPointer > 57) {
            this.pathPointer = 57;
        }
    }

    reflectView() {
        this.view.setAttribute('src', this.shrinked ? this.size.shrinked : this.size.normal);
    }

    shrink() {
        this.shrinked = true;
        this.reflectView();
    }

    normal() {
        this.shrinked = false;
        this.reflectView();
    }
}
