import React from "react";
import styles from "./Search.module.css";

const Search = (props) => {
    debugger;
    return (
        <div className={styles.searchPageContainer}>
            <div className={styles.title}>
                Результаты по запросу "{props.currentSearchResult}"
            </div>
            <div className={styles.searchPageCards}>
                { props.cards }
            </div>
        </div>
    );
};

export default Search;