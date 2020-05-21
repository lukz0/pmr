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
        created() {
            this.getAll()
        },
        methods: {
            ...mapActions('missions', {
                getAll: 'getAll'
            })
        },
        computed: {
            ...mapState({
                missions: state => state.missions
            })
        }
    }
</script>



<style scoped>

</style>