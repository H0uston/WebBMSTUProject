export const UPDATE_SEARCH_STRING = "UPDATE-SEARCH-STRING";
export const SET_PRODUCTS = "SET-PRODUCTS";
export const SET_CURRENT_SEARCH_STRING = "SET-CURRENT-SEARCH-STRING";

export const updateSearchStringAction = (searchString) => ({
    type: UPDATE_SEARCH_STRING, searchString
});

export const setProductsAction = (products) => ({
    type: SET_PRODUCTS, products
});

export const setCurrentSearchStringAction = (currentSearchString) => ({
    type: SET_CURRENT_SEARCH_STRING, currentSearchString
});