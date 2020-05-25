<template>
    <b-jumbotron class="mt-4" id="test-users-list" >
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
                        Edit
                    </b-button>
                    <b-button variant="warning" :id="`test-delete-user${data.item.username}`"
                              v-if="account.user.id !== data.item.id"
                              @click="delete_user(data.item.id)">
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
                <b-form @submit="onSubmit" @reset="onReset" v-if="show">
                    <b-form-group
                            id="input-group-1"
                            label="Email address:"
                            label-for="input-1"
                            description="We'll never share your email with anyone else."
                    >
                        <b-form-input
                                id="input-1"
                                v-model="form.email"
                                type="email"
                                required
                        ></b-form-input>
                    </b-form-group>

                    <b-form-group id="input-group-2" label="Your Name:" label-for="input-2">
                        <b-form-input
                                id="input-2"
                                v-model="form.name"
                                required
                                placeholder="Enter name"
                        ></b-form-input>
                    </b-form-group>

                    <b-form-group id="input-group-3" label="Food:" label-for="input-3">
                        <b-form-select
                                id="input-3"
                                v-model="form.food"
                                :options="foods"
                                required
                        ></b-form-select>
                    </b-form-group>

                    <b-form-group id="input-group-4">
                        <b-form-checkbox-group v-model="form.checked" id="checkboxes-4">
                            <b-form-checkbox value="me">Check me out</b-form-checkbox>
                            <b-form-checkbox value="that">Check that out</b-form-checkbox>
                        </b-form-checkbox-group>
                    </b-form-group>

                    <b-button type="submit" variant="primary">Submit</b-button>
                    <b-button type="reset" variant="danger">Reset</b-button>
                </b-form>
                <b-card class="mt-3" header="Form Data Result">
                    <pre class="m-0">{{ form }}</pre>
                </b-card>
            </div>
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
    </b-jumbotron>
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
                    {key: 'Edit', label: '#'}],
                form: {
                    name: this.user.name,
                    username: '',
                    email: '',
                    Role: '',
                    food: null,
                    checked: []
                },
                foods: [{ text: 'Select One', value: null }, 'Carrots', 'Beans', 'Tomatoes', 'Corn'],
                show: true
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
            },
            delete_user(id){
                this.deleteUser(id);
            },
            onSubmit(evt) {
                evt.preventDefault()
                alert(JSON.stringify(this.form))
            },
            onReset(evt) {
                evt.preventDefault()
                // Reset our form values
                this.form.email = ''
                this.form.name = ''
                this.form.food = null
                this.form.checked = []
                // Trick to reset/clear native browser form validation state
                this.show = false
                this.$nextTick(() => {
                    this.show = true
                })
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