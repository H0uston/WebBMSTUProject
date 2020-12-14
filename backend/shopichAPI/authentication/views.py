import jwt
from django.contrib import auth
from django.contrib.auth import hashers
from django.forms import model_to_dict
from drf_yasg import openapi
from drf_yasg.utils import swagger_auto_schema
from rest_framework.decorators import action
from rest_framework.generics import GenericAPIView
from django.conf import settings

from authentication.serializers import LoginSerializer
from user.serializers import UserSerializer
from rest_framework.response import Response
from rest_framework import status

from user.models import get_default_role_id


def form_user_model(data):  # TODO to logic
    for key in list(data):  # for all keys
        data[str(key)] = data.pop(key)  # change key
    data["user_password"] = hashers.make_password(data["user_password"])
    data["role_id"] = get_default_role_id()  # adding default role
    return data


class RegisterView(GenericAPIView):
    serializer_class = UserSerializer

    def post(self, request):
        data = request.data
        formatted_data = form_user_model(data)
        serializer = UserSerializer(data=formatted_data)

        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)

        return Response(serializer.errors, status=status.HTTP_401_UNAUTHORIZED)


class LoginView(GenericAPIView):
    serializer_class = LoginSerializer

    def post(self, request):
        data = request.data
        email = data.get('user_email', '')
        password = data.get('user_password', '')
        user = auth.authenticate(email=email, password=password)
        if user:
            auth_token = jwt.encode({'user_email': user.user_email}, settings.JWT_SECRET_KEY)

            serializer = UserSerializer(user)

            data = {
                'user': serializer.data,
                'token': auth_token
            }

            return Response(data, status=status.HTTP_200_OK)

        return Response({'detail': 'Invalid credentials'}, status=status.HTTP_401_UNAUTHORIZED)

