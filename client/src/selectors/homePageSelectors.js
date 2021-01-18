export const getCategoriesSelector = (state) => {
    return state.homePage.categories;
};

export const getProductsSelector = (state) => {
    return state.homePage.products;
};

export const getCountOfCategoriesSelector = (state) => {
    return state.homePage.countOfCategories;
};