import axios from 'axios'
import { requestService } from "./request.service";

export const robotService = {
    getAll,
    add,
}

// Get all relisted robots
function getAll() {
    return axios.get(`/robots`)
        .then(r => requestService.handleResponse(r, null), e => requestService.handleResponse(e.response, e))
}
// Register new robot
function add(robot) {
    return axios.post(`/robots`, robot)
        .then(r => requestService.handleResponse(r, null), e => requestService.handleResponse(e.response, e));
}