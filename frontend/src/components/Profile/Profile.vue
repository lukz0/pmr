<template>
    <b-jumbotron class="mt-4">
        <h3>Account</h3>
        <p>First Name: {{profile.firstName}}</p>
        <p>Last Name: {{profile.lastName}}</p>
        <p>Username: {{profile.username}}</p>
        <p>Role: {{profile.role}}</p>
        <p>Edit user info, ask for new role</p>
    </b-jumbotron>
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