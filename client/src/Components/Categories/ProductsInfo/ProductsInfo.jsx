import React from "react";
import styles from "./ProductsInfo.module.css";

const ProductsInfo = (props) => {
    return (
        <div className={styles.productsInfoContainer}>
            <div className={styles.title}>
                Выбранная категория: { props.currentCategory ? props.currentCategory.categoryName : "Все" }
            </div>
            <div className={styles.productSection}>
                {props.cards}
            </div>
        </div>
    )
};

export default ProductsInfo;