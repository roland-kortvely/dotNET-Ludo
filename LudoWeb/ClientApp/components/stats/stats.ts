import Vue from 'vue';
import {Component} from 'vue-property-decorator';

interface Score {
    id: number,
    name: string;
    points: number;
}

interface Rating {
    id: number,
    stars: number;
    content: string;
}

interface Comment {
    id: number,
    name: string;
    content: string;
}

@Component
export default class DataComponent extends Vue {

    scores: Score[] = [];
    ratings: Rating[] = [];
    comments: Comment[] = [];

    mounted() {
        fetch('api/score')
            .then(response => response.json() as Promise<Score[]>)
            .then(data => {
                this.scores = data;
            });
        
        fetch('api/rating')
            .then(response => response.json() as Promise<Rating[]>)
            .then(data => {
                this.ratings = data;
            });
        
        fetch('api/comment')
            .then(response => response.json() as Promise<Comment[]>)
            .then(data => {
                this.comments = data;
            });
    }
}
