export const ADD_PRODUCT = "ADD-PRODUCT";
export const DELETE_PRODUCT = "DELETE-PRODUCT";
export const CHANGE_COUNT = "CHANGE-COUNT";
export const SET_CART = "SET-CART";

export const addProductAction = (productId, count) => ({
    type: ADD_PRODUCT, productId, count
});

export const deleteProductAction = (productInCartId) => ({
    type: DELETE_PRODUCT, productInCartId
});

export const changeCountAction = (productInCartId, count) => ({
    type: CHANGE_COUNT, productInCartId, count
});

export const setCartAction = (products) => ({
    type: SET_CART, products
});