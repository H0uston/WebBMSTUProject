import {getIsAuthenticatedSelector} from "../../selectors/authSelectors";
import Header from "./Header";
import {connect} from "react-redux";
import {compose} from "redux";
import {getSearchStringSelector} from "../../selectors/searchSelector";
import {setCurrentSearchString, updateSearchString} from "../../state/searchReducer/searchReducer";

const mapStateToProps = (state) => ({
    isAuthenticated: getIsAuthenticatedSelector(state),
    searchString: getSearchStringSelector(state),
});

export default compose(connect(mapStateToProps, { updateSearchString, setCurrentSearchString }))(Header);