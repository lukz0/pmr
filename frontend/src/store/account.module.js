import { userService } from '../services/user.service';
import { router } from '../router';

const user = JSON.parse(localStorage.getItem('user'));
const state = user
    ? { status: { loggedIn: true }, user }
    : { status: {}, user: null };

const actions = {
    login({ dispatch, commit }, payload) {
        let { username, password, api } = payload;
        commit('loginRequest', { username });

        userService.login({username: username, password: password, api: api})
            .then(
                user => {
                    commit('loginSuccess', user);
                    router.push('/');
                },
                error => {
                    commit('loginFailure', error);
                    dispatch('alert/error', error, { root: true });
                }
            );
    },
    logout({ commit }) {
        userService.logout();
        commit('logout');
    },
    register({ dispatch, commit }, payload) {

        let{user, api} = payload;
        commit('registerRequest', user);

        userService.register({user: user, api: api})
            .then(
                user => {
                    commit('registerSuccess', user);
                    setTimeout(() => {
                        // display success message after route change completes
                        dispatch('alert/success', 'Registration successful', { root: true });
                        router.push("/usermanager");
                    })
                },
                error => {
                    commit('registerFailure', error);
                    dispatch('alert/error', error, { root: true });
                }
            );
    }
};

const mutations = {
    loginRequest(state, user) {
        state.status = { loggingIn: true };
        state.user = user;
    },
    loginSuccess(state, user) {
        state.status = { loggedIn: true };
        state.user = user;
    },
    loginFailure(state) {
        state.status = {};
        state.user = null;
    },
    logout(state) {
        state.status = {};
        state.user = null;
    },
    registerRequest(state, user) { // eslint-disable-line no-unused-vars
        state.status = { registering: true };
    },
    registerSuccess(state, user) { // eslint-disable-line no-unused-vars
        state.status = {};
    },
    registerFailure(state, error) {
        state.status = {};
        console.log(error)
    }
};

export const account = {
    namespaced: true,
    state,
    actions,
    mutations
};