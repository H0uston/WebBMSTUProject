export const SET_PRODUCT = "SET-PRODUCT";
export const UPDATE_PRODUCT_RATING = "UPDATE-PRODUCT-RATING";

export const setProductAction = (product) => ({
    type: SET_PRODUCT, product
});

export const updateProductRating = (rating) => ({
    type: UPDATE_PRODUCT_RATING, rating
});