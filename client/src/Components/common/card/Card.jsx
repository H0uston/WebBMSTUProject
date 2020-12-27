import React from "react";
import styles from "./Card.module.css";

const Card = (props) => {
    return (
        <div className={styles.cardContent}>
            Название продукта тут и всякое
        </div>
    )
};

export default Card;