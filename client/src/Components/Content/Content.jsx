import React from "react";
import NoMatch from "../NoMatch/NoMatch";
import {Route, Switch} from "react-router-dom";
import styles from "./Content.module.css";
import HomeContainer from "../Home/HomeContainer";
import AuthContainer from "../Auth/AuthContainer";

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
                <Route path={"*"}>
                    <NoMatch />
                </Route>
            </Switch>
        </main>
    )
};

export default Content;