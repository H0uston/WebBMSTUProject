import React from "react";
import styles from "./Product.module.css";
import ProductInfo from "./ProductInfo/ProductInfo";
import ProductReviewContainer from "./ProductReview/ProductReviewContainer";

const Product = (props) => {
    return (
        <div className={styles.productContainer}>
            <ProductInfo {...props.product} {...props}/>
            <ProductReviewContainer token={props.token} productId={props.product.productId} />
        </div>
    )
};

export default Product;