import fetchData from "./fetchData";

export const reviewAPI = {
    fetchAllReviewsForProduct: (productId, current, size, token) => {
        let url = fetchData.baseURL + `review/${productId}`;

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
                "Content-Type": 'application/json'
            }
        });
    },

    createReview: (data, token) => {
        return fetch(fetchData.baseURL + "review",{
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

    changeReview: (data, token) => {
        return fetch(fetchData.baseURL + "review",{
            method: "PUT",
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

    deleteReview: (reviewId, token) => {
        return fetch(fetchData.baseURL + `review/${reviewId}`,{
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
};