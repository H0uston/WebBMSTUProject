import React from "react";
import {getIsAuthenticatedSelector, getTokenSelector} from "../../selectors/authSelectors";
import {
    getCategoriesSelector,
    getChosenCategorySelector,
    getDefaultCountOfProducts,
    getProductsSelector
} from "../../selectors/categoriesSelector";
import {compose} from "redux";
import {connect} from "react-redux";
import {fetchCategories, setCategory, unsetCategory} from "../../state/categoriesReducer/categoriesReducer";
import {getIsFetchingSelector} from "../../selectors/isFetchingSelectors";
import Preloader from "../common/preloader/Preloader";
import Categories from "./Categories";
import Card from "../common/card/Card";
import Category from "./CategoriesInfo/Category/Category";
import {addProductToCart} from "../../state/cartReducer/cartReducer";

class CategoriesContainer extends React.Component {

    componentDidMount() {
        let fetchData = async () => {
            await this.props.fetchCategories();
        };

        fetchData()
    }

    render () {

        if (this.props.isFetching || this.props.products === null) {
            return <Preloader />
        }

        let categories = this.props.categories.map(c =>
            <Category key={c.categoryId} {...c} {...this.props}/>
        );

        let cards = this.props.products.map(p =>
            <Card key={p.productId} {...p} {...this.props} />
        );

        let currentCategory = this.props.categories.find(c => c.categoryId === this.props.chosenCategory);

        return <Categories {...this.props} cards={cards} categories={categories} currentCategory={currentCategory}/>
    }
}

let mapStateToProps = (state) => ({
    token: getTokenSelector(state),
    isAuthenticated: getIsAuthenticatedSelector(state),
    isFetching: getIsFetchingSelector(state),
    products: getProductsSelector(state),
    categories: getCategoriesSelector(state),
    chosenCategory: getChosenCategorySelector(state),
    defaultCountOfProducts: getDefaultCountOfProducts(state),
});

export default compose(connect(mapStateToProps, {
    setCategory,
    unsetCategory,
    fetchCategories,
    addProductToCart
}))(CategoriesContainer)