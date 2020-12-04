import jwt
from user.models import User
from rest_framework import authentication, exceptions
from django.contrib.auth.backends import ModelBackend
from django.conf import settings


class JWTAuthentication(authentication.BaseAuthentication):
    def authenticate(self, request):
        auth_data = authentication.get_authorization_header(request)

        if not auth_data:
            return None

        prefix, token = auth_data.decode('utf-8').split(' ')

        try:
            payload = jwt.decode(token, settings.JWT_SECRET_KEY)
            user = User.objects.get(user_email=payload['email'])
            return user, token

        except jwt.DecodeError as identifier:
            raise exceptions.AuthenticationFailed('Your token is invalid, login')
        except jwt.ExpiredSignatureError as identifier:
            raise exceptions.AuthenticationFailed('Your token is expired, login')

        return super().authenticate(request)


class EmailBackend(ModelBackend):
    def authenticate(self, request, email=None, password=None, **kwargs):
        try:
            user = User.objects.get(user_email=email)
        except:  # TODO
            return None

        if user.check_password(password) and self.user_can_authenticate(user):
            return user
