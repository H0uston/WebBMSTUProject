import React from "react";
import styles from "./Search.module.css";

const Search = (props) => {
    return (
        <div className={styles.searchContent}>
            <input className={styles.input} />
        </div>
    );
};

export default Search;