import React from "react";
import styles from "./Review.module.css";
import Rating from "@material-ui/lab/Rating";

const Review = (props) => {
    return (
        <div className={styles.reviewContainer}>
            <div className={styles.reviewContent}>
                <div className={styles.reviewHeader}>
                    <div className={styles.reviewName}>
                        {
                            props.user.userSurname ?
                                <div>
                                    {props.user.userSurname}
                                </div>
                                    :
                                ""
                        }
                        {
                            props.user.userName ?
                                <div>
                                    {props.user.userName}
                                </div>
                                :
                                ""
                        }
                        { props.user.userName == undefined && props.user.userSurname == undefined && "Аноним"}
                    </div>
                    <div className={styles.reviewRating}>
                        <Rating value={props.reviewRating} readOnly={true}/>
                    </div>
                </div>
                <textarea className={styles.reviewTextArea} value={props.reviewText} readOnly={true}/>
            </div>
        </div>
    )
};

export default Review;