import styles from './App.module.css';
import React from "react";
import Header from "./Components/Header/Header";
import Content from "./Components/Content/Content";
import Footer from "./Components/Footer/Footer";

function App() { // TODO
    return (
        <div className={styles.wrapper}>
            <Header/>
            <Content/>
            <Footer/>
        </div>
    );
}

export default App;
