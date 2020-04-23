import '@babel/polyfill'
import 'mutationobserver-shim'
import './plugins/bootstrap-vue'
import { router } from './router'
import { store } from './store'
import App from './App.vue'
import axios from 'axios'
import Vue from 'vue'

Vue.config.productionTip = false;


const api = axios.create({
  baseURL: "https://localhost:5001/"
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
