import {FORM_ERROR} from "final-form";
import {Form} from "react-final-form";
import styles from "./Account.module.css";
import FormField from "../common/FormField/FormField";
import React from "react";

const EditPasswordForm = (props) => {
    let submit = async (data) => {
        let errorText = await props.editAccountInfo(props.userId, props.token, data);
        if (errorText) {
            return { [FORM_ERROR]: errorText };
        }
    };

    return (
        <Form
            onSubmit={submit}
            validate={values => {
                const errors = {};
                if (values.userPassword !== values.checkPassword) {
                    errors.checkPassword = 'Неправильно повторён пароль';
                }

                return errors;
            }}
            render={({handleSubmit, form, submitting, pristine, values, submitError, submitSucceeded}) => (
                <form className={styles.form} onSubmit={handleSubmit}>
                    <FormField name={"userPassword"} errorStyle={styles.error}
                               formFieldStyle={styles.formField} inputStyle={styles.field} inputType={"password"}
                               placeholder={"Введите пароль"} labelText={"Введите пароль"}/>
                    <FormField name={"checkPassword"} errorStyle={styles.error}
                               formFieldStyle={styles.formField} inputStyle={styles.field} inputType={"password"}
                               placeholder={"Повторите пароль"} labelText={"Повторите пароль"}/>
                    {submitError &&  <div className={styles.error}>{submitError}</div>}
                    {submitSucceeded &&  <div className={styles.success}>{"Изменения сохранены!"}</div>}
                    <div className={styles.formButton}>
                        <button className={styles.button} type={"submit"} disabled={props.isFetching}>Сохранить</button>
                    </div>
                </form>
            )}/>
    )
};

export default EditPasswordForm;