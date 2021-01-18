import React from "react";
import styles from "./UserReview.module.css";
import Rating from "@material-ui/lab/Rating";
import FunctionalButton from "../../../common/buttons/FunctionalButton";

const UserReview = (props) => {
    debugger;
    return (
        <div className={styles.userReviewContainer}>
            <div className={styles.reviewContent}>
                <textarea className={styles.reviewTextArea} placeholder={"Введите текст отзыва..."} value={props.reviewText}
                    onChange={(e) => props.updateReviewText(e.currentTarget.value)}/>
                <div className={styles.reviewFooter}>
                    <div>
                        <div className={styles.miniTitle}>
                            Рейтинг продукта
                        </div>
                        <Rating value={props.reviewRating} onChange={(e) => props.updateReviewRating(e.currentTarget.value)} size={"large"}/>
                    </div>
                    {
                        props.myReview ?
                            <div className={styles.editReview}>
                                <FunctionalButton
                                    text={"Изменить отзыв"}
                                    onClick={() => props.changeReview(
                                        props.productId,
                                        props.reviewText,
                                        props.reviewRating,
                                        props.token
                                    )}
                                />
                                <FunctionalButton
                                    text={"Удалить отзыв"}
                                    onClick={() => props.deleteReview(
                                        props.myReview.reviewId,
                                        props.productId,
                                        props.token
                                    )}
                                />
                            </div>
                            :
                            <FunctionalButton
                                text={"Добавить отзыв"}
                                onClick={() => props.createReview(props.productId, props.reviewText, props.reviewRating, props.token)}
                            />
                    }
                </div>
            </div>
        </div>
    )
};

export default UserReview;