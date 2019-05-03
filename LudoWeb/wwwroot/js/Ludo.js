let Common = {
    newComponent(componentName)
    {
        return document.querySelector('.components').querySelector('.' + componentName).cloneNode(true)
    },
    getAttribute(node, attributeName)
    {
        if (typeof node.getAttribute === 'function') {
            return node.getAttribute('data-' + attributeName)
        }
        console.error('No getAttribute function exist for', node);
        return node
    },
    setAttribute(node, attributeName, attributeValue)
    {
        if (typeof node.setAttribute === 'function') {
            return node.setAttribute('data-' + attributeName, attributeValue)
        }
        console.error('No setAttribute function exist for', node);
        return node
    },
    setCSS(node, properties)
    {
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

//TODO:: API
async function api(url, data = null, prefix = "game/")
{
    let $data = JSON.parse(JSON.stringify(data));

    let $result;
    if (data === null) {
        $result = await axios.get("/api/" + prefix + url);
    } else {
        $result = await axios.post("/api/" + prefix + url, $data);
    }

    if ($result.data.message !== null && $result.data.message.length > 0) {
        status($result.data.message);
    }

    if ($result.data.state !== true) {
        throw Error($result.data.message);
    }

    return $result.data.data;
}

function status(msg)
{
    let status = document.querySelector("#status");
    status.innerHTML = msg;
}

function diceShake(roll)
{
    Ludo.diceShake(roll);
}

function setPlayer(id)
{
    // console.log("Current player: " + id);
    Ludo.game.sessionPlayer = id;
}

function setRoom(id)
{
    Ludo.game.sessionRoom = id;
}

let Ludo = {
    context: document.querySelector('.game'),
    game: {
        data: {
            currentPlayer: 0,
            dice: 6,
            players: []
        },
        positions: [],
        sessionPlayer: 0,
        sessionRoom: 0,
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

    init()
    {
        document.querySelector("#new_game").addEventListener('click', () => {
            api("init").then((r) => {
                this.reset();
            }).catch((e) => {
                console.log(e);
                status("Unable to start a new game.");
            });
        });

        setPlayer(0);
        setRoom(1);

        this.reset();

        setInterval(() => this.sync(), 500);

        status("Ready to play.");
    },

    reset()
    {
        this.unit = this.game.radius / 15;
        this.diceRadius = this.unit * 1.5;
        this.game.defaultPositions.diceCenter = (this.unit * 7.5) - (this.diceRadius / 2);

        this.resetBoard();
        this.setupDice();
    },

    sync()
    {
        api("sync").then((r) => {

            Object.assign(this.game.data, r.game);
            Object.assign(this.game.positions, r.sync);

            let $dice = this.game.data.dice;
            if ($dice !== this.game.dice.value) {
                diceShake($dice);
                this.game.dice.value = $dice;
            }

            let $player = this.game.data.players[this.game.sessionPlayer];
            status($player.status);

            let $figures = this.game.positions;
            if ($figures.length) {

                for (let i = 0; i < Ludo.game.pieces.length; i++) {
                    let $figure = Ludo.game.pieces[i]; 
                    for (let j = 0; j < $figures.length; j++) {
                        let $syncFigure = $figures[j];  

                        if ($figure.playerIndex !== $syncFigure.player) {
                            continue;
                        }

                        if ($figure.pieceIndex !== $syncFigure.index) {
                            continue;
                        }

                        let $diff = $syncFigure.position - $figure.pathPointer;
                        if ($diff !== 0) {
                            if ($diff > 0) {
                                $figure.step($diff);
                            } else {
                                $figure.walkTo($figure.pathPointer, $syncFigure.position);
                                $figure.pathPointer = $syncFigure.position;
                            }
                        }
                    }
                }
            }
        }).catch((e) => {
            console.log(e);
            status("Unable to sync a game.");
        });
    },

    resetBoard()
    {
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

                piece.view.addEventListener('click', () => {
                    piece.move(this.game);
                });

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

    setupDice()
    {
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

    random6()
    {
        return 1 + Math.round(Math.random() * 5)
    },

    rollDice(game, roll)
    {
        api("roll", {currentPlayer: this.game.sessionPlayer}).then((r) => {
            roll = r.dice;
        }).catch((e) => {
            console.log(e);
        });
    },

    diceShake(roll)
    {
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

        function changeFace(anim)
        {
            let faceAttr = 'face-' + (1 + Math.round(Math.random() * 5)) + '-src';
            let target = anim.animatables[0].target;
            target.setAttribute('src', Common.getAttribute(target, faceAttr));
        }

        function endFace(anim)
        {
            let faceAttr = 'face-' + roll + '-src';
            let target = anim.animatables[0].target;
            target.setAttribute('src', Common.getAttribute(target, faceAttr));
        }
    }
};

class Piece {

    constructor(profile)
    {

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

    createPath()
    {
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
                [xSteps, ySteps] = [ySteps.map((y) => (y * -1)), xSteps];
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

    get previousStep()
    {
        return this.path[this.path.length - 1];
    }

    walkTo(from, to)
    {
        if (from < to) {
            to = to > 57 ? 57 : to;
            from = from < 0 ? 0 : from
        } else {
            from = from > 57 ? 57 : from;
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

    step(n)
    {
        this.walkTo(this.pathPointer, this.pathPointer + n);
        this.pathPointer += n;
        if (this.pathPointer < 0) {
            this.pathPointer = 0;
        }
        if (this.pathPointer > 57) {
            this.pathPointer = 57;
        }
    }

    reflectView()
    {
        this.view.setAttribute('src', this.shrinked ? this.size.shrinked : this.size.normal);
    }

    shrink()
    {
        this.shrinked = true;
        this.reflectView();
    }

    normal()
    {
        this.shrinked = false;
        this.reflectView();
    }

    move(game)
    {
        api("move", {figure: this, currentPlayer: Ludo.game.sessionPlayer}).then((r) => {
            this.step(this.pathPointer === 0 ? 1 : game.dice.value);
            // console.log(this.pieceIndex);
        }).catch((e) => {
            console.log(e);
        });
    }

    kick()
    {
        this.walkTo(this.pathPointer, 0);
        this.pathPointer = 0;
    }
}
