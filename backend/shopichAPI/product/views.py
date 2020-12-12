from django.forms import model_to_dict
from rest_framework import status
from rest_framework.generics import ListCreateAPIView
from rest_framework.response import Response
from rest_framework.viewsets import ModelViewSet

from category.models import Categories, Category
from .models import Product
from .serializers import ProductSerializer


class ProductList(ModelViewSet):
    serializer_class = ProductSerializer

    def get_products(self, request):
        current = int(request.GET.get('current', 1))
        size = int(request.GET.get('size', 5))

        elements = Product.objects.all()

        filtered_elements = elements[(current - 1) * size:current * size]
        final_elements = []
        for element in filtered_elements.values():
            element["products"] = []
            categories_ids = Categories.objects.filter(product_id=element["product_id"])

            for categories_id in list(categories_ids.values()):
                category = Category.objects.get(category_id=categories_id["category_id"])
                element["products"].append(model_to_dict(category))

            final_elements.append(element)

        return Response(data=final_elements, status=status.HTTP_200_OK)


class ProductDetail(ModelViewSet):
    serializer_class = ProductSerializer

    def get_product(self, request, product_id):
        if not Product.objects.filter(product_id=product_id).exists():
            return Response({"messages": "Product does not exist"}, status=status.HTTP_400_BAD_REQUEST)
        product = model_to_dict(Product.objects.get(product_id=product_id))
        product["categories"] = []
        categories_ids = Categories.objects.filter(product_id=product_id)

        for categories_id in list(categories_ids.values()):
            category = Category.objects.get(category_id=categories_id["category_id"])
            product["categories"].append(model_to_dict(category))

        return Response(data=product, status=status.HTTP_200_OK)
