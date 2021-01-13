import React from "react";
import NoMatch from "../NoMatch/NoMatch";
import {Route, Switch} from "react-router-dom";
import styles from "./Content.module.css";
import HomeContainer from "../Home/HomeContainer";

const Content = (props) => {
    return (
        <main className={styles.mainContent}>
            <Switch>
                <Route path={"/"} render={() => <HomeContainer />}/>
                <Route path={"/*"} render={() => <NoMatch />}/>
            </Switch>
        </main>
    )
};

export default Content;