<template>
    <b-list-group>
        <b-list-group-item to="/dashboard" exact exact-active-class="active">
            <b-icon-house-door />
            Dashboard
        </b-list-group-item>
        <b-list-group-item to="stats" exact exact-active-class="active">
            <b-icon-bar-chart/>
            Stats
        </b-list-group-item>
        <b-list-group-item to="missions" exact exact-active-class="active">
            <b-icon-clipboard-data/>
            Missions
        </b-list-group-item>
        <b-list-group-item to="robots" exact exact-active-class="active">
            <b-icon-outlet/>
            Robots
        </b-list-group-item>
        <b-list-group-item to="help">
            <b-icon-question-circle/>
            Help
        </b-list-group-item>
    </b-list-group>
</template>

<script>
    export default {
        name: "Sidebar",
        data: function(){
            return {
                currentLocation: window.location.pathname
            }
        }
    }
</script>