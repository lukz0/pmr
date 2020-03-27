<template>
  <div id="app" class="app">
    <Popup></Popup>
    <div class="loader" v-if="!ready"> LOADING </div>
    <div v-else>
      <div class="top-bar">
        <ul>
          <li><router-link to="/profile">Profiles</router-link></li>
          <li><router-link to="/register">Register</router-link></li>
          <li><router-link to="/">Dashboard</router-link></li>
        </ul>
      </div>
      <div>
        <router-view class="page"></router-view>
      </div>
    </div>
  </div>
</template>

<script>
  import  {mapActions, mapState} from "vuex";
  import Popup from "./components/Popup";

export default {
  name: "App",
  components:{
    Popup
  },
  created() {
    this.init();
    this.loadProfiles(this.$api);
  },
  methods: {
    ...mapActions({
      init: "INIT_APP"
    }),
    ...mapActions('profiles',{
      loadProfiles: 'LOAD_PROFILES'
    })
  },
  computed: {
    ...mapState({
      ready: "appReady"
    }),
    ...mapState('profiles',{
      profiles: state => state.profiles
    })
  }
};
</script>

<style lang="stylus">
  .page
    background aqua

  .loader
    background #eee
    position fixed
    width 100%
    height 100%
    top 0
    left 0

  .top-bar
    ul
      list-style-type: none;
      margin: 0;
      padding: 0;
      overflow: hidden;
      background-color: #333;

    li
      float: right;

    li a
      display: block;
      color: white;
      text-align: center;
      padding: 14px 16px;
      text-decoration: none;

    li a:hover
      background-color: #111;
</style>
