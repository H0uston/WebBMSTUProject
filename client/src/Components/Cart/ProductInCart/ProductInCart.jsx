import React from "react";
import styles from "./ProductInCart.module.css";
import noProduct from "../../../assets/images/no_product_photo.png";
import Incrementer from "../../common/incrementer/Incrementer";
import fetchData from "../../../API/fetchData";

const ProductInCart = (props) => {

    let saveCount = (count) => {
        props.changeProductCount(props.productInCartId, count, props.token);
    };

    return (
        <div className={styles.CartProduct}>
            <div className={styles.CartProductImg}>
                <img src={props.productPhotoPath ? fetchData.domainURL + props.productPhotoPath : noProduct} alt={""} />
            </div>
            <div className={styles.CartProductNameContainer}>
                <div className={styles.title}>Название продукта</div>
                <h2>{props.productName}</h2>
            </div>
            <div className={styles.CartProductPriceContainer}>
                <div className={styles.title}>Цена за штуку</div>
                <h2>{props.productPriceWithDiscount || props.productPrice} руб.</h2>
            </div>
            <div className={styles.incrementerContainer}>
                <Incrementer count={props.count} saveCount={saveCount}/>
            </div>
            <div className={styles.CartProductTotalAmountContainer}>
                <h1>{ (props.productPriceWithDiscount || props.productPrice) * props.count } руб.</h1>
            </div>

            <div className={styles.CartProductCloseButton}>
                <button onClick={() => props.deleteProduct(props.productInCartId, props.token)}>
                    <img src="https://www.flaticon.com/svg/static/icons/svg/1828/1828778.svg" />
                </button>
            </div>
        </div>
    )
};

export default ProductInCart;