<template>
    <div>
        <h1>Account</h1>
        <p>First Name: {{profile.firstName}}</p>
        <p>Last Name: {{profile.lastName}}</p>
        <p>Username: {{profile.username}}</p>
        <p>Role:</p>
        <p>Edit user info, ask for new role</p>
    </div>


</template>

<script>
    import {mapState} from "vuex";

    export default {
        name: "Profile",
        computed:{
            ...mapState('account', {
                profile: state => state.user
            })
        }
    }
</script>

<style scoped>

</style>