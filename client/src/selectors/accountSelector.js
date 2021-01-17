export const getSurnameSelector = (state) => {
    return state.accountPage.surname;
};

export const getNameSelector = (state) => {
    return state.accountPage.name;
};

export const getCitySelector = (state) => {
    return state.accountPage.city;
};

export const getStreetSelector = (state) => {
    return state.accountPage.street;
};

export const getHouseSelector = (state) => {
    return state.accountPage.house;
};

export const getFlatSelector = (state) => {
    return state.accountPage.flat;
};

export const getIndexSelector = (state) => {
    return state.accountPage.index;
};

export const getPasswordSelector = (state) => {
    return state.accountPage.password;
};

export const getIsDataFetchedSelector = (state) => {
    return state.accountPage.isDataFetched;
};