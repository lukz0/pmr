<template>
    <b-jumbotron>
        <h2>Robots</h2>
        <div>
            <b-list-group horizontal="md">
                <b-list-group-item to="robots/add">Add new robot</b-list-group-item>
                <b-list-group-item to="delete">Update robot</b-list-group-item>
                <b-list-group-item to="edit">Modified</b-list-group-item>
            </b-list-group>
        </div>
        <div class="text-center mt-5" v-if="robots.status.isLoading">
            <b-spinner variant="primary" style="width: 3rem; height: 3rem;" label="Large Busy"></b-spinner>
        </div>
        <b-list-group v-if="!robots.status.isLoading">
            <b-list-group-item button>Button item</b-list-group-item>
            <b-list-group-item button>I am a button</b-list-group-item>
            <b-list-group-item button disabled>Disabled button</b-list-group-item>
            <b-list-group-item button>This is a button too</b-list-group-item>
        </b-list-group>
        {{robots}}
        <Robotcard></Robotcard>
    </b-jumbotron>
</template>

<script>
    vue.use(Robotcard)
    import { mapState, mapActions } from 'vuex'
    import Robotcard from "@/Robotcard";
    export default {
        name: "Robots",
        components: {Robotcard},
        created() {
            this.getAll()
        },
        methods: {
            ...mapActions('robots', {
                getAll: 'getAll'
            })
        },
        computed: {
            ...mapState({
               robots: state => state.robots
            })
        }
    }
</script>