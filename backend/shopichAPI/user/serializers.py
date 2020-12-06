from django.forms import model_to_dict
from rest_framework import serializers
from rest_framework.serializers import ModelSerializer
from .models import Role, User


class RoleSerializer(ModelSerializer):
    class Meta:
        model = Role
        fields = ['role_id', 'role_name', 'role_description']


class UserSerializer(ModelSerializer):
    class Meta:
        model = User
        fields = ['user_id', 'role_id', 'user_email', 'user_password', 'user_phone', 'user_name', 'user_surname', 'user_city',
                  'user_street', 'user_house', 'user_flat', 'user_index', 'user_birthday']

    def validate(self, attrs):
        email = attrs.get('user_email')
        if User.objects.filter(user_email=email).exists():  # TODO
            raise serializers.ValidationError({'user_email': ('Email is already in use')})
        return super().validate(attrs)
