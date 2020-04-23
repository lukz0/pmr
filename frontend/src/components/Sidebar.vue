<template>
    <b-list-group>
        <b-list-group-item to="/dashboard" active>
            <b-icon-house-door />
            Dashboard
        </b-list-group-item>
        <b-list-group-item to="stats">
            <b-icon-bar-chart/>
            Stats
        </b-list-group-item>
        <b-list-group-item to="missions">
            <b-icon-clipboard-data/>
            Missions
        </b-list-group-item>
        <b-list-group-item to="robots">
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
        name: "Sidebar"
    }
</script>