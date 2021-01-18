import {getIsFetchingSelector} from "../../selectors/isFetchingSelectors";
import {getIsAuthenticatedSelector, getTokenSelector} from "../../selectors/authSelectors";
import {addProductToCart} from "../../state/cartReducer/cartReducer";
import {compose} from "redux";
import {connect} from "react-redux";
import {getDefaultCountOfProductsSelector, getProductSelector} from "../../selectors/productSelector";
import React, {Component} from "react";
import {withRouter} from "react-router-dom";
import Product from "./Product";
import {fetchProduct, setProduct} from "../../state/productReducer/productReducer";
import Preloader from "../common/preloader/Preloader";

class ProductContainer extends Component {

    componentDidMount() {
        let fetchData = async () => {
            await this.props.fetchProduct(+this.props.match.params.productId);
        };
        if (+this.props.match.params.productId) {
            fetchData();
        } else {
            console.error("Wrong page!");
        }
    }

    componentWillUnmount() {
        this.props.setProduct(null);
    }

    render() {
        if (this.props.product === null) {
            return <Preloader />
        }

        return (
            <Product {...this.props}/>
        );
    }
}

let mapStateToProps = (state) => ({
    isFetching: getIsFetchingSelector(state),
    isAuthenticated: getIsAuthenticatedSelector(state),
    product: getProductSelector(state),
    token: getTokenSelector(state),
    defaultCountOfProducts: getDefaultCountOfProductsSelector(state),
});

export default compose(
    connect(mapStateToProps, {
        addProductToCart,
        fetchProduct,
        setProduct
    }),
    withRouter)(ProductContainer);