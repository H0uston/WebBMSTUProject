import React from "react";
import {
    getCurrentPageSelector,
    getCurrentSearchResultSelector,
    getDefaultCountOfProductsSelector,
    getProductsSelector,
    getSearchStringSelector,
    getSizePageSelector
} from "../../selectors/searchSelector";
import {addProductToCart} from "../../state/cartReducer/cartReducer";
import {fetchProducts} from "../../state/searchReducer/searchReducer";
import {compose} from "redux";
import {connect} from "react-redux";
import {withRouter, useLocation} from "react-router-dom";
import {getIsFetchingSelector} from "../../selectors/isFetchingSelectors";
import {getIsAuthenticatedSelector, getTokenSelector} from "../../selectors/authSelectors";
import Search from "./Search";
import Preloader from "../common/preloader/Preloader";
import Card from "../common/card/Card";

class SearchContainer extends React.Component {

    async updateProducts(productName) {
        await this.props.fetchProducts(productName, this.props.currentPage, this.props.sizePage);
    }

    componentDidMount() {
        let query = new URLSearchParams(this.props.location.search);
        let productName = query.get("productName");
        if (productName) {
            this.updateProducts(productName);
        } else {
            this.props.history.push("/notFound")
        }
    }

    componentDidUpdate(prevProps, prevState, snapshot) {
        debugger;
        if (prevProps.location.search !== this.props.location.search) {
            let query = new URLSearchParams(this.props.location.search);
            let productName = query.get("productName");
            if (productName) {
                this.updateProducts(productName);
            } else {
                this.props.history.push("/notFound")
            }
        }
    }

    render() {
        if (this.props.isFetching || this.props.products === null) {
            return <Preloader />
        }

        let cards = this.props.products.map(p =>
            <Card {...this.props} {...p} />
        );

        return <Search cards={cards} {...this.props} />
    }
}

let mapStateToProps = (state) => ({
    isFetching: getIsFetchingSelector(state),
    isAuthenticated: getIsAuthenticatedSelector(state),
    products: getProductsSelector(state),
    searchString: getSearchStringSelector(state),
    sizePage: getSizePageSelector(state),
    currentPage: getCurrentPageSelector(state),
    currentSearchResult: getCurrentSearchResultSelector(state),
    defaultCountOfProducts: getDefaultCountOfProductsSelector(state),
    token: getTokenSelector(state),
});

export default compose(connect(mapStateToProps, {
    addProductToCart,
    fetchProducts
}),
    withRouter)(SearchContainer);