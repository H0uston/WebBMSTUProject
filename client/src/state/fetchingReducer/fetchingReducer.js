import {SET_IS_FETCHING} from "./fetchingAction";

let initialState = {
    isFetching: false,
};

const fetchingReducer = (state=initialState, action) => {
    let stateCopy = {...state};

    switch (action.type) {
        case (SET_IS_FETCHING):
            stateCopy.isFetching = action.isFetching;
            break;
        default:
            break;
    }

    return stateCopy;
};

export default fetchingReducer;