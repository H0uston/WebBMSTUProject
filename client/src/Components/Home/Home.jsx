import React from "react";
import styles from "./Home.module.css";

import {Carousel} from "antd";
import "antd/dist/antd.css";
import Card from "../common/card/Card";

const Home = (props) => {
    return (
        <div className={"homeContainer"}>
            <Carousel autoplay>
                <div className={styles.slide}>
                    <Card name={"Cart 1"}/>
                    <Card name={"Cart 2"}/>
                    <Card name={"Cart 3"}/>
                    <Card name={"Cart 4"}/>
                </div>
                <div className={styles.slide}>
                    <Card name={"Cart 1"}/>
                    <Card name={"Cart 2"}/>
                    <Card name={"Cart 3"}/>
                    <Card name={"Cart 4"}/>
                </div>
                <div className={styles.slide}>
                    <Card name={"Cart 1"}/>
                    <Card name={"Cart 2"}/>
                    <Card name={"Cart 3"}/>
                    <Card name={"Cart 4"}/>
                </div>
                <div className={styles.slide}>
                    <Card name={"Cart 1"}/>
                    <Card name={"Cart 2"}/>
                    <Card name={"Cart 3"}/>
                    <Card name={"Cart 4"}/>
                </div>
            </Carousel>
        </div>
    )
};

export default Home;