from rest_framework import serializers
from Shopich import models

class OrderSerializer(serializers.ModelSerializer):
    class Meta:
        model = models.Order
        fields = ('order_id', 'user', 'order_date', 'is_approved')

class UserSerializer(serializers.ModelSerializer):
    class Meta:
        model = models.User
        fields = ('user_id', 'user_email', 'user_password',
                  'user_phone', 'user_name', 'user_surname',
                  'user_city', 'user_street', 'user_house',
                  'user_flat', 'user_index', 'user_birthday',
                  'user_role')


