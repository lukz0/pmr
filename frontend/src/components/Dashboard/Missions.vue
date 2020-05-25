<template>
    <b-jumbotron class="mt-4">
            <b-row>
                <b-col>
                    <b-dropdown id="dropdown-1" text="Select robot" class="m-md-2">
                        <b-dropdown-item v-for="robot in robots.all.items" :key="robot.id" @click="select(robot)">{{robot.hostname}}</b-dropdown-item>
                    </b-dropdown>
                </b-col>
            </b-row>
            <b-row>
                <b-col cols="8">
                    <h2>Missions</h2>
                    <div v-if="currentRobot">
                        <b-list-group v-for="m in missions.all.missionsList" :key="m.id">
                            <b-list-group-item button v-if="m.robotId === currentRobot.id">{{m.name}}</b-list-group-item>
                        </b-list-group>
                    </div>
                </b-col>
                <b-col cols="4">
                    <h2>Queue</h2>
                    <div>
                        <b-list-group v-for="m in missions.all.queueList" :key="m.id">
                            <b-list-group-item button v-if="m.robotId === currentRobot.id">{{m.name}}</b-list-group-item>
                        </b-list-group>
                    </div>
                </b-col>
            </b-row>
    </b-jumbotron>
</template>

<script>
    import {mapState, mapActions} from 'vuex'
    export default {
        name: "Missions",
        data(){
            return {
                currentRobot: null
            }
        },
        created() {
            this.getAllMissions();
            this.getAllRobots();
        },
        methods: {
            ...mapActions('missions', {
                getAllMissions: 'getAll'
            }),
            ...mapActions('robots', {
                getAllRobots: 'getAll'
            }),
            select(robot){
                this.currentRobot = robot;
                console.log(this.missions.all.missionsList)
                console.log(this.missions.all.queueList)
                //console.log(this.currentRobot);
            }
        },
        computed: {
            ...mapState({
                missions: state => state.missions,
                robots: state => state.robots
            })
        }
    }
</script>