/*
    Description:
    Auth header is a helper function that returns an HTTP Authorization header
    containing the JSON Web Token (JWT) of the current logged in user from local storage.
    If the user isn't logged in an empty object is returned.
 */

// Use: This function is used to make authenticated HTTP requests to the server api using jwt authentication
export function authHeader() {
  // return authorization header with jwt token
  let user = JSON.parse(localStorage.getItem('user'));

  if (user && user.token) {
    return { 'Authorization': 'Bearer ' + user.token };
  } else {
    return {};
  }
}