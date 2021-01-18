import React from "react";
import styles from "./ProductReview.module.css";
import UserReview from "./UserReview/UserReview";

const ProductReview = (props) => {
    debugger;
    return (
        <div className={styles.productReview}>
            { props.isAuthenticated &&
                <>
                    <div className={styles.title}>
                        {props.myReview ? "Редактировать отзыв" : "Добавить отзыв"}
                    </div>
                    <UserReview {...props}/>
                </>}
            <div className={styles.title}>
                { props.reviewsElements.length > 0 ?
                    "Отзывы" :
                    props.myReview ? "" : "Нет отзывов"
                }
            </div>
            <div>
                {props.reviewsElements}
            </div>
        </div>
    )
};

export default ProductReview;