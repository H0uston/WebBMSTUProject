import {applyMiddleware, combineReducers, compose, createStore} from "redux";
import thunkMiddleware from "redux-thunk";
import homeReducer from "./homeReducer/homeReducer";
import authReducer from "./authReducer/authReducer";
import fetchingReducer from "./fetchingReducer/fetchingReducer";
import accountReducer from "./accountReducer/accountReducer";
import cartReducer from "./cartReducer/cartReducer";
import productReducer from "./productReducer/productReducer";
import reviewReducer from "./reviewReducer/reviewReducer";
import searchReducer from "./searchReducer/searchReducer";
import categoriesReducer from "./categoriesReducer/categoriesReducer";

let reducers = combineReducers({
    homePage: homeReducer,
    authInfo: authReducer,
    fetchingInfo: fetchingReducer,
    accountPage: accountReducer,
    cartPage: cartReducer,
    productPage: productReducer,
    reviewInfo: reviewReducer,
    searchPage: searchReducer,
    categoriesPage: categoriesReducer,
});

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
let store = createStore(reducers, composeEnhancers(applyMiddleware(thunkMiddleware)));

export default store;