from rest_framework.serializers import ListSerializer, ModelSerializer
from .models import Order, Orders


class OrderSerializer(ModelSerializer):
    class Meta:
        model = Order
        fields = ['order_id', 'user', 'order_date', 'is_approved']


class OrdersSerializer(ModelSerializer):
    class Meta:
        model = Orders
        fields = ['orders_id', 'order_id', 'product_id', "count"]
