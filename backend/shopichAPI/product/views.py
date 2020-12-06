from django.forms import model_to_dict
from rest_framework import status
from rest_framework.generics import ListCreateAPIView
from rest_framework.response import Response
from rest_framework.viewsets import ModelViewSet

from .models import Product
from .serializers import ProductSerializer
from rest_framework.permissions import IsAuthenticated
from datetime import datetime


class ProductList(ModelViewSet):
    serializer_class = ProductSerializer

    def get_products(self, request):
        current = int(request.GET.get('current', 1))
        size = int(request.GET.get('size', 5))

        elements = Product.objects.all()

        filtered_elements = elements[(current - 1) * size:current * size]

        return Response(data=list(filtered_elements.values()), status=status.HTTP_200_OK)


class ProductDetail(ListCreateAPIView):  # TODO delete method, actually, we already have it
    serializer_class = ProductSerializer

    def get_queryset(self):
        product_id = self.kwargs['product_id']
        return Product.objects.filter(product_id=product_id)
