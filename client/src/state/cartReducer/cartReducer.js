import {
    CHANGE_COUNT,
    changeCountAction,
    DELETE_PRODUCT,
    deleteProductAction,
    SET_CART,
    setCartAction
} from "./cartAction";
import {setIsFetching} from "../fetchingReducer/fetchingAction";
import {cartAPI} from "../../API/cartAPI";
import {productAPI} from "../../API/productAPI";
import {orderAPI} from "../../API/orderAPI";

let initialState = {
    products: null,
    finalPrice: 0,
};

const cartReducer = (state=initialState, action) => {
    let stateCopy = {...state};

    switch (action.type) {
        case (SET_CART):
            stateCopy.products = action.products;
            break;
        case (CHANGE_COUNT):
            stateCopy.products = [...stateCopy.products];
            let product = stateCopy.products.find(p => p.productInCartId === action.productInCartId);
            product.count = action.count;
            break;
        case (DELETE_PRODUCT):
            stateCopy.products = [...stateCopy.products];
            stateCopy.products = stateCopy.products.filter(p => p.productInCartId !== action.productInCartId);
            break;
        default:
            break;
    }

    return stateCopy;
};

export const addProductToCart = (productId, count, token) => async (dispatch) => {
    debugger;
    dispatch(setIsFetching(true));
    let response = await cartAPI.addProductToCart({productId, count}, token);
    if (response.status === 200) {
        dispatch(setIsFetching(false));
    } else if (response.status === 401) {
        console.error("Unauthorized!");
    } else if (response.status === 400) {
        console.error("Invalid count!");
    } else {
        console.error("Unknown error in add product to cart" + response.status);
    }
};

export const changeProductCount = (ordersId, count, token) => async (dispatch) => {
    let response = await cartAPI.changeCount({ordersId, count}, token);
    if (response.status === 204) {
        dispatch(changeCountAction(ordersId, count));
    } else if (response.status === 401) {
        console.error("Unauthorized!");
    } else {
        console.error("Unknown error in change product count" + response.status);
    }
};

export const deleteProduct = (productInCartId, token) => async (dispatch) => {
    debugger;
    dispatch(setIsFetching(true));
    let response = await cartAPI.deleteProduct(productInCartId, token);
    if (response.status === 204) {
        dispatch(deleteProductAction(productInCartId));
        dispatch(setIsFetching(false));
    } else if (response.status === 401) {
        console.error("Unauthorized!");
    } else {
        console.error("Unknown error in delete product" + response.status);
    }
};

export const fetchCart = (token) => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await cartAPI.fetchAll(token);
    if (response.status === 200) {
        let result = await response.json();
        let products = [];
        for (let productInCart of result) {
            let response = await productAPI.fetchProduct(productInCart.productId);
            let product = await response.json();
            product.productInCartId = productInCart.ordersId;
            product.count = productInCart.count;
            products.push(product);
        }
        dispatch(setCartAction(products));
        dispatch(setIsFetching(false));
    } else if (response.status === 401) {
        console.error("Unauthorized!");
    } else {
        console.error("Unknown error in fetch cart" + response.status);
    }
};

export const makeOrder = (token) => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await orderAPI.createOrder({}, token);
    if (response.status === 201) {
        dispatch(setCartAction([]));
        dispatch(setIsFetching(false));
    } else if (response.status === 401) {
        console.error("Unauthorized!");
    } else {
        console.error("Unknown error in fetch cart" + response.status);
    }
};

export default cartReducer;