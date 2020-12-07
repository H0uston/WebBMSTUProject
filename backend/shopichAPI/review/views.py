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
        data = request.data
        data['user_id'] = request.user.user_id
        data['review_date'] = datetime.now().date()
        serializer = ReviewSerializer(data=data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)

        return Response(serializer.errors, status=status.HTTP_401_UNAUTHORIZED)

    def delete(self, request, pk, format=None):
        if Review.objects.filter(review_id=pk).exists():
            Review.objects.get(review_id=pk).delete()
            return Response(status=status.HTTP_204_NO_CONTENT)
        return Response({"message": "object does not exist"}, status=status.HTTP_400_BAD_REQUEST)


class ReviewDetailView(ListCreateAPIView):
    serializer_class = ReviewSerializer

    def get_queryset(self):
        product_id = self.kwargs['pk']
        return Review.objects.filter(product_id=product_id)


class ReviewManageView(BaseManageView):
    VIEWS_BY_METHOD = {
        'DELETE': ReviewList.as_view,
        'GET': ReviewDetailView.as_view,
        'POST': ReviewList.as_view,
    }