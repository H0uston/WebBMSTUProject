export const getProductsSelector = (state) => {
    return state.categoriesPage.products;
};

export const getCategoriesSelector = (state) => {
    return state.categoriesPage.categories;
};

export const getChosenCategorySelector = (state) => {
    return state.categoriesPage.chosenCategory;
};

export const getDefaultCountOfProducts = (state) => {
    return state.categoriesPage.defaultCountOfProducts;
};

