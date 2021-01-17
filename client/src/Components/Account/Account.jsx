import React from "react";
import styles from "./Account.module.css";
import {FORM_ERROR} from "final-form";
import {Field, Form} from "react-final-form";

const isNumber = (value) => isNaN(+value) && value !== null ? "Некорректное значение" : undefined;

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
                    <Field name={"userSurname"} defaultValue={props.surname}>
                        {({input, meta}) => (
                            <div className={styles.formField}>
                                <label>Фамилия</label>
                                <input {...input} type={"text"}
                                       className={styles.field}
                                       placeholder={"Введите фамилию"}/>
                                {meta.error && meta.touched && <div className={styles.error}>{meta.error}</div>}
                            </div>
                        )}
                    </Field>
                    <Field name={"userName"} initialValue={props.name}>
                        {({input, meta}) => (
                            <div className={styles.formField}>
                                <label>Имя</label>
                                <input {...input} type={"text"}
                                       className={styles.field}
                                       placeholder={"Введите имя"}/>
                                {meta.error && meta.touched && <div className={styles.error}>{meta.error}</div>}
                            </div>
                        )}
                    </Field>
                    <Field name={"userCity"} initialValue={props.city}>
                        {({input, meta}) => (
                            <div className={styles.formField}>
                                <label>Город</label>
                                <input {...input} type={"text"}
                                       className={styles.field}
                                       placeholder={"Введите город"}/>
                                {meta.error && meta.touched && <div className={styles.error}>{meta.error}</div>}
                            </div>
                        )}
                    </Field>
                    <Field name={"userStreet"} initialValue={props.street}>
                        {({input, meta}) => (
                            <div className={styles.formField}>
                                <label>Улица</label>
                                <input {...input} type={"text"}
                                       className={styles.field}
                                       placeholder={"Введите улица"}/>
                                {meta.error && meta.touched && <div className={styles.error}>{meta.error}</div>}
                            </div>
                        )}
                    </Field>
                    <Field name={"userHouse"} initialValue={props.flat}>
                        {({input, meta}) => (
                            <div className={styles.formField}>
                                <label>Дом</label>
                                <input {...input} type={"text"}
                                       className={styles.field}
                                       placeholder={"Введите дом"}
                                       value={props.house}/>
                                {meta.error && meta.touched && <div className={styles.error}>{meta.error}</div>}
                            </div>
                        )}
                    </Field>
                    <Field name={"userFlat"} initialValue={props.flat}>
                        {({input, meta}) => (
                            <div className={styles.formField}>
                                <label>Квартира</label>
                                <input {...input} type={"text"}
                                       className={styles.field}
                                       placeholder={"Введите квартиру"}/>
                                {meta.error && meta.touched && <div className={styles.error}>{meta.error}</div>}
                            </div>
                        )}
                    </Field>
                    <Field name={"userIndex"} validate={isNumber} initialValue={props.index}>
                        {({input, meta}) => (
                            <div className={styles.formField}>
                                <label>Индекс</label>
                                <input {...input} type={"text"}
                                       className={styles.field}
                                       placeholder={"Введите индекс"}/>
                                {meta.error && meta.touched && <div className={styles.error}>{meta.error}</div>}
                            </div>
                        )}
                    </Field>
                    {submitError &&  <div className={styles.error}>{submitError}</div>}
                    {submitSucceeded &&  <div className={styles.success}>{"Изменения сохранены!"}</div>}
                    <div className={styles.formButton}>
                        <button className={styles.button} type={"submit"} disabled={props.isFetching}>Сохранить</button>
                    </div>
                </form>
            )}/>
    )
};

const EditPasswordForm = (props) => {
    let submit = async (data) => {
        debugger;
        let errorText = await props.editAccountInfo(props.userId, props.token, data);
        if (errorText) {
            return { [FORM_ERROR]: errorText };
        }
    };

    return (
        <Form
            onSubmit={submit}
            validate={values => {
                debugger;
                const errors = {};
                if (values.userPassword !== values.checkPassword) {
                    errors.checkPassword = 'Неправильно повторён пароль';
                }

                return errors;
            }}
            render={({handleSubmit, form, submitting, pristine, values, submitError, submitSucceeded}) => (
                <form className={styles.form} onSubmit={handleSubmit}>
                    <Field name={"userPassword"}>
                        {({input, meta}) => (
                            <div className={styles.formField}>
                                <label>Пароль</label>
                                <input {...input} type={"text"}
                                       className={styles.field}
                                       placeholder={"Введите пароль"}/>
                                {meta.error && meta.touched && <div className={styles.error}>{meta.error}</div>}
                            </div>
                        )}
                    </Field>
                    <Field name={"checkPassword"}>
                        {({input, meta}) => (
                            <div className={styles.formField}>
                                <label>Повторите пароль</label>
                                <input {...input} type={"text"}
                                       className={styles.field}
                                       placeholder={"Повторите пароль"}/>
                                {meta.error && meta.touched && <div className={styles.error}>{meta.error}</div>}
                            </div>
                        )}
                    </Field>
                    {submitError &&  <div className={styles.error}>{submitError}</div>}
                    {submitSucceeded &&  <div className={styles.success}>{"Изменения сохранены!"}</div>}
                    <div className={styles.formButton}>
                        <button className={styles.button} type={"submit"} disabled={props.isFetching}>Сохранить</button>
                    </div>
                </form>
            )}/>
    )
};

const Account = (props) => {
    return (
        <div className={styles.accountContent}>
            <div>
                <div className={styles.mainTitle}>
                    Редактировать информацию
                </div>
                <EditInfoForm {...props} />
                <div className={styles.mainTitle}>
                    Изменение пароля
                </div>
                <EditPasswordForm {...props}/>
            </div>
        </div>
    )
};

export default Account;