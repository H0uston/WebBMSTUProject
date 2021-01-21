import React from "react";
import {Form} from "react-final-form";
import styles from "./Register.module.css"
import {FORM_ERROR} from "final-form";
import {validateEmail} from "../../../utils/regEx";
import FormField from "../../common/FormField/FormField";

const required = (value) => {
    return value ? undefined : 'Обязательное поле'
};

const checkEmail = (value) => {
    return validateEmail(value) ? undefined : "Некорректная почта"
};

const checkPassword = (value) => {
    return value.length >= 8 ? undefined : "Длина пароля должна быть 8 и более символов";
};

const composeValidators = (...validators) => value =>
    validators.reduce((error, validator) => error || validator(value), undefined);

const RegisterForm = (props) => {
    let submit = async (data) => {
        let errorText = await props.register(data);
        if (errorText) {
            return { [FORM_ERROR]: errorText };
        }
    };

    return (
        <Form
            onSubmit={submit}
            validate={values => {
                const errors = {};
                if (values.password !== values.check_password) {
                    errors.check_password = 'Неправильно повторён пароль';
                }

                return errors;
            }}
            render={({handleSubmit, form, submitting, pristine, values, submitError, submitSucceeded}) => (
                <form className={styles.form} onSubmit={handleSubmit}>
                    <FormField name={"email"} validate={composeValidators(required, checkEmail)}
                               formFieldStyle={styles.formField} placeholder={"Введите электронную почту"}
                               inputStyle={styles.field} errorStyle={styles.error} labelText={"Электронная почта"}
                               inputType={"text"}/>
                    <FormField name={"password"} validate={composeValidators(required, checkPassword)}
                               formFieldStyle={styles.formField} placeholder={"Введите пароль"}
                               inputStyle={styles.field} errorStyle={styles.error} labelText={"Пароль"}
                               inputType={"text"}/>
                    <FormField name={"check_password"} validate={required}
                               formFieldStyle={styles.formField} placeholder={"Повторите пароль"}
                               inputStyle={styles.field} errorStyle={styles.error} labelText={"Повторите пароль"}
                               inputType={"text"}/>
                    {submitError &&  <div className={styles.error}>{submitError}</div>}
                    {submitSucceeded &&  <div className={styles.success}>{"Успешная регистрация"}</div>}
                    <div className={styles.formButton}>
                        <button className={styles.button} type={"submit"}
                                disabled={props.isFetching}>Зарегистрироваться</button>
                    </div>
                </form>
            )}/>
    )
};

const Register = (props) => {
    return (
        <RegisterForm {...props}/>
    )
};

export default Register;