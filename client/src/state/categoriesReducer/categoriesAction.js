export const SET_CATEGORY = "SET-CATEGORY";
export const UNSET_CATEGORY = "UNSET-CATEGORY";
export const SET_PRODUCTS = "SET-PRODUCTS";
export const SET_CATEGORIES = "SET-CATEGORIES";

export const setCategoryAction = (categoryId) => ({
    type: SET_CATEGORY, categoryId
});

export const unsetCategoryAction = () => ({
    type: UNSET_CATEGORY
});

export const setProductsAction = (products) => ({
    type: SET_PRODUCTS, products
});

export const setCategoriesAction = (categories) => ({
    type: SET_CATEGORIES, categories
});
