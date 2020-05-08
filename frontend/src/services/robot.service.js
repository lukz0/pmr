import {userService} from "./user.service";

export * from './user.service'
import axios from 'axios'

export const robotService = {
    getAll
}


function getAll() {
    return axios.get(`/api/robots`)
        .then(r => handleResponse(r, null), e => handleResponse(e.response, e))
}

function handleResponse(response, error) {
    if (error !== null) {
        if (response.status === 401) {
            // auto logout if 401 response returned from api
            userService.logout();
            location.reload(true);
        }
        return Promise.reject(response.data.message || `${response.statusText}: ${error.message}`);
    } else {
        return Promise.resolve(response.data);
    }
}