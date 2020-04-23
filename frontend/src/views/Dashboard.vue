<template>
  <div>
  <Navbar v-bind:User="account.user"/>
    <b-container fluid="">
      <b-row>
        <b-col sm="3" class="m-lg-2">
          <Sidebar/>
        </b-col>
        <b-col sm="8">
          <router-view></router-view>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>
<script>
  import Navbar from "@/components/Navbar";
  import { mapState, mapActions } from 'vuex'
  import Sidebar from "@/components/Sidebar";

  export default {
    title(){
      document.title = 'Dashboard'
    },
    components: { Sidebar, Navbar },
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
