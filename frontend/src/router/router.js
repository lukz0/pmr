/*
    Description:
    The vue router defines all the routes for the application,
    and contains a function that runs before each route change to prevent unauthenticated users
    from accessing restricted routes
 */
import Vue from 'vue';
import Router from 'vue-router';

import Home from '../views/Home'
import Login from '../views/Login'
import Register from '../views/Register'
import Dashboard from '../views/Dashboard'

Vue.use(Router);

export const router = new Router({
    mode: 'history',
    routes: [
        { path: '/', component: Home },
        { path: '/login', component: Login },
        { path: '/register', component: Register },
        { path: '/dashboard', component: Dashboard},

        // otherwise redirect to home
        { path: '*', redirect: '/' }
    ]
});

router.beforeEach((to, from, next) => {
    // redirect to login page if not logged in and trying to access a restricted page
    const publicPages = ['/login'];
    const authRequired = !publicPages.includes(to.path);
    const loggedIn = localStorage.getItem('user');

    if (authRequired && !loggedIn) {
        return next('/login');
    }

    next();
})