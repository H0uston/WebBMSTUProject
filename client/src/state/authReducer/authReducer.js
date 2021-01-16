import {
    SET_IS_AUTHENTICATED,
    SET_TOKEN,
    setIsAuthenticatedAction,
    setTokenAction, UPDATE_EMAIL, UPDATE_IS_REGISTER_TAB, UPDATE_IS_REMEMBER_ME, UPDATE_PASSWORD,
    updateEmailAction, updateIsRegisterTabAction, updateIsRememberMeAction,
    updatePasswordAction
} from "./authAction";
import {setIsFetching} from "../fetchingReducer/fetchingAction";
import {authAPI} from "../../API/authAPI";


let initialState = {
    isAuthenticated: false,
    token: null,
    email: "",
    password: "",
    isRememberMe: false,
    isRegisterTab: false
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
        case (UPDATE_EMAIL):
            stateCopy.email = action.email;
            break;
        case (UPDATE_PASSWORD):
            stateCopy.password = action.password;
            break;
        case (UPDATE_IS_REMEMBER_ME):
            stateCopy.isRememberMe = action.isRememberMe;
            break;
        case (UPDATE_IS_REGISTER_TAB):
            stateCopy.isRegisterTab = action.isRegisterTab;
            break;
        default:
            break;
    }

    return stateCopy;
};

export const login = (data) => async (dispatch) => {
    debugger;
    let response = await authAPI.fetchLogin(data);
    if (response.status === 200) {
        let result = await response.json();
        dispatch(setTokenAction(result.access_token));
        dispatch(setIsAuthenticatedAction(true));
        dispatch(setIsFetching(false));
    } else if (response.status === 403) {
        dispatch(setIsFetching(false));
        return "Wrong email or password";
    } else {
        console.error("Internal error in login!");
    }
};

export const register = (data) => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await authAPI.fetchRegister(data);
    if (response.status === 201) {
        dispatch(setIsFetching(false));
    } else if (response.status === 403) {
        dispatch(setIsFetching(false));
        return "Email is already in use";
    } else {
        console.error("Internal error in login!");
    }
};

export const updateEmail = (email) => (dispatch) => {
    dispatch(updateEmailAction(email));
};

export const updatePassword = (password) => (dispatch) => {
    dispatch(updatePasswordAction(password));
};

export const updateIsRememberMe = (isRememberMe) => (dispatch) => {
    dispatch(updateIsRememberMeAction(isRememberMe));
};

export const updateIsRegisterTab = (isRegisterTab) => (dispatch) => {
    dispatch(updateIsRegisterTabAction(isRegisterTab));
};

export default authReducer;