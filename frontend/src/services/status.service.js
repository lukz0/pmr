import axios from 'axios'
import { requestService } from "./request.service";

export const statusService = {
    getByRobotId
}

// Get all missions
function getByRobotId(id) {
    return axios.get(`/status/robotid=`+id)
        .then(r => requestService.handleResponse(r, null), e => requestService.handleResponse(e.response, e))
}