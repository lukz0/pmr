import axios from 'axios'
import { requestService } from "./request.service";

export const missionsService = {
    getAll
}

// Get all missions
function getAll() {
    return axios.get(`/missions`)
        .then(r => requestService.handleResponse(r, null), e => requestService.handleResponse(e.response, e))
}