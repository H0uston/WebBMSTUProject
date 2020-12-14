from rest_framework import serializers

from order.models import Order


class CartSerializer(serializers.ModelSerializer):
    order_id = serializers.IntegerField()

    class Meta:
        model = Order
        fields = ['order_id']
