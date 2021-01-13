export const SET_TOKEN = "SET-TOKEN";
export const SET_IS_AUTHENTICATED = "SET-IS-AUTHENTICATED";

export const setIsAuthenticated = () => ({
    type: SET_IS_AUTHENTICATED
});

export const setToken = (token) => ({
    type: SET_TOKEN, token
});