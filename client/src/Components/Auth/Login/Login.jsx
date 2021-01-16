import React from "react";
import {Field, Form} from "react-final-form";
import styles from "./Login.module.css"
import {FORM_ERROR} from "final-form";

const required = (value) => {
    return value ? undefined : 'Обязательное поле'
};

const LoginForm = (props) => {
    let submit = async (data) => {
        let errorText = await props.login(data);
        if (errorText) {
            return { [FORM_ERROR]: errorText };
        }
    };

    return (
        <Form
            onSubmit={submit}
            render={({handleSubmit, form, submitting, pristine, values, submitError}) => (
                <form className={styles.form} onSubmit={handleSubmit}>
                    <Field name={"email"} validate={required}>
                        {({input, meta}) => (
                            <div className={styles.formField}>
                                <label>Электронная почта</label>
                                <input {...input} type={"text"}
                                       className={styles.field}
                                       placeholder={"Введите электронную почту"}/>
                                {meta.error && meta.touched && <div className={styles.error}>{meta.error}</div>}
                            </div>
                        )}
                    </Field>
                    <Field name={"password"} validate={required}>
                        {({input, meta}) => (
                            <div className={styles.formField}>
                                <label>Пароль</label>
                                <input {...input} type={"password"}
                                       className={styles.field}
                                       placeholder={"Введите пароль"}/>
                                {meta.error && meta.touched && <div className={styles.error}>{meta.error}</div>}
                            </div>
                        )}
                    </Field>
                    <Field name={"isRememberMe"} type={"checkbox"}>
                        {({input, meta}) => (
                            <div className={styles.rememberMe}>
                                <label>Запомнить меня</label>
                                <input {...input} type={"checkbox"}
                                       className={styles.field}/>
                            </div>
                        )}
                    </Field>
                    {submitError &&  <div className={styles.error}>{submitError}</div>}
                    <div className={styles.formButton}>
                        <button className={styles.button} type={"submit"} disabled={props.isFetching}>Войти</button>
                    </div>
                </form>
            )}/>
    )
};

const Login = (props) => {
    return (
        <LoginForm {...props}/>
    )
};

export default Login;