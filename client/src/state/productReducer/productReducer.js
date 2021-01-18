import {productAPI} from "../../API/productAPI";
import {SET_PRODUCT, setProductAction, UPDATE_PRODUCT_RATING} from "./productAction";

let initialState = {
    product: null,
    defaultCountOfProducts: 1
};

const productReducer = (state=initialState, action) => {
    let stateCopy = {...state};
    switch (action.type) {
        case (SET_PRODUCT):
            stateCopy.product = action.product;
            break;
        case (UPDATE_PRODUCT_RATING):
            stateCopy.product = {...stateCopy.product};
            stateCopy.product.productRating = action.rating;
            break;
        default:
            break;
    }

    return stateCopy;
};

export const fetchProduct = (productId) => async (dispatch) => {
    let response = await productAPI.fetchProduct(productId);
    if (response.status === 200) {
        let result = await response.json();
        dispatch(setProductAction(result));
    } else {
        console.error("Error while fetching product");
    }
};

export const setProduct = (product) => (dispatch) => {
    dispatch(setProductAction(product));
};

export default productReducer;