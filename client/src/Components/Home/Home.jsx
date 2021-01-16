import React from "react";
import styles from "./Home.module.css";

const Home = (props) => {
    return (
        <div className={styles.center}>
            {props.topCategories}
        </div>
    )
};

export default Home;