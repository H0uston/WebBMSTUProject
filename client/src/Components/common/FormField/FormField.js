import React from "react";
import {Field} from "react-final-form";


const FormField = ({name, validate, defaultValue, formFieldStyle,
                       inputStyle, errorStyle, inputType, placeholder, labelText}) => {
    return (
        <Field name={name} validate={validate} defaultValue={defaultValue}>
            {({input, meta}) => (
                <div className={formFieldStyle}>
                    <label>{labelText}</label>
                    <input {...input} type={inputType}
                           className={inputStyle}
                           placeholder={placeholder}/>
                    {meta.error && meta.touched && <div className={errorStyle}>{meta.error}</div>}
                </div>
            )}
        </Field>
    )
};

export default FormField;