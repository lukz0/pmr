<template>
    <b-list-group>
        <b-list-group-item href="#" active>
            <b-icon-house-door />
            Dashboard
        </b-list-group-item>
        <b-list-group-item href="#">
            <b-icon-bar-chart/>
            Stats
        </b-list-group-item>
        <b-list-group-item href="#">
            <b-icon-clipboard-data/>
            Missions
        </b-list-group-item>
        <b-list-group-item href="#">
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