import { robotService } from "../services/robot.service";
//import { router } from '../router';

const state = {
    all: {}
};

const actions = {
    getAll({commit}) {
        commit('getAllRequest');

        robotService.getAll()
            .then(
                robots => setTimeout(() => commit('getAllSuccess', robots), 9000),
                error => console.log("Kunne ikke laste inn robotter"+ error)
            )
    }
}

const mutations = {
    getAllRequest(state) {
        state.status = { isLoading: true }
    },
    getAllSuccess(state, robots) {
        state.all = { items: robots }
        state.status = { isLoading: false }
    }
}

export const robots = {
    namespaced: true,
    state,
    actions,
    mutations
}