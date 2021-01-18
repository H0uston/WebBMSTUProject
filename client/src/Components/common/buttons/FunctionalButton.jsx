import React from "react";
import styles from "./Buttons.module.css";

const FunctionalButton = ({text, onClick, bgcolor}) => {
    return (
        <button style={{backgroundColor: bgcolor}} className={styles.button} onClick={onClick}>
            {text}
        </button>
    )
};

export default FunctionalButton;