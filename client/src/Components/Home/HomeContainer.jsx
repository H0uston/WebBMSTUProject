import React, {useEffect} from "react";
import Home from "./Home";
import {compose} from "redux";
import {
    getCategoriesSelector,
    getCountOfCategoriesSelector,
    getProductsSelector
} from "../../selectors/homePageSelectors";
import {connect} from "react-redux";
import {getCategories} from "../../state/homeReducer/homeReducer";
import Preloader from "../common/preloader/Preloader";
import {getIsFetchingSelector} from "../../selectors/isFetchingSelectors";
import Card from "../common/card/Card";
import styles from "./Home.module.css";
import Carousel from "../common/carousel/Carousel";
import {getTokenSelector} from "../../selectors/authSelectors";
import {addProductToCart} from "../../state/cartReducer/cartReducer";
import {getDefaultCountOfProductsSelector} from "../../selectors/productSelector";


const HomeContainer = (props) => {

    useEffect(() => {
        const fetchData = async () => {
            await props.getCategories();
        };

        fetchData();
    }, []);

    if (props.isFetching || props.categories == null) {
        return <Preloader/>
    }

    let sortedCategories = props.categories.sort((a, b) => b.categoryCollection.length - a.categoryCollection.length);

    let cards = [];
    for (let i = 0; i < props.countOfCategories; i++) {
        cards.push([]);
        let productIds = sortedCategories[i].categoryCollection;

        for (let productId of productIds) {
            let product = props.products.find(p => p.productId === productId.productId);
            cards[i].push(<Card key={productId} {...product} addProductToCart={props.addProductToCart}
                                token={props.token} defaultCountOfProducts={props.defaultCountOfProducts}/>)
        }
    }

    let topCategories = cards.map((c, index) =>
        (<div key={c.ProductId} className={styles.block}>
            <div className={styles.categoryTitle}>{sortedCategories[index].categoryName}</div>
            <Carousel cards={c}/>
        </div>)
    );

    return (
        <Home topCategories={topCategories}/>
    )
};

const mapStateToProps = (state) => ({
    categories: getCategoriesSelector(state),
    products: getProductsSelector(state),
    countOfCategories: getCountOfCategoriesSelector(state),
    isFetching: getIsFetchingSelector(state),
    token: getTokenSelector(state),
    defaultCountOfProducts: getDefaultCountOfProductsSelector(state),
});

export default compose(connect(mapStateToProps, {
    getCategories,
    addProductToCart
}))(HomeContainer);