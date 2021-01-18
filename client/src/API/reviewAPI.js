import fetchData from "./fetchData";

export const reviewAPI = {
    fetchAllReviewsForProduct: (productId) => {
        let url = fetchData.baseURL + `products/${productId}/reviews`;

        return fetch(url, {
            method: "GET",
            headers: {
                "Content-Type": 'application/json'
            }
        });
    },

    createReview: (data, productId, token) => {
        return fetch(fetchData.baseURL + `products/${productId}/reviews`,{
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": 'application/json'
            },
            body: JSON.stringify(data)
        });
    },

    changeReview: (data, productId, token) => {
        return fetch(fetchData.baseURL + `products/${productId}/reviews`,{
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": 'application/json'
            },
            body: JSON.stringify(data)
        });
    },

    deleteReview: (reviewId, productId, token) => {
        return fetch(fetchData.baseURL + `products/${productId}/reviews/${reviewId}`,{
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": 'application/json'
            },
        });
    },
};