import {
    SET_CATEGORIES,
    SET_CATEGORY,
    SET_PRODUCTS,
    setCategoriesAction, setCategoryAction,
    setProductsAction,
    UNSET_CATEGORY, unsetCategoryAction
} from "./categoriesAction";
import {setIsFetching} from "../fetchingReducer/fetchingAction";
import {categoryAPI} from "../../API/categoryAPI";
import {productAPI} from "../../API/productAPI";

let initialState = {
    products: null,
    categories: null,
    chosenCategory: null,
    defaultCountOfProducts: 1
};

const categoriesReducer = (state = initialState, action) => {
    let stateCopy = {...state};

    switch (action.type) {
        case (SET_PRODUCTS):
            stateCopy.products = action.products;
            break;
        case (SET_CATEGORIES):
            stateCopy.categories = action.categories;
            break;
        case (SET_CATEGORY):
            stateCopy.chosenCategory = action.categoryId;
            break;
        case (UNSET_CATEGORY):
            stateCopy.chosenCategory = null;
            break;
        default:
            break;
    }

    return stateCopy;
};

export const fetchCategories = () => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await categoryAPI.fetchAll();
    if (response.status === 200) {
        let categories = await response.json();

        response = await productAPI.fetchAll();
        if (response.status === 200) {
            let products = await response.json();
            dispatch(setCategoriesAction(categories));
            dispatch(setProductsAction(products));
            dispatch(setIsFetching(false));
        } else {
            console.error("Error while fetching categories");
        }
    } else {
        throw Error(response.statusText);
    }
};

export const setCategory = (categoryId) => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await categoryAPI.fetchCategory(categoryId);
    if (response.status === 200) {
        let categories = await response.json();
        let products = [];

        for (let categoryCollection of categories.categoryCollection) {
            let resp = await productAPI.fetchProduct(categoryCollection.productId);

            if (resp.status === 200) {
                let product = await resp.json();
                products.push(product);
            } else {
                throw Error(response.statusText);
            }
        }
        dispatch(setProductsAction(products));
        dispatch(setCategoryAction(categoryId));
        dispatch(setIsFetching(false));

    } else {
        throw Error(response.statusText);
    }
};

export const unsetCategory = () => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await productAPI.fetchAll();
    if (response.status === 200) {
        let products = await response.json();
        dispatch(setProductsAction(products));
        dispatch(unsetCategoryAction());
        dispatch(setIsFetching(false));
    } else {
        console.error("Error while fetching categories");
    }
};

export default categoriesReducer;