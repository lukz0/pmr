import Vue from 'vue';
import Router from 'vue-router';

import LoginPage from '../views/Login'
import Dashboard from "../components/Dashboard/Dashboard";
import MiRDash from "../views/MiRDash";
import UserManager from "../components/UserManager/UserManager";
import Profile from "../components/Profile/Profile";
import Robots from "../components/Dashboard/Robots";
import Missions from "../components/Dashboard/Missions";
import Stats from "../components/Dashboard/Stats";
import Users from "../components/UserManager/Users";
import Register from "../components/UserManager/Register";
import Edit from "../components/UserManager/Edit";

Vue.use(Router);

export const router = new Router({
  mode: 'history',
  routes: [
    // { path: '/', component: HomePage },
    // { path: '/register', component: RegisterPage },
    { path: '/login', component: LoginPage },
    { path: '/', component: MiRDash, children: [
        {path: '/', component: Dashboard, children: [
            {path:'/', component: Robots},
            {path:'missions', component: Missions},
            {path:'stats', component: Stats}
          ]},
        {path: 'usermanager', component: UserManager, children: [
            {path:'/', component: Users},
            {path:'register', component: Register},
            {path:'edit', component: Edit}
          ]},
        {path: 'profile', component: Profile, children: [
            {path:'/'},
            {path:'edit'}
      ]},
  ]},
    // otherwise redirect to home
    { path: '*', redirect: '/' }
]});

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ['/login'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('user');

  if (authRequired && !loggedIn) {
    return next('/login');
  }

  next();
});