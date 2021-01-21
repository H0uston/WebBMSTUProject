import {FORM_ERROR} from "final-form";
import {Form} from "react-final-form";
import styles from "./Account.module.css";
import FormField from "../common/FormField/FormField";
import React from "react";

const isNumber = (value) => {
    return isNaN(+value) && value !== null && value !== undefined ? "Некорректное значение" : undefined;
};

const EditInfoForm = (props) => {
    let submit = async (data) => {
        let errorText = await props.editAccountInfo(props.userId, props.token, data);
        if (errorText) {
            return { [FORM_ERROR]: errorText };
        }
    };

    return (
        <Form
            onSubmit={submit}
            render={({handleSubmit, form, submitting, pristine, values, submitError, submitSucceeded}) => (
                <form className={styles.form} onSubmit={handleSubmit}>
                    <FormField name={"userSurname"} defaultValue={props.surname} errorStyle={styles.error}
                               formFieldStyle={styles.formField} inputStyle={styles.field} inputType={"text"}
                               placeholder={"Введите фамилию"} labelText={"Фамилия"}/>
                    <FormField name={"userName"} defaultValue={props.name} errorStyle={styles.error}
                               formFieldStyle={styles.formField} inputStyle={styles.field} inputType={"text"}
                               placeholder={"Введите имя"} labelText={"Имя"}/>
                    <FormField name={"userCity"} defaultValue={props.city} errorStyle={styles.error}
                               formFieldStyle={styles.formField} inputStyle={styles.field} inputType={"text"}
                               placeholder={"Введите город"} labelText={"Город"}/>
                    <FormField name={"userStreet"} defaultValue={props.street} errorStyle={styles.error}
                               formFieldStyle={styles.formField} inputStyle={styles.field} inputType={"text"}
                               placeholder={"Введите улицу"} labelText={"Улица"}/>
                    <FormField name={"userHouse"} defaultValue={props.house} errorStyle={styles.error}
                               formFieldStyle={styles.formField} inputStyle={styles.field} inputType={"text"}
                               placeholder={"Введите дом"} labelText={"Дом"}/>
                    <FormField name={"userFlat"} defaultValue={props.flat} errorStyle={styles.error}
                               formFieldStyle={styles.formField} inputStyle={styles.field} inputType={"text"}
                               placeholder={"Введите квартиру"} labelText={"Квартира"}/>
                    <FormField name={"userIndex"} defaultValue={props.index} errorStyle={styles.error}
                               formFieldStyle={styles.formField} inputStyle={styles.field} inputType={"text"}
                               placeholder={"Введите индекс"} labelText={"Индекс"} validate={isNumber}/>
                    {submitError &&  <div className={styles.error}>{submitError}</div>}
                    {submitSucceeded &&  <div className={styles.success}>{"Изменения сохранены!"}</div>}
                    <div className={styles.formButton}>
                        <button className={styles.button} type={"submit"} disabled={props.isFetching}>Сохранить</button>
                    </div>
                </form>
            )}/>
    )
};

export default EditInfoForm;