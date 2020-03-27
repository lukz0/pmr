import Vue from "vue";
import App from "./App.vue";
import router from './router'
import axios from 'axios'
import {store} from './stores/store'

Vue.config.productionTip = false;

const api = axios.create({
  baseURL: "http://localhost:3000/"
});

const axiosPlugin = {
  install(Vue){
    Vue.prototype.$api = api;
  }
};

Vue.use(axiosPlugin);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
