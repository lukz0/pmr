<template>
    <div id="test-users-list" class="jumbotron">
        <h2>
            <b-icon-people-fill/>
            Manage Users
        </h2>
        <b-table
                striped
                hover
                :fields="fields"
                :items="users">
            <template v-slot:cell(Edit)="data">
                <b-button-group class="mx-1">
                    <b-button variant="info" @click="loadUser(data.item)">
                        <b-icon-person-lines-fill/>
                        View/Edit
                    </b-button>
                    <b-button variant="warning" :id="`test-delete-user${data.item.username}`"
                              v-if="account.user.id !== data.item.id"
                              @click="deleteUser({Id: data.item.Id})">
                        <b-icon-person-dash/>
                        Delete
                    </b-button>
                </b-button-group>
            </template>
        </b-table>

        <b-modal v-model="modalShow" header-bg-variant="dark" header-text-variant="light">
            <template v-slot:modal-title>
                <b-icon-person-bounding-box/>
                {{user.firstName}} {{user.lastName}} profile card
            </template>
            <div>
                <table class="table table-borderless table-hover">
                    <tr>
                        <th>UserId</th>
                        <td>{{user.id}}</td>
                    </tr>
                    <tr>
                        <th>Name</th>
                        <td>{{user.firstName}} {{user.lastName}} </td>
                    </tr>
                    <tr>
                        <th>Username</th>
                        <td>{{user.username}}</td>
                    </tr>
                    <tr>
                        <th>Email</th>
                        <td>{{user.email}}</td>
                    </tr>
                    <tr>
                        <th>Role</th>
                        <td>{{user.role}}</td>
                    </tr>
                </table>
            </div>
        </b-modal>
    </div>
</template>

<script>
    import {mapActions, mapState} from 'vuex'
    export default {
        name: "Users",
        data() {
            return {
                modalShow: false,
                user: {'Id': '', firstName: '', lastName: '', username: ''},
                fields: [
                    {key: 'username', label: 'Username', sortable: true},
                    {
                        key: 'firstName',
                        label: 'Full name',
                        sortable: true,
                        formatter: (value, key, item) => item.firstName + ' ' + item.lastName
                    },
                    {key: 'email', label: 'Email address', sortable: true},
                    {
                        key: 'role',
                        label: 'Policy',
                        sortable: true,
                        formatter: (value, key, item) => item.role != null ? item.role : 'User'
                    },
                    {key: 'Edit', label: '#'}]
            }
        },
        created() {
            this.getAll();
        },
        methods: {
            ...mapActions('users', {
                getAll: 'getAll',
                deleteUser: 'delete'
            }),
            loadUser: function (user) {
                this.user = user;
                this.modalShow = !this.modalShow
            }
        },
        computed: {
            ...mapState({
                account: state => state.account,
                users: state => state.users.all.items
            })
        }
    }
</script>