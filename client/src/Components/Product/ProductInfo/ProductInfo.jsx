import React from "react";
import styles from "./ProductInfo.module.css";
import Incrementer from "../../common/incrementer/Incrementer";
import FunctionalButton from "../../common/buttons/FunctionalButton";
import noProductPhoto from "../../../assets/images/no_product_photo.png";
import Rating from "@material-ui/lab/Rating";

const ProductInfo = (props) => {
    return (
        <div className={styles.productContainer}>
            <div className={styles.topContainer}>
                <div>
                    {props.productName}
                </div>
                <div className={styles.ratingContainer}>
                    {!props.productRating ? "Нет оценок" : <Rating value={props.productRating}  readOnly={true} />}
                </div>
            </div>
            <div className={styles.infoContainer}>
                <div className={styles.imageContainer}>
                    <img src={props.productPhotoPath ? "http://localhost:443/" + props.productPhotoPath : noProductPhoto}  alt={""}/>
                </div>
                <div className={styles.productInfo}>
                    <div className={styles.title}>
                        Описание
                    </div>
                    <div>
                        {props.productAvailability ? "В наличии" : "Нет в наличии"}
                    </div>
                    {
                        props.productDiscount ?
                            <>
                                <div>
                                    На товар действует скидка {props.productDiscount}%.
                                </div>
                                <div>
                                    <div className={styles.oldPrice}>
                                        {props.productPrice} руб.
                                    </div>
                                    <div className={styles.newPrice}>
                                        {props.productPriceWithDiscount} руб.
                                    </div>
                                </div>
                            </>
                            :
                            <div className={styles.onePrice}>
                                {props.productPrice} руб.
                            </div>
                    }
                    {
                        props.isAuthenticated ?
                            <>
                                <div>
                                    <Incrementer count={1} saveCount={(count) => console.log(count)} />
                                </div>
                                <div>
                                    <FunctionalButton text={"В корзину"}/>
                                </div>
                            </>
                            :
                            ""
                    }
                </div>
            </div>
        </div>
    );
};

export default ProductInfo;