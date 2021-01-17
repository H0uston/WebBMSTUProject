export const getTokenSelector = (state) => {
    return state.authInfo.token;
};

export const getUserIdSelector = (state) => {
    return state.authInfo.userId;
};

export const getIsAuthenticatedSelector = (state) => {
    return state.authInfo.isAuthenticated;
};

export const getEmailSelector = (state) => {
    return state.authInfo.email;
};

export const getPasswordSelector = (state) => {
    return state.authInfo.password;
};

export const getIsRememberMeSelector = (state) => {
    return state.authInfo.isAuthenticated;
};

export const getIsRegisterTabSelector = (state) => {
    return state.authInfo.isRegisterTab;
};
