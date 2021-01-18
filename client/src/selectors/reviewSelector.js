export const getReviewsSelector = (state) => {
    return state.reviewInfo.reviews;
};

export const getReviewTextSelector = (state) => {
    return state.reviewInfo.reviewText;
};

export const getReviewRatingSelector = (state) => {
    return state.reviewInfo.reviewRating;
};

export const getIsReviewExistSelector = (state) => {
    return state.reviewInfo.isReviewExist;
};