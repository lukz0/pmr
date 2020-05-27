import { statusService } from "../services/status.service";
import {router} from "../router";

const state = {
    all: {}
};

const actions = {
    getByRobotId({commit}, id) {
        commit('getAllRequest');

        statusService.getByRobotId(id)
            .then(
                status => commit('getAllSuccess', status),
                error => commit('getAllFailure', error)
            )
    },
    sendState({dispatch}, {robotId, state_id}) {
        console.log("state id sendstate:", state_id);
        statusService.sendState(robotId, state_id)
            .then(
                () => {
                    router.push('/robots').catch(() => undefined);
                    dispatch('alert/success', 'Robot state put', { root: true });
                },
                error => {
                    dispatch('alert/error', error, { root: true });
                }
            );
    }
}

const mutations = {
    getAllRequest(state) {
        state.status = { isLoading: true }
    },
    getAllSuccess(state, status) {
        state.all = { items: status }
        state.status = { isLoading: false }
    },
    getAllFailure(state, error) {
        state.all = { error }
    },
    addRequest(state) {
        state.all = { adding: true }
    }
}

export const status = {
    namespaced: true,
    state,
    actions,
    mutations
}
