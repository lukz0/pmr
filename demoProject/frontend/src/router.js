import Vue from 'vue'
import Router from 'vue-router'
import {store} from './stores/store'

import Error404 from "./pages/404";

import Profile from "./pages/Profile";
import Dashboard from "./pages/Dashboard";
import Register from "./pages/Register";

import Missions from "./pages/Missions";
import Stats from "./pages/Stats";
import Registers from "./pages/Registers";
import Settings from "./pages/Settings";

Vue.use(Router);

const router = new Router({
    mode: 'history',
    routes: [
        {
            path: '/',
            name:'dashboard',
            component:Dashboard,
            children: [
                {
                    path:'missions',
                    component: Missions
                },
                {
                    path:'registers',
                    component: Registers
                },
                {
                    path:'stats',
                    component: Stats
                },
                {
                    path:'settings',
                    component: Settings
                }
            ]
        },
        {
            path: '/register',
            name: 'register',
            component: Register
        },
        {
            path: '/profile/:name?',
            name:'profile',
            component:Profile,
            meta: {auth: true},
            children: [
                {
                    path:'stats',
                    component: Stats
                }
            ]
        },
        {
            path: '*',
            name:'404',
            component:Error404

        }
    ]
});

router.beforeEach((to, from, next) => {
    function proceed() {
        if(to.matched.some(record => record.meta.auth)) {
            let authenticated = true;
            if(authenticated){
                console.log("User Authenticated");
                next();
            }
            else{
                // redirect to login
            }
        }
        next();
    }

    if(!store.state.appReady)
    {
        store.watch(
            (state) => state.appReady,
            (ready) => {
                if(ready){
                    proceed();
                }
            }
        )
    }else{
        proceed();
    }





});

export default router;