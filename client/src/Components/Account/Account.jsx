import React from "react";
import styles from "./Account.module.css";
import EditInfoForm from "./EditInfoForm";
import EditPasswordForm from "./EditPasswordForm";

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