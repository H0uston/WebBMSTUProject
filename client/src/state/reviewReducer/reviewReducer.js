import {
    SET_REVIEWS,
    UPDATE_REVIEW_RATING,
    UPDATE_REVIEW_TEXT,
    setReviewsAction,
    updateReviewTextAction, updateReviewRatingAction, SET_IS_REVIEW_EXIST, setIsReviewExistAction
} from "./reviewAction";
import {setIsFetching} from "../fetchingReducer/fetchingAction";
import {reviewAPI} from "../../API/reviewAPI";
import {updateProductRating} from "../productReducer/productAction";
import {productAPI} from "../../API/productAPI";

let initialState = {
    reviews: null,
    reviewText: "",
    reviewRating: 1,
    isReviewExist: false,
};

const reviewReducer = (state=initialState, action) => {
    let stateCopy = {...state};

    switch (action.type) {
        case (SET_REVIEWS):
            stateCopy.reviews = action.reviews;
            break;
        case (UPDATE_REVIEW_TEXT):
            stateCopy.reviewText = action.reviewText;
            break;
        case (UPDATE_REVIEW_RATING):
            stateCopy.reviewRating = action.reviewRating;
            break;
        case (SET_IS_REVIEW_EXIST):
            stateCopy.isReviewExist = action.isReviewExist;
            break;
        default:
            break;
    }

    return stateCopy;
};

export const fetchReviews = (productId) => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await reviewAPI.fetchAllReviewsForProduct(productId);
    if (response.status === 200) {
        let result = await response.json();
        dispatch(setReviewsAction(result));
        dispatch(setIsFetching(false));
    } else {
        console.error("Unknown error while fetching reviews");
    }
};

export const createReview = (productId, reviewText, reviewRating, token) => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await reviewAPI.createReview({reviewText, reviewRating, productId}, productId,  token);
    if (response.status === 201) {
        let response = await reviewAPI.fetchAllReviewsForProduct(productId);
        if (response.status === 200) {
            let result = await response.json();
            dispatch(setReviewsAction(result));
            dispatch(setIsFetching(false));
            response = await productAPI.fetchProduct(productId);
            if (response.status === 200) {
                result = await response.json();
                dispatch(updateProductRating(result.productRating));
            }
        } else {
            console.error("Unknown error while fetching reviews after creating");
        }
    } else {
        console.error("Unknown error while creating review");
    }
};

export const changeReview = (productId, reviewText, reviewRating, token) => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await reviewAPI.changeReview({reviewText, reviewRating, productId}, productId, token);
    if (response.status === 200) {
        let response = await reviewAPI.fetchAllReviewsForProduct(productId);
        if (response.status === 200) {
            let result = await response.json();
            dispatch(setReviewsAction(result));
            dispatch(setIsFetching(false));
            response = await productAPI.fetchProduct(productId);
            if (response.status === 200) {
                result = await response.json();
                dispatch(updateProductRating(result.productRating));
            }
        } else {
            console.error("Unknown error while fetching reviews after creating");
        }
    } else {
        console.error("Unknown error while creating review");
    }
};

export const deleteReview = (reviewId, productId, token) => async (dispatch) => {
    dispatch(setIsFetching(true));
    let response = await reviewAPI.deleteReview(reviewId, productId, token);
    if (response.status === 204) {
        let response = await reviewAPI.fetchAllReviewsForProduct(productId);
        if (response.status === 200) {
            let result = await response.json();
            dispatch(setReviewsAction(result));
            dispatch(updateReviewText(""));
            dispatch(updateReviewRating(1));
            dispatch(setIsFetching(false));
            response = await productAPI.fetchProduct(productId);
            if (response.status === 200) {
                result = await response.json();
                dispatch(updateProductRating(result.productRating));
            }
        } else {
            console.error("Unknown error while fetching reviews after creating");
        }
    } else {
        console.error("Unknown error while creating review");
    }
};

export const setReviews = (reviews) => async (dispatch) => {
    dispatch(setReviewsAction(reviews));
};

export const updateReviewText = (reviewText) => async (dispatch) => {
    dispatch(updateReviewTextAction(reviewText));
};

export const updateReviewRating = (reviewRating) => async (dispatch) => {
    dispatch(updateReviewRatingAction(reviewRating));
};

export const setIsReviewExist = (isReviewExist) => async (dispatch) => {
    dispatch(setIsReviewExistAction(isReviewExist));
};

export default reviewReducer;