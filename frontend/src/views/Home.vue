<template>
  <div>
  <Navbar v-bind:User="account.user"/>
    <b-container fluid="">
      <b-row>
        <b-col sm="3" class="m-lg-2">
          <Sidebar/>
        </b-col>
        <b-col sm="8">
            <b-jumbotron>
              <template v-slot:header>Velkomen {{account.user.firstName}}!</template>
              <template v-slot:lead>
                Bruk postman for å registrere flere brukere. Du kan send POST request til
                <code>https://localhost:5001/</code>
                <pre>
                  {
                    "firstName": "Knut",
                    "lastName": "Larsen",
                    "username": "knuseten95",
                    "password": "Password1."
                  }
                </pre>
              </template>

              <hr class="my-4">
              <b-spinner label="Loading..." v-show="users.loading"></b-spinner>
              <span v-if="users.error" class="text-danger">ERROR: {{users.error}}</span>
              <UsersList v-if="users.items" v-bind:users="users.items" v-on:del-user="delete_user"></UsersList>

            </b-jumbotron>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
  import Navbar from "@/components/Navbar";
  import UsersList from "@/components/UsersList"
  import { mapState, mapActions } from 'vuex'
  import Sidebar from "@/components/Sidebar";

  export default {
    components: { Sidebar, UsersList, Navbar },
    computed: {
      ...mapState({
        account: state => state.account,
        users: state => state.users.all
      })
    },
    created () {
      this.getAllUsers();
    },
    methods: {
      ...mapActions('users', {
        getAllUsers: 'getAll',
        deleteUser: 'delete'
      }),
      delete_user(id){
        this.deleteUser(id)
      }
    }
  };
</script>
