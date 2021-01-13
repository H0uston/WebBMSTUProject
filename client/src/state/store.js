import {applyMiddleware, combineReducers, compose, createStore} from "redux";
import thunkMiddleware from "redux-thunk";
import homeReducer from "./homeReducer/homeReducer";
import authReducer from "./authReducer/authReducer";
import fetchingReducer from "./fetchingReducer/fetchingReducer";

let reducers = combineReducers({
    homePage: homeReducer,
    authInfo: authReducer,
    fetchingInfo: fetchingReducer,
});

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
let store = createStore(reducers, composeEnhancers(applyMiddleware(thunkMiddleware)));

export default store;