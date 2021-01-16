import fetchData from "./fetchData";

export const authAPI = {
    fetchLogin: (data) => {
        return fetch(fetchData.baseURL + "auth/login", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        });
    },
    fetchRegister: (data) => {
        debugger;
        return fetch(fetchData.baseURL + "auth/register",{
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        });
    }
};