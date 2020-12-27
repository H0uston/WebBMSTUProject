import fetchData from "./fetchData";

export const cartAPI = {
    fetchAll: (current, size, token) => {
        let url = fetchData.baseURL + "cart";

        if (current != null && size != null) {
            url += `?current=${current}&size=${size}`;
        } else if (current != null) {
            url += `?current=${current}`;
        } else if (size != null) {
            url += `?size=${size}`;
        }

        return fetch(url, {
            method: "GET",
            withCredentials: true,
            headers: {
                "Authorization": "Bearer" + token,
                "Content-Type": 'application/json'
            }
        });
    },

    addProductToCart: (data, token) => {
        return fetch(fetchData.baseURL + "cart",{
            method: "POST",
            mode: "CORS",
            credentials: "same-origin",
            withCredentials: true,
            headers: {
                "Authorization": "Bearer" + token,
                "Content-Type": 'application/json'
            },
            body: JSON.stringify(data)
        });
    },

    changeCount: (data, token) => {
        return fetch(fetchData.baseURL + "cart",{
            method: "PATCH",
            mode: "CORS",
            credentials: "same-origin",
            withCredentials: true,
            headers: {
                "Authorization": "Bearer" + token,
                "Content-Type": 'application/json'
            },
            body: JSON.stringify(data)
        });
    },

    deleteProduct: (ordersId, token) => {
        return fetch(fetchData.baseURL + `cart/${ordersId}`,{
            method: "DELETE",
            mode: "CORS",
            credentials: "same-origin",
            withCredentials: true,
            headers: {
                "Authorization": "Bearer" + token,
                "Content-Type": 'application/json'
            },
        });
    }
};