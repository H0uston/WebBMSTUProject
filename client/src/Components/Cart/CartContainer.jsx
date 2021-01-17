import {compose} from "redux";
import {connect} from "react-redux";
import Cart from "./Cart";
import {getIsAuthenticatedSelector, getTokenSelector} from "../../selectors/authSelectors";
import {Redirect} from "react-router-dom";
import React, {Component} from "react";
import ProductInCart from "./ProductInCart/ProductInCart";
import {changeProductCount, deleteProduct, fetchCart, makeOrder} from "../../state/cartReducer/cartReducer";
import {getProductsFromCartSelector} from "../../selectors/cartSelector";
import Preloader from "../common/preloader/Preloader";
import {getIsFetchingSelector} from "../../selectors/isFetchingSelectors";

class CartContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {finalPrice: 0};
    }

    componentDidMount() {
        let fetchData = async () => {
            await this.props.fetchCart(this.props.token);
        };

        if (this.props.isAuthenticated) {
            fetchData()
        }
    }

    updateFinalPrice = (oldPrice, newPrice) => {
        this.setState({finalPrice: newPrice - oldPrice});
    };

    render() {
        if (!this.props.isAuthenticated) {
            return <Redirect to={"/auth"}/>
        }

        if (this.props.isFetching || this.props.products === null) {
            return <Preloader/>
        }

        let productInCart = this.props.products.map(p =>
            <ProductInCart {...p} {...this.props} updateFinalPrice={this.updateFinalPrice.bind(this)} />
        );

        let finalPrice = this.props.products.reduce((accum, current) => accum + (current.productPriceWithDiscount || current.productPrice) * current.count, 0);

        return (
            <Cart productInCart={productInCart} finalPrice={finalPrice} {...this.props}/>
        )
    }
}

const mapStateToProps = (state) => ({
    products: getProductsFromCartSelector(state),
    token: getTokenSelector(state),
    isAuthenticated: getIsAuthenticatedSelector(state),
    isFetching: getIsFetchingSelector(state),
});

export default compose(connect(mapStateToProps, {
    fetchCart,
    deleteProduct,
    changeProductCount,
    makeOrder
}))(CartContainer);