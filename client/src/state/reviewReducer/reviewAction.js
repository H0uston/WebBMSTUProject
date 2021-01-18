export const SET_REVIEWS = "SET-REVIEWS";
export const CREATE_REVIEW = "CREATE-REVIEW";
export const CHANGE_REVIEW = "CHANGE-REVIEW";
export const DELETE_REVIEW = "DELETE-REVIEW";
export const UPDATE_REVIEW_TEXT = "UPDATE-REVIEW-TEXT";
export const UPDATE_REVIEW_RATING = "UPDATE-REVIEW-RATING";
export const SET_IS_REVIEW_EXIST = "SET-IS-REVIEW-EXIST";

export const setReviewsAction = (reviews) => ({
    type: SET_REVIEWS, reviews
});

export const createReviewAction = (productId) => ({
    type: CREATE_REVIEW, productId
});

export const changeReviewAction = (productId, data) => ({
    type: CHANGE_REVIEW, data
});

export const deleteReviewAction = (productId, reviewId) => ({
    type: DELETE_REVIEW, productId, reviewId
});

export const updateReviewTextAction = (reviewText) => ({
    type: UPDATE_REVIEW_TEXT, reviewText
});

export const updateReviewRatingAction = (reviewRating) => ({
    type: UPDATE_REVIEW_RATING, reviewRating
});

export const setIsReviewExistAction = (isReviewExist) => ({
    type: SET_IS_REVIEW_EXIST, isReviewExist
})