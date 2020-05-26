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
                            <div v-if="m.robotId === currentRobot.id">
                                <b-list-group-item button>{{m.name}}</b-list-group-item>
                                <b-button variant="primary" @click="addToQueue(m)">Add to queue</b-button>
                            </div>
                        </b-list-group>
                    </div>
                </b-col>
                <b-col cols="4">
                    <h2>Queue</h2>
                    <div v-if="currentRobot">
                        <b-list-group v-for="q in missionQueue.all.missionQueue" :key="q.id">
                            <b-list-group-item button>{{q.url}}</b-list-group-item>
                            <b-button variant="primary">{{q.state}}</b-button>
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
                currentRobot: null,
                polling: null,
                addRequestBody: {
                    mission_id: null,
                    name: "",
                    guid: "",
                    description: ""
                }
            }
        },
        created() {
            this.getAllMissions();
            this.getAllRobots();
            this.pollData()
        },
        methods: {
            ...mapActions('missions', {
                getAllMissions: 'getAll'
            }),
            ...mapActions('missionQueue', {
                getMissionQueue: 'getMissionQueue',
                addMissionToQueue: 'addMissionToQueue'
            }),
            ...mapActions('robots', {
                getAllRobots: 'getAll'
            }),
            pollData () {
                this.polling = setInterval(() => {
                    if(this.currentRobot){this.getMissionQueue(this.currentRobot.id)}
                }, 1000)
            },
            select(robot){
                this.currentRobot = robot;
                this.getMissionQueue(robot.id);
            },
            addToQueue(mission){
                this.fillInRequestBody(mission)
                this.addMissionToQueue({mission: this.addRequestBody, id: this.currentRobot.id})
            },
            fillInRequestBody(mission){
                this.addRequestBody.mission_id = mission.id
                this.addRequestBody.guid = mission.guide
                this.addRequestBody.name = mission.name
                this.addRequestBody.description = "A mission is a Mission"
            }
        },
        computed: {
            ...mapState({
                missions: state => state.missions,
                missionQueue: state => state.missionQueue,
                robots: state => state.robots
            })
        }
    }
</script>