<template>
    <div id="UsermanagerUserlist">
        <h1>Users</h1>
        <p v-for="u in users" :key="u.id">{{u.username}}
            <b-button-toolbar aria-label="Toolbar with button groups and dropdown menu">
                <b-button-group class="mx-1">
                    <b-button>Edit</b-button>
                    <b-button :id="`UsermanagerDelete${u.username}`" v-if="profile.id !== u.id" @click="deleteUser({id: u.id, api: $api})">Delete</b-button>
                </b-button-group>
            </b-button-toolbar>
        </p>
    </div>
</template>

<script>
    import {mapActions, mapState} from 'vuex'
    export default {
        name: "Users",
        props:{
            viewUsers: []
        },

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
            }),
            ...mapState('account',{
                profile: state => state.user
            })
        }
    }
</script>

<style scoped>

</style>