<template>
    <div id="UsermanagerUserlist">
        <h1></h1>
        <h1>Manage Users</h1>
        <b-table :fields="fields" :items="users">
            <template v-slot:cell(Edit)="data">
                <b-button-group class="mx-1">
                    <b-button>View/Edit</b-button>
                    <b-button :id="`UsermanagerDelete${data.item.username}`" v-if="profile.id !== data.item.id"
                              @click="deleteUser({id: data.item.id, api: $api})">Delete
                    </b-button>
                </b-button-group>
            </template>
        </b-table>
    </div>
</template>

<script>
    import {mapActions, mapState} from 'vuex'

    export default {
        name: "Users",
        props: {
            viewUsers: []
        },
        data() {
            return {
                fields: [
                    { key: 'id', label: 'UserId' },
                    { key: 'username', label: 'Username'},
                    { key: 'firstName', label: 'Full name', formatter: (value, key, item) => item.firstName + ' ' + item.lastName},
                    { key: 'email', label: 'Email address'},
                    { key: 'role', label: 'Policy', formatter: (value, key, item) => item.role != null ? item.role : 'User'},
                    { key: 'Edit', label: '#'}]
            }
        },
        created() {
            this.getAll(this.$api);
        },
        methods: {
            ...mapActions('users', {
                getAll: 'getAll',
                deleteUser: 'delete'
            })
        },
        computed: {
            ...mapState('users', {
                users: state => state.users
            }),
            ...mapState('account', {
                profile: state => state.user
            })
        }
    }
</script>

<style scoped>

</style>