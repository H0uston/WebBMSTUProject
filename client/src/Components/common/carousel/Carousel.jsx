import React, { Component } from "react";
import styles from "./Carousel.module.css"
import Slider from "react-slick";


const SampleNextArrow = (props) => {

    const { className, style, onClick } = props;

    return (
        <div
            className={styles.right + " " + styles.button}
            style={{...style}}
            onClick={onClick}
        />
    );
};

const SamplePrevArrow = (props) => {

    const { className, style, onClick } = props;

    return (
        <div
            className={styles.left + " " + styles.button}
            style={{...style}}
            onClick={onClick}
        />
    );
};

const Carousel = (props) => {
    const settings = {
        dots: false,
        infinite: true,
        speed: 500,
        slidesToShow: 4,
        slidesToScroll: 1,
        nextArrow: <SampleNextArrow />,
        prevArrow: <SamplePrevArrow />
    };

    return (
        <div className={styles.block}>
            <Slider {...settings}>
                {props.cards}
            </Slider>
        </div>
    )
};

export default Carousel