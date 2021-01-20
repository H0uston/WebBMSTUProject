import React from "react";
import styles from "./Category.module.css";
import FunctionalButton from "../../../common/buttons/FunctionalButton";

const Category = (props) => {

    let changeCategoryStatus = () => {
        if (props.categoryId === props.chosenCategory) {
            props.unsetCategory();
        } else {
            props.setCategory(props.categoryId);
        }
    };

    return (
        <div className={styles.categoryContainer}>
            <FunctionalButton bgcolor={props.chosenCategory === props.categoryId && "grey"}
                              onClick={() => changeCategoryStatus()}
                              text={props.categoryName} />
        </div>
    )
};

export default Category;