import jwt

from rest_framework import status
from rest_framework.permissions import IsAuthenticated
from rest_framework.response import Response
from rest_framework.viewsets import ModelViewSet
from django.forms.models import model_to_dict

from shopichAPI import settings
from user.models import User
from user.serializers import UserSerializer


def update_user(user, updated_user):  # TODO to logic
    for key in updated_user:
        if key == "email" and user.user_email != updated_user["email"]:  # can't change password!
            if User.objects.filter(user_email=updated_user["email"]).exists():  # TODO
                raise TypeError({'user_email': ('Email is already in use')})
        setattr(user, "user_" + key, updated_user[key])

    auth_token = jwt.encode({'email': user.user_email}, settings.JWT_SECRET_KEY)

    return auth_token


class UserDetailView(ModelViewSet):
    serializer_class = UserSerializer
    permission_classes = [IsAuthenticated]

    def get_user_details(self, request):
        try:
            user = model_to_dict(User.objects.get(user_id=request.user.user_id))
        except:
            return Response(status=status.HTTP_401_UNAUTHORIZED)

        return Response(user, status=status.HTTP_201_CREATED)

    def change_user_details(self, request):
        try:
            user = User.objects.get(user_id=request.user.user_id)
        except:
            return Response(status=status.HTTP_401_UNAUTHORIZED)

        try:  # TODO to logic
            auth_token = update_user(user, request.data)
        except TypeError as e:
            return Response({'user_email': ('Email is already in use')}, status=status.HTTP_400_BAD_REQUEST)
        user.save()
        data = {
            'user': model_to_dict(user),
            'token': auth_token
        }
        return Response(data, status=status.HTTP_202_ACCEPTED)
