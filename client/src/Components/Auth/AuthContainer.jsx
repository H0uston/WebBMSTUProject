import React from "react";
import {
    getEmailSelector,
    getIsAuthenticatedSelector,
    getIsRegisterTabSelector,
    getIsRememberMeSelector,
    getPasswordSelector
} from "../../selectors/authSelectors";
import {connect} from "react-redux";
import {compose} from "redux";
import Auth from "./Auth";
import {
    login,
    register,
    updateEmail,
    updateIsRegisterTab,
    updateIsRememberMe,
    updatePassword
} from "../../state/authReducer/authReducer";

const AuthContainer = (props) => {

    return (
        <Auth {...props} />
    );
};

const mapStateToProps = (state) => ({
    email: getEmailSelector(state),
    password: getPasswordSelector(state),
    rememberMe: getIsRememberMeSelector(state),
    isRegisterTab: getIsRegisterTabSelector(state),
    isAuthenticated: getIsAuthenticatedSelector(state)
});

export default compose(connect(mapStateToProps, {
    updateEmail,
    updatePassword,
    updateIsRegisterTab,
    updateIsRememberMe,
    login,
    register
}))(AuthContainer);