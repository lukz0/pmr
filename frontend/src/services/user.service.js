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

function login(payload) {
    let { username, password, api } = payload;

    return api.post('/users/authenticate', {username: username, password: password})
        .then(response => {
            // login successful if there's a jwt token in the response
            if (response.data.token) {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(response.data));
            }
            return response.data;})
        .catch(handleError);
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
}

function register(payload) {
    let {user, api} = payload;
    return api.post('/users/register', user)
        .then(response => {return response.data})
        .catch(handleError);
}

function getAll(api) {
    return api.get(`/users`, {headers:authHeader()})
        .then(response => {return response.data})
        .catch(handleError);
}


function getById(id, api) {
    return api.get(`/users/${id}`, {headers:authHeader()})
        .then(response => {return response.data})
        .catch(handleError);
}

function update(user, api) {
    return api.put(`/users/${user.id}`, {headers: authHeader(), user: user})
        .then(response => {return response.data})
        .catch(handleError);
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(payload) {
    let {id, api} = payload;
    return api.delete(`/users/${id}`, {headers: authHeader()})
        .then(response => {return response.data})
        .catch(handleError);
}

function handleError(error) {
    if (error.status === 401) {
        // auto logout if 401 response returned from api
        logout();
        location.reload(true);
    }
    // return Promise.reject(error);
    return error.data;
}