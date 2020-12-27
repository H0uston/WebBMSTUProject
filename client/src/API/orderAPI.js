import fetchData from "./fetchData";

export const orderAPI = {
    fetchAll: (current, size, token) => {
        let url = fetchData.baseURL + "order";

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

    createOrder: (data, token) => {
        return fetch(fetchData.baseURL + "order",{
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

    deleteOrder: (orderId, token) => {
        return fetch(fetchData.baseURL + `order/${orderId}`,{
            method: "DELETE",
            mode: "CORS",
            credentials: "same-origin",
            withCredentials: true,
            headers: {
                "Authorization": "Bearer" + token,
                "Content-Type": 'application/json'
            },
        });
    },

    getOrder: (orderId, token) => {
        return fetch(fetchData.baseURL + `order/${orderId}`,{
            method: "GET",
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