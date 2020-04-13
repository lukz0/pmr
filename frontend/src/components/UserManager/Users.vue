<template>
    <div>
        <h1>Users</h1>
        <p v-for="u in users" :key="u.id">{{u.firstName}}
            <b-button-toolbar aria-label="Toolbar with button groups and dropdown menu">
                <b-dropdown class="mx-1" right text="menu">
                    <b-dropdown-item>Edit</b-dropdown-item>
                    <b-dropdown-item @click="deleteUser({id: u.id, api: $api})">Delete</b-dropdown-item>
                </b-dropdown>
            </b-button-toolbar>
        </p>


    </div>

</template>

<script>
    import {mapActions, mapState} from 'vuex'
    export default {
        name: "Users",
        created() {
            this.getAll(this.$api);
        },
        methods:{
            ...mapActions('users',{
                getAll: 'getAll',
                deleteUser: 'delete'
            })
        },
        computed:{
            ...mapState('users',{
                users: state => state.users
            })
        }
    }
</script>

<style scoped>

</style>