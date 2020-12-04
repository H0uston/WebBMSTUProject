import jwt
from django.contrib import auth
from django.shortcuts import render
from rest_framework.generics import GenericAPIView
from django.conf import settings

from .serializers import UserSerializer
from rest_framework.response import Response
from rest_framework import status, authentication


class RegisterView(GenericAPIView):
    serializer_class = UserSerializer

    def post(self, request):
        serializer = UserSerializer(data=request.data)

        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)

        return Response(serializer.errors, status=status.HTTP_401_UNAUTHORIZED)


class LoginView(GenericAPIView):

    def post(self, request):
        data = request.data
        username = data.get('username', '')
        password = data.get('password', '')
        # print("auth_data = " + str(authentication.get_authorization_header(request)))
        user = auth.authenticate(username=username, password=password)
        '''
        print("auth_data = " + str(authentication.get_authorization_header(request)))
        print("user = " + str(auth.authenticate(username=username, password=password)))
        print("user.username = " + user.username)
        '''
        if user:
            auth_token = jwt.encode({'username': user.username}, settings.JWT_SECRET_KEY)

            serializer = UserSerializer(user)

            data = {
                'user': serializer.data,
                'token': auth_token
            }

            return Response(data, status=status.HTTP_200_OK)

        return Response({'detail': 'Invalid credentials'}, status=status.HTTP_401_UNAUTHORIZED)
