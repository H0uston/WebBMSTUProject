from rest_framework import status
from rest_framework.generics import ListCreateAPIView
from rest_framework.response import Response

from .models import Review
from .serializers import ReviewSerializer
from rest_framework.permissions import IsAuthenticated
from datetime import datetime


class ReviewList(ListCreateAPIView):
    serializer_class = ReviewSerializer
    permission_classes = [IsAuthenticated]

    def create(self, request, *args, **kwargs):
        data = request.data
        data['user_id'] = request.user.user_id
        data['review_date'] = datetime.now().date()
        serializer = ReviewSerializer(data=data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)

        return Response(serializer.errors, status=status.HTTP_401_UNAUTHORIZED)


class ReviewDetailView(ListCreateAPIView):  # TODO delete method, actually, we already have it
    serializer_class = ReviewSerializer

    def get_queryset(self):
        product_id = self.kwargs['product_id']
        return Review.objects.filter(product_id=product_id)
