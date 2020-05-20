<template>
    <div id="test-users-list" class="jumbotron">
        <h2>Missions</h2>
        <p v-for="m in missions.all.items" :key="m.id">{{m.name}}</p>
    </div>
</template>

<script>
    import {mapState, mapActions} from 'vuex'
    export default {
        name: "Missions",
        data(){
            return {
                polling: null
            }
        },
        created() {
            this.getAll()
        },
        methods: {
            ...mapActions('missions', {
                getAll: 'getAll'
            }),
            loadPlaceholder(text){
                return 'https://dummyimage.com/418x150/000/518c8b?text='+text
            },
            pollData () {
                this.polling = setInterval(() => {
                    this.$store.dispatch('missions/getAll')
                }, 1000)
            }
        },
        computed: {
            ...mapState({
                missions: state => state.missions
            })
        },
        beforeDestroy() {
            clearInterval(this.polling)
        }
    }
</script>



<style scoped>

</style>