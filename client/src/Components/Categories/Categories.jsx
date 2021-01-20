import React from "react";
import styles from "./Categories.module.css";
import CategoriesInfo from "./CategoriesInfo/CategoriesInfo";
import ProductsInfo from "./ProductsInfo/ProductsInfo";

const Categories = (props) => {
    return (
        <div className={styles.categoriesWrapper}>
            <CategoriesInfo {...props} />
            <ProductsInfo {...props}/>
        </div>
    )
};


export default Categories;