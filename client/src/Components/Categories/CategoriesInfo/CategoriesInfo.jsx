import React from "react";
import styles from "./CategoriesInfo.module.css";

const CategoriesInfo = (props) => {
    return (
        <div className={styles.categoriesContainer}>
            <div className={styles.title}>
                Категории
            </div>
            <div className={styles.categoriesSection}>
                {props.categories}
            </div>
        </div>
    )
};

export default CategoriesInfo;