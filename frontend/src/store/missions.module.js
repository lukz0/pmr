import {missionsService} from "../services/missions.service";


const state = {
    all: {}
};

const actions = {
    getAll({commit}) {
        commit('getAllRequest');

        missionsService.getAllMissions()
            .then(
                missions => commit('getAllMissionsSuccess', missions),
                error => commit('getAllMissionsFailure', error)
            )
        missionsService.getAllMissionQues()
            .then(
                missionQueue => commit('getAllQueuesSuccess', missionQueue),
                error => commit('getAllQueuesFailure', error)
            )
    }
}

const mutations = {
    getAllRequest(state) {
        state.status = { isLoading: true }
    },
    getAllMissionsSuccess(state, missions) {
        state.all = { missionsList: missions }
        state.status = { isLoading: false }
    },
    getAllMissionsFailure(state, error) {
        state.all = { error }
    },
    getAllQueuesSuccess(state, queues) {
        state.all = { queueList: queues }
        state.status = { isLoading: false }
    },
    getAllQueuesFailure(state, error) {
        state.all = { error }
    }
}


export const missions = {
    namespaced: true,
    actions,
    mutations,
    state
};