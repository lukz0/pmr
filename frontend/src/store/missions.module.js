import {missionsService} from "../services/missions.service";


const state = {
    all: {}
};

const actions = {
    getAll({commit}) {
        commit('getAllRequest');

        missionsService.getAll()
            .then(
                missions => commit('getAllSuccess', missions),
                error => commit('getAllFailure', error)
            )
    }
}

const mutations = {
    getAllRequest(state) {
        state.status = { isLoading: true }
    },
    getAllSuccess(state, missions) {
        state.all = { items: missions }
        state.status = { isLoading: false }
    },
    getAllFailure(state, error) {
        state.all = { error }
    }
}


export const missions = {
    namespaced: true,
    actions,
    mutations,
    state
};