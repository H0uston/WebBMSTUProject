import {
    UPDATE_CITY,
    UPDATE_FLAT,
    UPDATE_HOUSE,
    UPDATE_INDEX,
    UPDATE_NAME,
    UPDATE_STREET,
    UPDATE_SURNAME,
    UPDATE_PASSWORD,
    UPDATE_CHECK_PASSWORD,
    SET_USER_INFO,
    setUserInfoAction,
    updateSurnameAction,
    updateNameAction,
    updateCityAction,
    updateHouseAction,
    updateStreetAction,
    updateIndexAction,
    updateFlatAction,
    updatePasswordAction,
    updateCheckPasswordAction,
    setIsDataFetchedAction, SET_IS_DATA_FETCHED
} from "./accountAction";
import {setIsFetching} from "../fetchingReducer/fetchingAction";
import {userAPI} from "../../API/userAPI";


let initialState = {
    surname: "",
    name: "",
    city: "",
    street: "",
    house: "",
    flat: "",
    index: "",
    password: "",
    isDataFetched: false,
};

const accountReducer = (state=initialState, action) => {
    let stateCopy = {...state};

    switch (action.type) {
        case (SET_USER_INFO):
            debugger;
            stateCopy.surname = action.data.userSurname;
            stateCopy.name = action.data.userName;
            stateCopy.city = action.data.userCity;
            stateCopy.street = action.data.userStreet;
            stateCopy.house = action.data.userHouse;
            stateCopy.flat = action.data.userFlat;
            stateCopy.index = action.data.userIndex;
            break;
        case (UPDATE_SURNAME):
            stateCopy.surname = action.surname;
            break;
        case (UPDATE_NAME):
            stateCopy.name = action.name;
            break;
        case (UPDATE_CITY):
            stateCopy.city = action.city;
            break;
        case (UPDATE_STREET):
            stateCopy.street = action.street;
            break;
        case (UPDATE_HOUSE):
            stateCopy.house = action.house;
            break;
        case (UPDATE_FLAT):
            stateCopy.flat = action.flat;
            break;
        case (UPDATE_INDEX):
            stateCopy.index = action.index;
            break;
        case (UPDATE_PASSWORD):
            stateCopy.password = action.password;
            break;
        case (UPDATE_CHECK_PASSWORD):
            stateCopy.check_password = action.checkPassword;
            break;
        case (SET_IS_DATA_FETCHED):
            stateCopy.isDataFetched = action.isDataFetched;
            break;
        default:
            break;
    }

    return stateCopy;
};

export const fetchAccountInfo = (id, token) => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await userAPI.fetchInfo(id, token);
    if (response.status === 200) {
        let result = await response.json();
        dispatch(setUserInfoAction(result));
        dispatch(setIsDataFetchedAction(true));
        dispatch(setIsFetching(false));
    } else if (response.status === 201) {
        console.error("Unauthorized!");
    } else {
        console.error("Unknown error in register!");
    }
};

export const editAccountInfo = (id, token, data) => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await userAPI.changeInfo(id, token, data);
    if (response.status === 200) {
        let result = await response.json();
        dispatch(setUserInfoAction(result));
        dispatch(setIsFetching(false));
    } else if (response.status === 201) {
        console.error("Unauthorized!");
    } else {
        console.error("Unknown error in register!");
    }
};

export const updateSurname = (surname) => (dispatch) => {
    dispatch(updateSurnameAction(surname));
};

export const updateName = (name) => (dispatch) => {
    dispatch(updateNameAction(name));
};

export const updateCity = (city) => (dispatch) => {
    dispatch(updateCityAction(city));
};

export const updateStreet = (street) => (dispatch) => {
    dispatch(updateStreetAction(street));
};

export const updateHouse = (house) => (dispatch) => {
    dispatch(updateHouseAction(house));
};

export const updateFlat = (flat) => (dispatch) => {
    dispatch(updateFlatAction(flat));
};

export const updateIndex = (index) => (dispatch) => {
    dispatch(updateIndexAction(index));
};

export const updatePassword = (password) => (dispatch) => {
    dispatch(updatePasswordAction(password))
};

export const updateCheckPassword = (checkPassword) => (dispatch) => {
    dispatch(updateCheckPasswordAction(checkPassword));
};

export const setIsDataFetched = (isDataFetched) => (dispatch) => {
    dispatch(setIsDataFetchedAction(isDataFetched));
};

export default accountReducer;