from rest_framework.serializers import ListSerializer, ModelSerializer
from .models import Order


class OrderSerializer(ModelSerializer):
    class Meta:
        model = Order
        fields = ['order_id', 'user', 'order_date', 'is_approved']
