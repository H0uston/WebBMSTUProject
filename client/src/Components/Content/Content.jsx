import React from "react";
import NoMatch from "../NoMatch/NoMatch";
import {Route, Switch} from "react-router-dom";
import styles from "./Content.module.css";
import HomeContainer from "../Home/HomeContainer";
import AuthContainer from "../Auth/AuthContainer";
import AccountContainer from "../Account/AccountContainer";
import CartContainer from "../Cart/CartContainer";
import ProductContainer from "../Product/ProductContainer";
import SearchContainer from "../Search/SearchContainer";
import CategoriesContainer from "../Categories/CategoriesContainer";

const Content = (props) => {
    return (
        <main className={styles.mainContent}>
            <Switch>
                <Route exact path={"/"}>
                    <HomeContainer />
                </Route>
                <Route exact path={"/auth"}>
                    <AuthContainer />
                </Route>
                <Route exact path={"/account"}>
                    <AccountContainer />
                </Route>
                <Route exact path={"/cart"}>
                    <CartContainer />
                </Route>
                <Route exact path={"/product/:productId(\\d+)"}>
                    <ProductContainer />
                </Route>
                <Route path={"/search"}>
                    <SearchContainer />
                </Route>
                <Route path={"/categories"}>
                    <CategoriesContainer />
                </Route>
                <Route path={"*"}>
                    <NoMatch />
                </Route>
            </Switch>
        </main>
    )
};

export default Content;