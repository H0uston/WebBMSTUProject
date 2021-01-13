import {SET_IS_AUTHENTICATED, SET_TOKEN, setIsAuthenticated, setToken} from "./authAction";
import {setIsFetching} from "../fetchingReducer/fetchingAction";
import {authAPI} from "../../API/authAPI";


let initialState = {
    isAuthenticated: false,
    token: null
};


const authReducer = (state=initialState, action) => {
    let stateCopy = {...state};

    switch (action.type) {
        case (SET_TOKEN):
            stateCopy.token = action.token;
            break;
        case (SET_IS_AUTHENTICATED):
            stateCopy.isAuthenticated = true;
            break;
        default:
            break;
    }

    return stateCopy;
};

export const login = (data) => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await authAPI.fetchLogin(data);
    if (response.status === 200) {
        let result = await response.json();
        dispatch(setToken(result.access_token));
        dispatch(setIsAuthenticated(true));
        dispatch(setIsFetching(false));
    }
};

export default authReducer;