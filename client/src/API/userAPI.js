import fetchData from "./fetchData";

export const userAPI = {
    fetchInfo: (id, token) => {
        return fetch(fetchData.baseURL + `users/${id}`, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": 'application/json'
            },
        });
    },

    changeInfo: (id, token, data) => {
        return fetch(fetchData.baseURL + `users/${id}`, {
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        })
    },
};