import Vue from "vue";
import VeeValidate from 'vee-validate'

// Page CSS
import '@fortawesome/fontawesome-free/css/all.min.css'
import 'startbootstrap-sb-admin-2/css/sb-admin-2.min.css'

import { store } from "./store";
import { router } from "./helpers";
import App from "./App";

Vue.config.productionTip = false;

Vue.use(VeeValidate);

new Vue({
  el: `#app`,
  router,
  store,
  render: h => h(App)
});