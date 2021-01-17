export const UPDATE_SURNAME = "UPDATE-SURNAME";
export const UPDATE_NAME = "UPDATE-NAME";
export const UPDATE_CITY = "UPDATE-CITY";
export const UPDATE_STREET = "UPDATE-STREET";
export const UPDATE_HOUSE = "UPDATE-HOUSE";
export const UPDATE_FLAT = "UPDATE-FLAT";
export const UPDATE_INDEX = "UPDATE-INDEX";
export const UPDATE_PASSWORD = "UPDATE_PASSWORD";
export const UPDATE_CHECK_PASSWORD = "UPDATE-CHECK-PASSWORD";
export const SET_USER_INFO = "SET-USER-INFO";
export const SET_IS_DATA_FETCHED = "SET-IS-DATA-FETCHED";

export const updateSurnameAction = (surname) => ({
    type: UPDATE_SURNAME, surname
});

export const updateNameAction = (name) => ({
    type: UPDATE_NAME, name
});

export const updateCityAction = (city) => ({
    type: UPDATE_CITY, city
});

export const updateStreetAction = (street) => ({
    type: UPDATE_STREET, street
});

export const updateHouseAction = (house) => ({
   type: UPDATE_HOUSE, house
});

export const updateFlatAction = (flat) => ({
   type: UPDATE_FLAT, flat
});

export const updateIndexAction = (index) => ({
   type: UPDATE_INDEX, index
});

export const updatePasswordAction = (password) => ({
   type: UPDATE_PASSWORD, password
});

export const updateCheckPasswordAction = (check_password) => ({
   type: UPDATE_CHECK_PASSWORD, check_password
});

export const setUserInfoAction = (data) => ({
   type: SET_USER_INFO, data
});

export const setIsDataFetchedAction = (isDataFetched) => ({
    type: SET_IS_DATA_FETCHED, isDataFetched
});