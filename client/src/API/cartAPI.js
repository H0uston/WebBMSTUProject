import fetchData from "./fetchData";

export const cartAPI = {
    fetchAll: (token) => {
        let url = fetchData.baseURL + "cart/products";

        return fetch(url, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": 'application/json'
            }
        });
    },

    addProductToCart: (data, token) => {
        return fetch(fetchData.baseURL + "cart/products",{
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": 'application/json'
            },
            body: JSON.stringify(data)
        });
    },

    changeCount: (data, token) => {
        return fetch(fetchData.baseURL + "cart/products",{
            method: "PATCH",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": 'application/json'
            },
            body: JSON.stringify(data)
        });
    },

    deleteProduct: (productId, token) => {
        return fetch(fetchData.baseURL + `cart/products/${productId}`,{
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": 'application/json'
            },
        });
    }
};