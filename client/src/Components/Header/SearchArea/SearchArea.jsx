import React from "react";
import styles from "./SearchArea.module.css";

const SearchArea = (props) => {
    return (
        <div className={styles.searchContent}>
            <input className={styles.input}
                   value={props.searchString}
                   onChange={(e) => props.updateSearchString(e.currentTarget.value)} />
        </div>
    );
};

export default SearchArea;