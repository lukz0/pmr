import axios from 'axios'

export const userService = {
    login,
    logout,
    register,
    getAll,
    getById,
    update,
    delete: _delete
};

function login(username, password) {
    return axios.post(`/users/authenticate`, { username, password })
        .then(r => handleResponse(r, null), e => handleResponse(e.response, e))
        .then(user => {
            // login successful if there's a jwt token in the response
            if (user.token) {
                localStorage.setItem('user', JSON.stringify(user));
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + user.token;
            }
            return user;
        });
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
}

function register(user) {
    return axios.post(`/users/register`, user)
        .then(r => handleResponse(r, null), e => handleResponse(e.response, e));
}

function getAll() {
    return axios.get(`/users`)
        .then(r => handleResponse(r, null), e => handleResponse(e.response, e));
}

function getById(id) {
    return axios.get(`/users/${id}`)
        .then(r => handleResponse(r, null), e => handleResponse(e.response, e));
}

function update(user) {
    return axios.put(`/users/${user.id}`, user)
        .then(r => handleResponse(r, null), e => handleResponse(e.response, e));
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {
    return axios.delete(`/users/${id}`)
        .then(r => handleResponse(r, null), e => handleResponse(e.response, e));
}
function handleResponse(response, error) {
    if (error !== null) {
        if (response.status === 401) {
            // auto logout if 401 response returned from api
            logout();
            console.log("You are about to logout")
            location.reload(true);
        }
        return Promise.reject(response.data.message || `${response.statusText}: ${error.message}`);
    } else {
        return Promise.resolve(response.data);
    }
}