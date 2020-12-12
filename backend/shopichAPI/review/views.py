from django.forms import model_to_dict
from rest_framework import status
from rest_framework.generics import ListCreateAPIView
from rest_framework.response import Response
from rest_framework.views import APIView

from .models import Review
from .serializers import ReviewSerializer
from rest_framework.permissions import IsAuthenticated, AllowAny
from datetime import datetime


class BaseManageView(APIView):

    def dispatch(self, request, *args, **kwargs):
        if not hasattr(self, 'VIEWS_BY_METHOD'):
            raise Exception('VIEWS_BY_METHOD static dictionary variable must be defined on a ManageView class!')
        if request.method in self.VIEWS_BY_METHOD:
            return self.VIEWS_BY_METHOD[request.method]()(request, *args, **kwargs)

        return Response(status=405)


class ReviewList(ListCreateAPIView):
    serializer_class = ReviewSerializer
    permission_classes = [IsAuthenticated]

    def post(self, request, *args, **kwargs):
        if Review.objects.filter(user_id=self.request.user.user_id).exists():
            return Response({"message": "Review is already exist"}, status=status.HTTP_400_BAD_REQUEST)
        data = request.data
        data['user_id'] = request.user.user_id
        data['review_date'] = datetime.now().date()
        serializer = ReviewSerializer(data=data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)

        return Response(serializer.errors, status=status.HTTP_401_UNAUTHORIZED)

    def put(self, request):
        if Review.objects.filter(user_id=self.request.user.user_id).exists():
            review = model_to_dict(Review.objects.get(user_id=self.request.user.user_id))
            review.review_date = datetime.now().date()
            review.review_text = request.data["review_text"]
            review.review_rating = request.data["review_rating"]
            return Response(status=status.HTTP_204_NO_CONTENT)
        return Response({"message": "object does not exist"}, status=status.HTTP_400_BAD_REQUEST)

    def delete(self, request, review_id, format=None):
        if Review.objects.filter(review_id=review_id).exists():
            Review.objects.get(review_id=review_id).delete()
            return Response(status=status.HTTP_204_NO_CONTENT)
        return Response({"message": "object does not exist"}, status=status.HTTP_400_BAD_REQUEST)


class ReviewDetailView(ListCreateAPIView):
    serializer_class = ReviewSerializer

    def get_queryset(self):
        product_id = self.kwargs['review_id']
        return Review.objects.filter(product_id=product_id)


class ReviewManageView(BaseManageView):
    VIEWS_BY_METHOD = {
        'DELETE': ReviewList.as_view,
        'GET': ReviewDetailView.as_view,
        'POST': ReviewList.as_view,
        'PUT': ReviewList.as_view
    }