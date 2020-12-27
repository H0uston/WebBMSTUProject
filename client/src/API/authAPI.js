import fetchData from "./fetchData";

export const authAPI = {
    fetchLogin: (data) => {
        return fetch(fetchData.baseURL + "auth/login", {
            method: "POST",
            mode: "CORS",
            credentials: "same-origin",
            body: JSON.stringify(data)
        });
    },
    fetchRegister: (data) => {
        return fetch(fetchData.baseURL + "auth/register",{
            method: "POST",
            mode: "CORS",
            credentials: "same-origin",
            body: JSON.stringify(data)
        });
    }
};