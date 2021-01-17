export const SET_TOKEN = "SET-TOKEN";
export const SET_IS_AUTHENTICATED = "SET-IS-AUTHENTICATED";
export const UPDATE_EMAIL = "UPDATE-EMAIL";
export const UPDATE_PASSWORD = "UPDATE-PASSWORD";
export const UPDATE_IS_REMEMBER_ME = "UPDATE-IS-REMEMBER-ME";
export const UPDATE_IS_REGISTER_TAB = "UPDATE-IS-REGISTER-TAB";
export const SET_USER_ID = "SET-USER-ID";

export const setIsAuthenticatedAction = () => ({
    type: SET_IS_AUTHENTICATED
});

export const setTokenAction = (token) => ({
    type: SET_TOKEN, token
});

export const setUserIdAction = (userId) => ({
    type: SET_USER_ID, userId
});

export const updateEmailAction = (email) => ({
    type: UPDATE_EMAIL, email
});

export const updatePasswordAction = (password) => ({
    type: UPDATE_PASSWORD, password
});

export const updateIsRememberMeAction = (isRememberMe) => ({
    type: UPDATE_IS_REMEMBER_ME, isRememberMe
});

export const updateIsRegisterTabAction = (isRegisterTab) => ({
    type: UPDATE_IS_REGISTER_TAB, isRegisterTab
});