from rest_framework.relations import PrimaryKeyRelatedField
from rest_framework.serializers import ListSerializer, ModelSerializer

from product.models import Product
from product.serializers import ProductSerializer
from .models import Category


class CategorySerializer(ModelSerializer):
    class Meta:
        model = Category
        fields = ['category_id', 'category_name', 'category_description']


class CategoriesSerializer(ModelSerializer):
    class Meta:
        model = Category
        fields = ['categories_id', 'product_id', 'category_id']
