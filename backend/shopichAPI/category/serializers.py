from rest_framework.serializers import ListSerializer, ModelSerializer
from .models import Category


class CategorySerializer(ModelSerializer):
    class Meta:
        model = Category
        fields = ['category_id', 'category_name', 'category_description']
