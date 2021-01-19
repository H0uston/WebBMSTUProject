export const getSearchStringSelector = (state) => {
    return state.searchPage.searchString;
};

export const getSizePageSelector = (state) => {
    return state.searchPage.sizePage;
};

export const getProductsSelector = (state) => {
    return state.searchPage.products;
};

export const getCurrentPageSelector = (state) => {
    return state.searchPage.currentPage;
};

export const getCurrentSearchResultSelector = (state) => {
    return state.searchPage.currentSearchString;
};

export const getDefaultCountOfProductsSelector = (state) => {
    return state.searchPage.defaultCountOfProducts;
};