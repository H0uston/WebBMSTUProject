from rest_framework.serializers import ModelSerializer
from .models import Product


class ProductSerializer(ModelSerializer):
    class Meta:
        model = Product
        fields = ['product_id', 'product_name', 'product_price', 'product_availability', 'product_discount']

