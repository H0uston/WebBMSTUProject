import React, {useState} from "react";
import styles from "./Card.module.css";

import noPhoto from "../../../assets/images/no_product_photo.png";
import starIcon from "../../../assets/images/star.svg";
import percentIcon from "../../../assets/images/percent.svg";
import minusIcon from "../../../assets/images/minus.svg";
import plusIcon from "../../../assets/images/plus.svg";
import Incrementer from "../incrementer/Incrementer";
import {NavLink, useHistory} from "react-router-dom";
import fetchData from "../../../API/fetchData";

const Card = (props) => {
    let [currentCount, saveCurrentCount] = useState(props.defaultCountOfProducts);
    const history = useHistory();

    let addToCart = async () => {
        await props.addProductToCart(props.productId, currentCount, props.token);
        history.push("/cart");
    };

    return (
        <div className={styles.ProductCard}>
            <div className={styles.ProductImgContainer}>
                <NavLink to={`/product/${props.productId}`}>
                    <img className={styles.photo} src={props.productPhotoPath ? fetchData.domainURL + props.productPhotoPath : noPhoto} alt={""}/>
                </NavLink>
                {props.productRating ?
                    <div className={styles.ProductRating}>
                        <img src={starIcon} alt={""}/>
                        <div className={styles.productRatingTitle}>{props.productRating ? props.productRating.toFixed(1) : "n/a"}</div>
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
                <Incrementer count={props.defaultCountOfProducts} saveCount={saveCurrentCount}/>
                <div className={styles.ToCartButton}>
                    <button disabled={!props.token} onClick={() => addToCart()}>В корзину</button>
                </div>
            </div>
        </div>
    )
};

export default Card;