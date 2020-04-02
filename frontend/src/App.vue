<template>
  <div>
    <b-alert v-if="alert.message" :class="`alert ${alert.type}`" show>{{alert.message}}</b-alert>
    <router-view></router-view>
  </div>
</template>

<script>
  import { mapState, mapActions } from 'vuex'
  export default {
    name: 'app',
    computed: {
      ...mapState({
        alert: state => state.alert
      })
    },
    methods: {
      ...mapActions({
        clearAlert: 'alert/clear'
      })
    },
    watch: {
      $route (){
        // clear alert on location change
        this.clearAlert();
      }
    }
  };
</script>