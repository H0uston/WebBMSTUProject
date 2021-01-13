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
                        <h2>{props.productRating ? props.productRating : "n/a"}</h2>
                    </div>
                    : ""
                }
                { props.productDiscount ?
                    <div className={styles.ProductDiscount}>
                        <img src={percentIcon} alt={""}/>
                        <h2>{props.productDiscount}</h2>
                    </div>
                    :
                    ""
                }
            </div>
            <div className={styles.BottomGear}>
                <div className={styles.ProductTitle}>
                    <h3>{props.productName}</h3>
                </div>
                <div className={styles.ProductPrice}>
                    <h2>{props.productPrice}руб</h2>
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