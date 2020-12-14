from rest_framework import serializers

from user.models import User


class LoginSerializer(serializers.ModelSerializer):
    user_password = serializers.CharField(
        max_length=65, min_length=8, write_only=True
    )
    user_email = serializers.EmailField(
        min_length=5,
        max_length=60
    )

    class Meta:
        model = User
        fields = ['user_email', 'user_password']

