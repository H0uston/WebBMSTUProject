import fetchData from "./fetchData";

export const categoryAPI = {
    fetchAll: (current, size) => {
        let url = fetchData.baseURL + "categories";

        if (current != null && size != null) {
            url += `?current=${current}&size=${size}`;
        } else if (current != null) {
            url += `?current=${current}`;
        } else if (size != null) {
            url += `?size=${size}`;
        }

        return fetch(url);
    },

    fetchCategory: (categoryId) => {
        return fetch(fetchData.baseURL + `categories/${categoryId}`);
    }
};