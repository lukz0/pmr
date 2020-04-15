<template>
    <b-list-group>
        <b-list-group-item id="UsermanagerUsers" to="/usermanager" active>
            <b-icon-house-door />Users
        </b-list-group-item>
        <b-list-group-item id="UsermanagerRegister" to="/usermanager/register">
            <b-icon-house-door />Register
        </b-list-group-item>

    </b-list-group>
</template>

<script>
    export default {
        name: "Sidebar"
    }
</script>

<style scoped>

</style>