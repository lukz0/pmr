<template>
    <div>
        <h1>Account (This page is not done)</h1>
        <p>First Name: {{profile.firstName}}</p>
        <p>Last Name: {{profile.lastName}}</p>
        <p>Username: {{profile.username}}</p>
        <p>Role: {{profile.role}}</p>
        <p>Edit user info, ask for new role</p>
    </div>
</template>

<script>
    import {mapState} from "vuex";

    export default {
        name: "Profile",
        computed:{
            ...mapState({
                profile: state => state.account.user
            })
        }
    }
</script>

<style scoped>

</style>