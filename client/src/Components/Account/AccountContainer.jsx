import {getIsFetchingSelector} from "../../selectors/isFetchingSelectors";
import {getIsAuthenticatedSelector, getTokenSelector, getUserIdSelector} from "../../selectors/authSelectors";
import {
    getCitySelector, getFlatSelector,
    getHouseSelector, getIndexSelector, getIsDataFetchedSelector,
    getNameSelector, getPasswordSelector,
    getStreetSelector,
    getSurnameSelector
} from "../../selectors/accountSelector";
import {compose} from "redux";
import {connect} from "react-redux";
import {
    editAccountInfo, fetchAccountInfo, setIsDataFetched,
    updateCheckPassword,
    updateCity, updateFlat,
    updateHouse, updateIndex,
    updateName, updatePassword,
    updateStreet,
    updateSurname
} from "../../state/accountReducer/accountReducer";
import Account from "./Account";
import React, {Component, useEffect} from "react";
import {Redirect} from "react-router-dom";

class AccountContainer extends Component {

    componentDidMount() {
        let fetchData = async (userId, token) => {
            await this.props.fetchAccountInfo(userId, token);
        };

        if (!this.props.isDataFetched) {
            fetchData(this.props.userId, this.props.token);
        }
    }

    render() {
        if (!this.props.isAuthenticated) {
            return <Redirect to={"/auth"}/>
        }

        return (
            <Account {...this.props}/>
        )
    }
}

let mapStateToProps = (state) => ({
    isAuthenticated: getIsAuthenticatedSelector(state),
    isFetching: getIsFetchingSelector(state),
    userId: getUserIdSelector(state),
    token: getTokenSelector(state),
    surname: getSurnameSelector(state),
    name: getNameSelector(state),
    city: getCitySelector(state),
    street: getStreetSelector(state),
    house: getHouseSelector(state),
    flat: getFlatSelector(state),
    index: getIndexSelector(state),
    password: getPasswordSelector(state),
    isDataFetched: getIsDataFetchedSelector(state),
});

export default compose(connect(mapStateToProps, {
    updateSurname,
    updateName,
    updateCity,
    updateStreet,
    updateHouse,
    updateFlat,
    updateIndex,
    updatePassword,
    updateCheckPassword,
    fetchAccountInfo,
    editAccountInfo,
    setIsDataFetched,
}))(AccountContainer)