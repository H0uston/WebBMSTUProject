import React from "react";
import {Form} from "react-final-form";
import styles from "./Login.module.css"
import {FORM_ERROR} from "final-form";
import FormField from "../../common/FormField/FormField";

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
                    <FormField name={"email"} validate={required} inputType={"text"} labelText={"Электронная почта"}
                               placeholder={"Введите электронную почту"} errorStyle={styles.error} inputStyle={styles.field}
                               formFieldStyle={styles.formField}  />
                    <FormField name={"password"} validate={required} inputType={"password"} labelText={"Пароль"}
                               placeholder={"Введите пароль"} errorStyle={styles.error} inputStyle={styles.field}
                               formFieldStyle={styles.formField} />
                    <FormField name={"isRememberMe"} inputType={"checkbox"} labelText={"Запомнить меня"}
                               inputStyle={styles.field} formFieldStyle={styles.rememberMe} />
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