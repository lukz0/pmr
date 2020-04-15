<template>
    <div>
        <b-navbar toggleable="sm" type="dark" variant="dark">
            <b-navbar-brand to="/">MiRDash</b-navbar-brand>
            <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
            <b-collapse id="nav-collapse" is-nav>
                <!--                everything inside here will collapse-->
            </b-collapse>

            <b-nav-item-dropdown right>
                <!-- Using 'button-content' slot -->
                <template v-slot:button-content>
                    <b-icon-people-circle />
                    <!--{{user.firstName}}-->
                </template>
                <b-dropdown-item to="/"><b-icon-house-door/> Dashboard</b-dropdown-item>
                <b-dropdown-item to="/usermanager"><b-icon-people-fill/> User Settings</b-dropdown-item>
                <b-dropdown-item to="/profile"><b-icon-person-lines-fill />Profile settings</b-dropdown-item>
                <b-dropdown-item to="/login"><b-icon-lock /> Sign Out</b-dropdown-item>
            </b-nav-item-dropdown>
        </b-navbar>
    </div>

</template>
<script>
    //import {mapActions, mapState} from 'vuex';

    export default {
        name: "Navbar"
        // created() {
        //     this.createRobots();
        // },
        // computed:{
        //     ...mapState('account', {
        //         user: state => state.user
        //     }),
        //     ...mapState('robots',{
        //         robots: state => state.robots
        //     })
        // },
        // methods:{
        //     ...mapActions('robots',{
        //         createRobots: 'createTestData',
        //         setRobot: 'setCurrentRobot'
        //     })
        // }
    }
</script>
