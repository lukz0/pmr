<template>
    <div id="UsermanagerUserlist" class="jumbotron">
        <h1>
            <b-icon-people-fill/>
            Manage Users
        </h1>
        <b-table
                striped
                hover
                :fields="fields"
                :items="users">
            <template v-slot:cell(Edit)="data">
                <b-button-group class="mx-1">
                    <b-button variant="info" @click="loadUser(data.item)">
                        <b-icon-person-bounding-box />
                        View/Edit</b-button>
                    <b-button variant="warning" :id="`UsermanagerDelete${data.item.username}`" v-if="profile.id !== data.item.id"
                              @click="deleteUser({id: data.item.id, api: $api})">
                        <b-icon-person-dash />
                        Delete
                    </b-button>
                </b-button-group>
            </template>
        </b-table>

        <b-modal v-model="modalShow">
            <div class="card">
                <img src="https://via.placeholder.com/286x180?text=Bruk" class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title">Card title</h5>
                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                </div>
                <ul class="list-group list-group-flush">
                    <li class="list-group-item">Cras justo odio</li>
                    <li class="list-group-item">Dapibus ac facilisis in</li>
                    <li class="list-group-item">Vestibulum at eros</li>
                </ul>
                <div class="card-body">
                    <a href="#" class="card-link">Card link</a>
                    <a href="#" class="card-link">Another link</a>
                </div>
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
                user: {'Id': '', firstName:'', lastName:'', username:''},
                fields: [
                    { key: 'username', label: 'Username', sortable: true },
                    {
                        key: 'firstName',
                        label: 'Full name',
                        sortable: true,
                        formatter: (value, key, item) => item.firstName + ' ' + item.lastName
                    },
                    { key: 'email', label: 'Email address', sortable: true },
                    {
                        key: 'role',
                        label: 'Policy',
                        sortable: true,
                        formatter: (value, key, item) => item.role != null ? item.role : 'User'
                    },
                    { key: 'Edit', label: '#' }]
            }
        },
        created() {
            this.getAll(this.$api);
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
            ...mapState('users', {
                users: state => state.users
            }),
            ...mapState('account', {
                profile: state => state.user
            })
        }
    }
</script>