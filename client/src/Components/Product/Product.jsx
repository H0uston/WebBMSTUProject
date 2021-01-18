import React from "react";
import styles from "./Product.module.css";
import ProductInfo from "./ProductInfo/ProductInfo";

const Product = (props) => {
    return (
        <div className={styles.productContainer}>
            <ProductInfo {...props.product} {...props}/>
            <div>
                Комментарии отзывы
            </div>
        </div>
    )
};

export default Product;