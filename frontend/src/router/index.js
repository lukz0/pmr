import Router from 'vue-router';
import Vue from 'vue';


import Register from "../components/UserManager/Register";
import Users from "../components/UserManager/Users";
import Edit from "../components/UserManager/Edit";

import UserManager from "../views/Usermanager";
import Dashboard from "../views/Dashboard";
import Notfound from "../views/Notfound";
import Account from "../views/Account";
import Login from '../views/Login';

import Help from "../views/Help";
import Home from "../components/Dashboard/Home";
import Stats from "../components/Dashboard/Stats";
import Robots from "../components/Dashboard/Robots";
import Missions from "../components/Dashboard/Missions";

Vue.use(Router);

export const router = new Router({
    mode: 'history',
    routes: [
        {
            path: '/login', component: Login, name: 'Authentication'
        },
        {
            path: '/help', component: Help, name: 'Help'
        },
        {
            // Dashboard
            path: '/', component: Dashboard, name: 'Main dashboard', children: [
                {path: '/', component: Home},
                {path: '/dashboard', component: Home},
                {path: '/stats', component: Stats},
                {path: '/missions', component: Missions},
                {path: '/robots', component: Robots}
            ]
        },
        {
            // User manager pages
            path: '/', component: Dashboard, children: [
                {path: '/usermanager', component: UserManager},
                {path: 'register', component: Register},
                {path: 'edit', component: Edit},
                {path: 'groups', comment: Users}
            ]
        },
        {
            // Account page
            path: 'account', component: Account, children: [
                {path: '/'},
                {path: 'edit'}
            ]
        },
        {
            // 404 - Page not found
            path: '*', component: Notfound
        }
    ]
});

router.beforeEach((to, from, next) => {
    // Redirect to login page if not logged in and trying to access a restricted page
    const publicPages = ['/login', '/help'];
    const authRequired = !publicPages.includes(to.path);
    const loggedIn = localStorage.getItem('user');

    if (authRequired && !loggedIn) {
        return next('/login');
    }

    // Update the page title
    document.title = to.name || 'MiRDash'
    next();
});