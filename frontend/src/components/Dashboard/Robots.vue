<template>
    <b-jumbotron class="mt-4">
        <h2>Robots
            <b-button to="robots/add" class="float-right ml-2" variant="primary">
                <b-icon-plug/>
                Add new robot
            </b-button>
            <b-button to="missions" class="float-right ml-2">
                <b-icon-clipboard-data/>
                Robots missions
            </b-button>
        </h2>
        <div class="text-center mt-5" v-if="robots.status.isLoading">
            <b-spinner variant="primary" label="Large Busy"></b-spinner>
        </div>
        <p style="display: none">{{robots}}</p>

        <b-row align-v="center" v-if="!robots.status.isLoading" class="mt-1">
            <b-col md="4" v-for="robot in robots.all.items" :key="robot.id">
                <b-card :img-src="loadPlaceholder(robot.hostname)" img-alt="Image" img-top class="mt-3">
                    <div class="float-right">
                        <b-badge variant="success" class="float-right" v-if="robot.isOnline">Online</b-badge>
                        <b-badge variant="danger" class="float-right" v-if="!robot.isOnline">Offline</b-badge>
                    </div>
                    <b-card-text>
                        <b :id="`Robotname-${robot.hostname}`">Hostname: </b><br> {{robot.hostname}}<br><br>
                        <b :id="`Robotaddress-${robot.hostname}`">IP-address:</b> <br>{{robot.basePath}}
                    </b-card-text>
                    <template v-slot:footer>
                        <!-- just use onClick here -->
                        <b-button class="btn-warning w-100">Remove robot</b-button>
                    </template>
                </b-card>
            </b-col>
        </b-row>
    </b-jumbotron>
</template>

<script>
    import {mapActions, mapState} from 'vuex'

    export default {
        name: "Robots",
        data() {
            return {
                polling: null
            }
        },
        created() {
            this.getAll()
            this.pollData()
        },
        methods: {
            ...mapActions('robots', {
                getAll: 'getAll'
            }),
            loadPlaceholder(text){
                return 'https://dummyimage.com/418x150/000/518c8b?text='+text
            },
            pollData () {
                this.polling = setInterval(() => {
                    this.$store.dispatch('robots/getAll')
                }, 1000)
            }
        },
        computed: {
            ...mapState({
               robots: state => state.robots
            })
        },
        beforeDestroy() {
            clearInterval(this.polling)
        }
    }
</script>
<style scoped>
    .badge{
        font-size: 14px;
        width: 95px;
    }
    .btn-warning{
        color: #ffffff;
        background-color: #c3beb8;
        border-color: #bab39e;
    }
</style>