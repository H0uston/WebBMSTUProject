from rest_framework.generics import ListCreateAPIView

from .models import Category
from .serializers import CategorySerializer


class CategoryList(ListCreateAPIView):
    serializer_class = CategorySerializer

    def get_queryset(self):
        return Category.objects.filter()


class CategoryDetailView(ListCreateAPIView):  # TODO delete method, actually, we already have it
    serializer_class = CategorySerializer

    def get_queryset(self):
        category_id = self.kwargs['category_id']
        return Category.objects.filter(category_id=category_id)
