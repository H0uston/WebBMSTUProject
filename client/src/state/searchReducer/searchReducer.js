import {
    SET_PRODUCTS,
    SET_CURRENT_SEARCH_STRING,
    setProductsAction,
    UPDATE_SEARCH_STRING,
    updateSearchStringAction, setCurrentSearchStringAction
} from "./searchAction";
import {setIsFetching} from "../fetchingReducer/fetchingAction";
import {productAPI} from "../../API/productAPI";

let initialState = {
    products: null,
    searchString: "",
    sizePage: 16,
    currentPage: 1,
    currentSearchString: "",
    defaultCountOfProducts: 1,
};

const searchReducer = (state=initialState, action) => {
    let stateCopy = {...state};

    switch (action.type) {
        case (SET_PRODUCTS):
            stateCopy.products = action.products;
            break;
        case (UPDATE_SEARCH_STRING):
            stateCopy.searchString = action.searchString;
            break;
        case (SET_CURRENT_SEARCH_STRING):
            stateCopy.currentSearchString = action.currentSearchString;
            break;
        default:
            break;
    }

    return stateCopy;
};

export const fetchProducts = (searchString, currentPage, sizePage) => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await productAPI.fetchAll(searchString, currentPage, sizePage);
    if (response.status === 200) {
        let result = await response.json();
        dispatch(setProductsAction(result));
        dispatch(setCurrentSearchStringAction(searchString));
        dispatch(updateSearchStringAction(""));
        dispatch(setIsFetching(false));
    } else {
        console.error("Error in fetch products!");
    }
};

export const updateSearchString = (searchString) => async (dispatch) => {
    dispatch(updateSearchStringAction(searchString));
};

export const setCurrentSearchString = (currentSearchString) => async (dispatch) => {
    dispatch(setCurrentSearchStringAction(currentSearchString));
};

export default searchReducer;