import Router from 'vue-router';
import Vue from 'vue';


import Register from "../components/UserManager/Register";
import Users from "../components/UserManager/Users";
import Dashboard from "../views/Dashboard";
import Notfound from "../views/Notfound";
import Login from '../views/Login';

import Help from "../views/Help";
import Home from "../components/Dashboard/Home";
import Stats from "../components/Dashboard/Stats";
import Profile from "../components/Profile/Profile";
import AddRobot from "../components/Robot/add";
import Robots from "../components/Robot/Home"
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
            path: '/', component: Dashboard, children: [
                {path: '/', component: Home},
                {path: '/dashboard', component: Home, name: 'Main dashboard'},
                {path: '/stats', component: Stats},
                {path: '/missions', component: Missions}
            ]
        },
        {
            // Robots
            path: '/robots', component: Dashboard, children: [
                { path: '/', component: Robots},
                { path: '/robots/add', component: AddRobot}
            ]
        },
        {
            // User manager pages
            path: '/', component: Dashboard, children: [
                {path: '/usermanager', component: Users, name: 'Manger users'},
                {path: 'register', component: Register, name: 'Add new User'},
                {path: 'groups', comment: Users}
            ]
        },
        {
            // Account page
            path: '/account', component: Profile, children: [
                {path: '/profile', component: Profile, name: 'User Profile'},
                {path: '/edit'}
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
    document.title = to.name || 'Dashboard'
    next();
});