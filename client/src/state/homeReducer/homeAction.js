export const SET_CATEGORIES = "SET-CATEGORIES";
export const SET_PRODUCTS = "SET-PRODUCTS";

export const setCategories = (categories) => ({
    type: SET_CATEGORIES, categories
});

export const setProducts = (products) => ({
    type: SET_PRODUCTS, products
});