import React from "react";
import styles from "./Home.module.css";

import {Carousel} from "antd";
import "antd/dist/antd.css";
import Card from "../common/card/Card";
import CarouselComponent from "../common/cardSlider/CardSlider";

const Home = (props) => {
    return (
        <div className={"homeContainer"}>
            <CarouselComponent />
        </div>
    )
};

export default Home;