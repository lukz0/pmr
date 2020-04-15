import { authHeader } from '../services';

export const userService = {
    login,
    logout,
    register,
    getAll,
    getById,
    update,
    delete: _delete
};

function login({ username, password, api }) {
    return api.post(`/users/authenticate`, { username: username, password: password })
        .then(r => handleResponse(r, null), e => handleResponse(e.response, e))
        .then(user => {
            // login successful if there's a jwt token in the response
            if (user.token) {
                localStorage.setItem('user', JSON.stringify(user));
            }
            return user;
        });
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
}

function register({ api, user }) {
    return api.post(`/users/register`, user, { headers: authHeader() })
        .then(r => handleResponse(r, null), e => handleResponse(e.response, e));
}

function getAll(api) {
    return api.get(`/users`, { headers: authHeader() })
        .then(r => handleResponse(r, null), e => handleResponse(e.response, e));
}

function getById(id, api) {
    return api.get(`/users/${id}`, { headers: authHeader() })
        .then(r => handleResponse(r, null), e => handleResponse(e.response, e));
}

function update(user, api) {
    return api.put(`/users/${user.id}`, user, { headers: authHeader() })
        .then(r => handleResponse(r, null), e => handleResponse(e.response, e));
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete({ id, api }) {
    return api.delete(`/users/${id}`, { headers: authHeader() })
        .then(r => handleResponse(r, null), e => handleResponse(e.response, e));
}
function handleResponse(response, error) {
    if (error !== null) {
        if (response.status === 401) {
            // auto logout if 401 response returned from api
            logout();
            location.reload(true);
        }
        return Promise.reject(response.data.message || `${response.statusText}: ${error.message}`);
    } else {
        return Promise.resolve(response.data);
    }
}