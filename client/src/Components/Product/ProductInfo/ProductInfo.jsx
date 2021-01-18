import React, {useState} from "react";
import styles from "./ProductInfo.module.css";
import Incrementer from "../../common/incrementer/Incrementer";
import FunctionalButton from "../../common/buttons/FunctionalButton";
import noProductPhoto from "../../../assets/images/no_product_photo.png";
import Rating from "@material-ui/lab/Rating";
import { useHistory } from "react-router-dom";

const ProductInfo = (props) => {
    let [currentCount, saveCurrentCount] = useState(props.defaultCountOfProducts);
    const history = useHistory();

    let addToCart = async () => {
        await props.addProductToCart(props.productId, currentCount, props.token);
        history.push("/cart");
    };

    return (
        <div className={styles.productContainer}>
            <div className={styles.topContainer}>
                <div>
                    {props.productName}
                </div>
                <div className={styles.ratingContainer}>
                    {!props.productRating ? "Нет оценок" : <Rating value={props.productRating}  readOnly={true} size={"large"} />}
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
                                {
                                    props.isAuthenticated ?
                                        <>
                                            <div>
                                                <Incrementer count={props.defaultCountOfProducts} saveCount={saveCurrentCount} />
                                            </div>
                                            <div>
                                                <FunctionalButton bgcolor={"orange"} text={"В корзину"} onClick={() => addToCart()}/>
                                            </div>
                                        </>
                                        :
                                        ""
                                }
                            </div>
                    }
                </div>
            </div>
        </div>
    );
};

export default ProductInfo;