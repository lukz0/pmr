<template>
    <b-list-group>
        <b-list-group-item to="/" active>
            <b-icon-question-circle/>Robots
        </b-list-group-item>
        <b-list-group-item to="/missions">
            <b-icon-house-door />Missions
        </b-list-group-item>
        <b-list-group-item to="/stats">
            <b-icon-bar-chart/>Stats
        </b-list-group-item>
    </b-list-group>
</template>

<script>
    export default {
        name: "Sidebar"
    }
</script>