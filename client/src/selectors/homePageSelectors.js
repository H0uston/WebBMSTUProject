export const getCategoriesSelector = (state) => {
    return state.homePage.categories;
};

export const getProductsSelector = (state) => {
    return state.homePage.products;
};

export const getCountOfCategories = (state) => {
    return state.homePage.countOfCategories;
};