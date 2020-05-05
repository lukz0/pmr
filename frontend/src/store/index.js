import Vue from 'vue'
import Vuex from 'vuex'

import { alert } from './alert.module';
import { account } from './account.module';
import { users } from './users.module';

Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
    alert, // Represent the error message model
    account, // The current logged in user
    users // List of users (will be empty if you are not admin)
  }
});
