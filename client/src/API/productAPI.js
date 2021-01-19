import fetchData from "./fetchData";

export const productAPI = {
    fetchAll: (productName, current, size) => {
        let url = fetchData.baseURL + "products";

        if (productName != null) {
            url += `?productName=${encodeURIComponent(productName)}`;
        }

        return fetch(url);
    },

    fetchProduct: (productId) => {
        return fetch(fetchData.baseURL + `products/${productId}`);
    }
};