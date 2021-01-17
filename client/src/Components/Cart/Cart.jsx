import React, {useState} from "react";
import styles from "./Cart.module.css";
import FunctionalButton from "../common/buttons/FunctionalButton";

const Cart = (props) => {

    return (
        <div className={styles.cartContainer}>
            <div className={styles.title}>
                Корзина
            </div>
            {
                props.productInCart.length > 0 ?
                    props.productInCart :
                    <div className={styles.emptyCart}>
                        Нет товаров, добавленных в корзину
                    </div>
            }
            <div className={styles.finalPrice}>
                Итого: {props.finalPrice} руб.
            </div>
            <div className={styles.makeOrder}>
                <FunctionalButton text={"Оформить заказ"} onClick={() => props.makeOrder(props.token)}/>
            </div>
        </div>
    )
};

export default Cart;