from django.forms import model_to_dict
from rest_framework import status
from rest_framework.generics import ListCreateAPIView
from rest_framework.response import Response
from rest_framework.viewsets import ModelViewSet

from product.models import Product
from .models import Category, Categories
from .serializers import CategorySerializer


class CategoryList(ModelViewSet):
    serializer_class = CategorySerializer

    def get_categories(self, request):
        current = int(request.GET.get('current', 1))
        size = int(request.GET.get('size', 5))

        elements = Category.objects.all()

        filtered_elements = elements[(current - 1) * size:current * size]

        return Response(data=list(filtered_elements.values()), status=status.HTTP_200_OK)


class CategoryDetailView(ModelViewSet):
    serializer_class = CategorySerializer

    def get_category(self, request,  category_id):
        if not Category.objects.filter(category_id=category_id).exists():
            return Response({"messages": "Category does not exist"}, status=status.HTTP_400_BAD_REQUEST)
        category = model_to_dict(Category.objects.get(category_id=category_id))
        category["products"] = []
        categories_ids = Categories.objects.filter(category_id=category_id)

        for categories_id in list(categories_ids.values()):
            product = Product.objects.get(product_id=categories_id["product_id"])
            category["products"].append(model_to_dict(product))

        return Response(data=category, status=status.HTTP_200_OK)

