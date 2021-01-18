import {setIsFetching} from "../fetchingReducer/fetchingAction";
import {productAPI} from "../../API/productAPI";
import {SET_PRODUCT, setProductAction} from "./productAction";

let initialState = {
    product: null,
};

const productReducer = (state=initialState, action) => {
    let stateCopy = {...state};

    switch (action.type) {
        case (SET_PRODUCT):
            stateCopy.product = action.product;
            break;
        default:
            break;
    }

    return stateCopy;
};

export const fetchProduct = (productId) => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await productAPI.fetchProduct(productId);
    if (response.status === 200) {
        let result = await response.json();
        dispatch(setProductAction(result));
        dispatch(setIsFetching(false));
    } else {
        console.error("Error while fetching product");
    }
};

export default productReducer;