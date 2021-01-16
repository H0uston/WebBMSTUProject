import fetchData from "./fetchData";

export const productAPI = {
    fetchAll: (productName, current, size) => {
        let url = fetchData.baseURL + "products";
        let isMoreThanOne = false;

        if (productName != null && current != null && size != null) {
            url += `?`;
        }

        if (productName != null) {
            url += `productName=${encodeURIComponent(productName)}`;
            isMoreThanOne = true;
        }

        if (current != null) {
            if (isMoreThanOne) {
                url += "&";
            }
            url += `current=${current}`;
            isMoreThanOne = true;
        }

        if (size != null) {
            if (isMoreThanOne) {
                url += "&";
            }
            url += `size=${size}`;
        }

        return fetch(url);
    },

    fetchProduct: (productId) => {
        return fetch(fetchData.baseURL + `products/${productId}`);
    }
};