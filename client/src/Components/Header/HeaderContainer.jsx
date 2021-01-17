import {getIsAuthenticatedSelector} from "../../selectors/authSelectors";
import Header from "./Header";
import {connect} from "react-redux";
import {compose} from "redux";

const mapStateToProps = (state) => ({
    isAuthenticated: getIsAuthenticatedSelector(state),
});

export default compose(connect(mapStateToProps, {}))(Header);