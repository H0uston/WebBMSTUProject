import jwt
from django.contrib import auth
from django.contrib.auth import hashers
from django.forms import model_to_dict
from rest_framework.generics import GenericAPIView
from django.conf import settings

from user.serializers import UserSerializer
from rest_framework.response import Response
from rest_framework import status

from user.models import get_default_role_id


def form_user_model(data):  # TODO to logic
    for key in list(data):  # for all keys
        data["user_" + str(key)] = data.pop(key)  # change key
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

    def post(self, request):
        data = request.data
        email = data.get('email', '')
        password = data.get('password', '')
        user = auth.authenticate(email=email, password=password)
        if user:
            auth_token = jwt.encode({'email': user.user_email}, settings.JWT_SECRET_KEY)

            serializer = UserSerializer(user)

            data = {
                'user': serializer.data,
                'token': auth_token
            }

            return Response(data, status=status.HTTP_200_OK)

        return Response({'detail': 'Invalid credentials'}, status=status.HTTP_401_UNAUTHORIZED)

