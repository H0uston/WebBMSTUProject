import React from "react";
import styles from "./Auth.module.css";
import {Redirect} from "react-router-dom";
import Register from "./Register/Register";
import Login from "./Login/Login";

const Auth = (props) => {

    debugger;

    if (props.isAuthenticated) {
        return <Redirect to={"/"}/>
    }

    return (
        <div className={styles.center}>
            <div className={styles.authContent}>
                <div className={styles.login}>
                    <div className={styles.authTab} onClick={() => props.updateIsRegisterTab(false)}>Вход</div>
                </div>
                <div className={styles.register}>
                    <div className={styles.authTab} onClick={() => props.updateIsRegisterTab(true)}>Регистрация</div>
                </div>
                <div className={styles.formField}>
                    {props.isRegisterTab ?
                        <Register {...props}/>
                        :
                        <Login {...props}/>
                    }
                </div>
            </div>
        </div>
    );
};

export default Auth;