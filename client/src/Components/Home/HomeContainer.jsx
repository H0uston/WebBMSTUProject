import React, {useEffect} from "react";
import Home from "./Home";
import {compose} from "redux";
import {getCategoriesSelector, getProductsSelector} from "../../selectors/homePageSelectors";
import {connect} from "react-redux";
import {getCategories} from "../../state/homeReducer/homeReducer";
import Preloader from "../common/preloader/Preloader";
import {getIsFetchingSelector} from "../../selectors/isFetchingSelectors";


const HomeContainer = (props) => {

    useEffect(() => {
        const fetchData = async () => {
            await props.getCategories();
        };

        fetchData();
    }, []);

    if (props.isFetching || props.categories == null) {
        return <Preloader />
    }

    let sortedCategories = props.categories.sort((a,b) => b.categoryCollection.length - a.categoryCollection.length);

    let cardSlides = [];
    let i = 0; // TODO
    for (let sortedCategory of sortedCategories) {
        if (i >= 3) {
            break;
        }

        let j = 0;
        cardSlides.push([]);

        let productsInCategory = [];
        for (let category of sortedCategory.categoryCollection) {
            if (j >= 8) {
                break;
            }

            let [product] = props.products.filter(p => p.productId === category.productId);
            productsInCategory.push(product);

            j += 1;
        }

        for (let k = 0; k < productsInCategory.length; k += 4) {
            cardSlides[i].push(productsInCategory.slice(k, k + 4));
        }

        i += 1;
    }

    return (
        <Home topCategories={sortedCategories} cardSlides={cardSlides}/>
    )
};

const mapStateToProps = (state) => ({
    categories: getCategoriesSelector(state),
    products: getProductsSelector(state),
    isFetching: getIsFetchingSelector(state)
});

export default compose(connect(mapStateToProps, {getCategories}))(HomeContainer);