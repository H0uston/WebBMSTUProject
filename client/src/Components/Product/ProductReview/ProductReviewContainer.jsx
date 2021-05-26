import React from "react";
import {
    getIsReviewExistSelector,
    getReviewRatingSelector,
    getReviewsSelector,
    getReviewTextSelector
} from "../../../selectors/reviewSelector";
import {compose} from "redux";
import {connect} from "react-redux";
import ProductReview from "./ProductReview";
import Preloader from "../../common/preloader/Preloader";
import {
    changeReview,
    createReview,
    deleteReview,
    fetchReviews, setIsReviewExist,
    setReviews,
    updateReviewRating,
    updateReviewText
} from "../../../state/reviewReducer/reviewReducer";
import Review from "./Review/Review";
import {getIsFetchingSelector} from "../../../selectors/isFetchingSelectors";
import {getIsAuthenticatedSelector, getTokenSelector, getUserIdSelector} from "../../../selectors/authSelectors";

class ProductReviewContainer extends React.Component {

    componentDidMount() {
        let fetchData = async () => {
            await this.props.fetchReviews(this.props.productId)
        };

        fetchData();
    }

    componentWillUnmount() {
        this.props.setReviews(null);

        if (this.props.isReviewExist) {
            this.props.updateReviewText("");
            this.props.setIsReviewExist(false);
            this.props.updateReviewRating(1);
        }
    }

    render() {
        if (this.props.isFetching || this.props.reviews === null) {
            return <Preloader />
        }

        let reviews = this.props.reviews, myReview = null;
        if (this.props.isAuthenticated) {
            let review = this.props.reviews.find(r => r.userId === this.props.userId);
            if (review) {
                myReview = review;
                reviews = this.props.reviews.filter(r => r.userId !== this.props.userId);
                if (!this.props.isReviewExist) {
                    this.props.setIsReviewExist(true);
                    this.props.updateReviewText(review.reviewText);
                    this.props.updateReviewRating(review.reviewRating);
                }
            }
        }

        let reviewsElements = reviews.map(r =>
            <Review key={r.reviewId} {...r}/>
        );

        return (
            <ProductReview myReview={myReview} reviewsElements={reviewsElements} {...this.props}/>
        )
    }
}

let mapStateToProps = (state) => ({
    reviews: getReviewsSelector(state),
    reviewText: getReviewTextSelector(state),
    reviewRating: getReviewRatingSelector(state),
    isFetching: getIsFetchingSelector(state),
    isAuthenticated: getIsAuthenticatedSelector(state),
    userId: getUserIdSelector(state),
    token: getTokenSelector(state),
    isReviewExist: getIsReviewExistSelector(state),
});

export default compose(connect(mapStateToProps, {
    fetchReviews,
    createReview,
    changeReview,
    deleteReview,
    setReviews,
    updateReviewText,
    updateReviewRating,
    setIsReviewExist
}))(ProductReviewContainer)