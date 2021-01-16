import React from "react";
import styles from "./Card.module.css";

import noPhoto from "../../../assets/images/no_product_photo.png";
import starIcon from "../../../assets/images/star.svg";
import percentIcon from "../../../assets/images/percent.svg";
import minusIcon from "../../../assets/images/minus.svg";
import plusIcon from "../../../assets/images/plus.svg";

const Card = (props) => {
    // TODO изменить роут

    debugger;
    return (
        <div className={styles.ProductCard}>
            <div className={styles.ProductImgContainer}>
                <img src={props.productPhotoPath ? "http://localhost:443/" + props.productPhotoPath : noPhoto} alt={""}/>
                {props.productRating ?
                    <div className={styles.ProductRating}>
                        <img src={starIcon} alt={""}/>
                        <div className={styles.productRatingTitle}>{props.productRating ? props.productRating : "n/a"}</div>
                    </div>
                    : ""
                }
                { props.productDiscount ?
                    <div className={styles.ProductDiscount}>
                        <img src={percentIcon} alt={""}/>
                        <div className={styles.productDiscountTitle}>{props.productDiscount}</div>
                    </div>
                    :
                    ""
                }
            </div>
            <div className={styles.BottomGear}>
                <div className={styles.ProductTitle}>
                    <div className={styles.productName}>{props.productName}</div>
                </div>
                <div>
                    {
                        props.productDiscount ?
                            <div className={styles.productNameWithDiscount}>
                                <div className={styles.productNameOldPrice}>{props.productPrice} руб.</div>
                                <div className={styles.productNameNewPrice}>{props.productPriceWithDiscount} руб.</div>
                            </div>
                            :
                            <div className={styles.productPrice}>{props.productPrice} руб.</div>
                    }
                </div>
                <div className={styles.ProductAmount}>
                    <button>
                        <img src={minusIcon} alt={""}/>
                    </button>
                    <input />
                    <button>
                        <img src={plusIcon} alt={""} />
                    </button>
                </div>
                <div className={styles.ToCartButton}>
                    <button>В корзину</button>
                </div>
            </div>
        </div>
    )
};

export default Card;