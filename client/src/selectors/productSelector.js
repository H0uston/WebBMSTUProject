export const getProductSelector = (state) => {
    return state.productPage.product;
};

export const getDefaultCountOfProductsSelector = (state) => {
    return state.productPage.defaultCountOfProducts;
};