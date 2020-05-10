import '@babel/polyfill'
import 'mutationobserver-shim'
import './plugins/bootstrap-vue'
import { router } from './router'
import { store } from './store'
import App from './App.vue'

import Vue from 'vue'

Vue.config.productionTip = false;

import axios from 'axios'

axios.defaults.baseURL = location.origin
const user = JSON.parse(localStorage.getItem('user'));
if (user)
  axios.defaults.headers.common['Authorization'] = 'Bearer ' + user.token;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
