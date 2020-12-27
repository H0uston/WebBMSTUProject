import React from 'react';
import {Row, Col, Carousel} from 'antd';
import {LeftOutlined, RightOutlined} from "@ant-design/icons";
import defaultStyles from "./CardSlider.css";

const contentStyle = {
    height: '160px',
    color: '#fff',
    lineHeight: '160px',
    textAlign: 'center',
    background: '#364d79'
};

const SampleNextArrow = props => {
    const {className, style, onClick} = props;
    return (
        <div
            className={className}
            style={{
                ...style,
                color: 'black',
                fontSize: '15px',
                lineHeight: '1.5715'
            }}
            onClick={onClick}
        >
            <RightOutlined/>
        </div>
    )
};

const SamplePrevArrow = props => {
    const {className, style, onClick} = props;
    return (
        <div
            className={className}
            style={{
                ...style,
                color: 'black',
                fontSize: '15px',
                lineHeight: '1.5715'
            }}
            onClick={onClick}
        >
            <LeftOutlined/>
        </div>
    )
};

const settings = {
    nextArrow: <SampleNextArrow/>,
    prevArrow: <SamplePrevArrow/>
};

const CarouselArrows = () => {
    return (
        <Row justify="center">
            <Col span={20}>
                <Carousel arrows {...settings}>
                    <div>
                        <h3 style={contentStyle}>1</h3>
                    </div>
                    <div>
                        <h3 style={contentStyle}>2</h3>
                    </div>
                    <div>
                        <h3 style={contentStyle}>3</h3>
                    </div>
                    <div>
                        <h3 style={contentStyle}>4</h3>
                    </div>
                </Carousel>
            </Col>
        </Row>
    )
};

export default CarouselArrows