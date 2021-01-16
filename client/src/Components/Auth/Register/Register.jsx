import React from "react";
import {Field, Form} from "react-final-form";
import styles from "./Register.module.css"
import {FORM_ERROR} from "final-form";

const required = (value) => {
    return value ? undefined : 'Обязательное поле'
};

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
                    <Field name={"check_password"} validate={required}>
                        {({input, meta}) => (
                            <div className={styles.formField}>
                                <label>Повторите пароль</label>
                                <input {...input} type={"password"}
                                       className={styles.field}
                                       placeholder={"Повторите пароль"}/>
                                {meta.error && meta.touched && <div className={styles.error}>{meta.error}</div>}
                            </div>
                        )}
                    </Field>
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