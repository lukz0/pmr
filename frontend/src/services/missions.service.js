import axios from 'axios'
import { requestService } from "./request.service";

export const missionsService = {
    getAllMissions,
    getAllMissionQues
}

// Get all missions
function getAllMissions() {
    return axios.get(`/missions`)
        .then(r => requestService.handleResponse(r, null), e => requestService.handleResponse(e.response, e))
}
function getAllMissionQues() {
    return axios.get(`/MissionQueueRequest`)
        .then(r => requestService.handleResponse(r, null), e => requestService.handleResponse(e.response, e))
}