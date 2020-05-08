<template>
    <b-jumbotron>
        <h2>Robots</h2>
        <div>
            <b-list-group horizontal="md">
                <b-list-group-item to="robots/add">
                    <b-icon-plus/> Add new robot</b-list-group-item>
                <b-list-group-item to="delete">
                    <b-icon-pencil/> Update robot</b-list-group-item>
                <b-list-group-item to="edit">Modified</b-list-group-item>
            </b-list-group>
        </div>
        <div class="text-center mt-5" v-if="robots.status.isLoading">
            <b-spinner variant="primary" style="width: 3rem; height: 3rem;" label="Large Busy"></b-spinner>
        </div>
        <p style="display: none">{{robots}}</p>

        <b-row align-v="center" v-if="!robots.status.isLoading" class="mt-1">
            <b-col md="4" v-for="robot in robots.all.items" :key="robot.id">
                <b-card :img-src="loadPlaceholder(robot.hostname)" img-alt="Image" img-top class="mt-3">
                    <b-card-text>
                        <b>Hostname: </b> {{robot.hostname}}<br>
                        <b>Ip-address: </b>{{robot.basePath}}
                    </b-card-text>
                    <template v-slot:footer>
                        <small class="text-muted">Last online 2 days ago</small>
                        <b-badge href="#" variant="success" class="float-right" v-if="robot.isOnline">Online</b-badge>
                        <b-badge href="#" variant="danger" class="float-right" v-if="!robot.isOnline">Offline</b-badge>
                    </template>
                </b-card>
            </b-col>
        </b-row>
    </b-jumbotron>
</template>

<script>
    import { mapState, mapActions } from 'vuex'
    export default {
        name: "Robots",
        created() {
            this.getAll()
        },
        methods: {
            ...mapActions('robots', {
                getAll: 'getAll'
            }),
            loadPlaceholder(text){
                return 'https://dummyimage.com/418x150/000/518c8b?text='+text
            }
        },
        computed: {
            ...mapState({
               robots: state => state.robots
            })
        }
    }
</script>