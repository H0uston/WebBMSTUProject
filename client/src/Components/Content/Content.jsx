import React from "react";
import NoMatch from "../NoMatch/NoMatch";
import {Route, Switch} from "react-router-dom";
import styles from "./Content.module.css";
import Home from "../Home/Home";

const Content = (props) => {
    return (
        <main className={styles.mainContent}>
            <Switch>
                <Route path={"/"} render={() => <Home />}/>
                <Route path={"/*"} render={() => <NoMatch />}/>
            </Switch>
        </main>
    )
};

export default Content;