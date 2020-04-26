<template>
  <div>
<!--    <b-alert v-if="alert.message" :class="`alert ${alert.type}`" id="Alert" show>{{alert.message}}</b-alert>-->
    <b-modal
            v-model="alert.type"
            :header-bg-variant="headerBgVariant(alert.type)"
            ok-only>
        <template v-slot:modal-title>
            {{ headerTitle(alert.type) }}
        </template>
        {{ alert.message }}
    </b-modal>
    <router-view></router-view>
  </div>
</template>

<script>
  import { mapState, mapActions } from 'vuex'
  export default {
    name: 'app',
    data(){
      return{
          headerBgVariant : function (type) { return type !== true ? "success" : "danger"},
          headerTitle: function (type) { return type !== true ? "Successfully resisted" : "Error"}
      }
    },
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