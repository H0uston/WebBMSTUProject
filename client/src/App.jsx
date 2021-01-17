import styles from './App.module.css';
import React from "react";
import Content from "./Components/Content/Content";
import Footer from "./Components/Footer/Footer";
import HeaderContainer from "./Components/Header/HeaderContainer";

const App = (props) => {
    return (
        <div className={styles.wrapper}>
            <HeaderContainer />
            <Content/>
            <Footer/>
        </div>
    );
};

export default App;
