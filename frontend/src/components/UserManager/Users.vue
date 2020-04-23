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
                    <b-button  @click="$bvModal.show('bv-modal')">View/Edit</b-button>
                    <b-button :id="`UsermanagerDelete${data.item.username}`" v-if="profile.id !== data.item.id"
                              @click="deleteUser({id: data.item.id, api: $api})">Delete
                    </b-button>
                </b-button-group>
            </template>
        </b-table>

        <b-modal id="bv-modal" hide-footer>
            <template v-slot:modal-title>
                Using <code>$bvModal</code> Methods
            </template>
            <div class="d-block text-center">
                <h3>Hello From This Modal!</h3>
            </div>
            <b-button class="mt-3" block @click="$bvModal.hide('bv-modal')">Close Me</b-button>
        </b-modal>

    </div>
</template>

<script>
    import {mapActions, mapState} from 'vuex'
    export default {
        name: "Users",
        data() {
            return {
                fields: [
                    { key: 'id', label: 'UserId', sortable: true },
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
            openModal() {
                this.modalOpen = !this.modalOpen;
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

<style scoped>

</style>