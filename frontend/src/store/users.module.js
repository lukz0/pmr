import { userService } from '../services/user.service';

const state = {
    all: {},
    users: {}
};

const actions = {
    getAll({ commit }, api) {
        commit('getAllRequest');

        userService.getAll(api)
            .then(
                users => commit('getAllSuccess', users),
                error => commit('getAllFailure', error)
            );
    },

    delete({ commit, dispatch }, payload) {
        let {id, api} = payload;
        commit('deleteRequest', id);

        userService.delete({id: id, api: api})
            .then(function () {
                dispatch('getAll', api);
                console.log('we in here');
            })
    }
};

const mutations = {
    getAllRequest(state) {
        state.all = { loading: true };
    },
    getAllSuccess(state, users) {
        state.all = { items: users };
        state.users = users;

    },
    getAllFailure(state, error) {
        state.all = { error };
    },
    deleteRequest(state, id) {
        // add 'deleting:true' property to user being deleted
        state.all.items = state.all.items.map(user =>
            user.id === id
                ? { ...user, deleting: true }
                : user
        )
    },
    deleteSuccess(state, id) {
        // remove deleted user from state
        state.all.items = state.all.items.filter(user => user.id !== id);
        state.users = state.users.filter(user => user.id !== id)
    },
    deleteFailure(state, { id, error }) {
        // remove 'deleting:true' property and add 'deleteError:[error]' property to user
        state.all.items = state.items.map(user => {
            if (user.id === id) {
                // make copy of user without 'deleting:true' property
                // eslint-disable-next-line no-unused-vars
                const { deleting, ...userCopy } = user;
                // return copy of user with 'deleteError:[error]' property
                return { ...userCopy, deleteError: error };
            }
            return user;
        })
    }
};

export const users = {
    namespaced: true,
    state,
    actions,
    mutations
};