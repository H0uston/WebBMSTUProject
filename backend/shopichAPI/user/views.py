import jwt
from django.contrib.auth.hashers import make_password

from rest_framework import status
from rest_framework.permissions import IsAuthenticated
from rest_framework.response import Response
from rest_framework.viewsets import ModelViewSet
from django.forms.models import model_to_dict

from shopichAPI import settings
from user.models import User
from user.serializers import UserSerializer


def update_user(user, updated_user):  # TODO to logic
    fields = ['email', 'old_password', 'phone', 'name', 'surname', 'city',
                  'street', 'house', 'flat', 'index', 'birthday']
    for key in updated_user:
        if not key in fields or updated_user[key] == "":
            continue

        if key == "email" and user.user_email != updated_user["email"]:  # can't change password!
            if User.objects.filter(user_email=updated_user["email"]).exists():  # TODO
                raise TypeError({'email': ('Email is already in use')})
        if key == "old_password" and "new_password" in updated_user:
            if user.check_password(updated_user["old_password"]):
                print("CHANGED")
                setattr(user, "user_" + key[4:], make_password(updated_user["new_password"]))
                continue
            else:
                raise ValueError({'password': ('Wrong password')})

        setattr(user, "user_" + key, updated_user[key])

    auth_token = jwt.encode({'email': user.user_email}, settings.JWT_SECRET_KEY)

    return auth_token


def change_user_to_api(user):
    new_user = {}
    for key in user:
        if key == "role_id":
            continue
        new_user[key[5:]] = user[key]
    del new_user['password']

    return new_user


class UserDetailView(ModelViewSet):
    serializer_class = UserSerializer
    permission_classes = [IsAuthenticated]

    def get_user_details(self, request):
        try:
            user = change_user_to_api(model_to_dict(User.objects.get(user_id=request.user.user_id)))
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
            return Response({'email': ('Email is already in use')}, status=status.HTTP_400_BAD_REQUEST)
        except ValueError as e:
            return Response({'wrong_password': ('Wrong password')}, status=status.HTTP_400_BAD_REQUEST)
        user.save()

        data = {
            'user': change_user_to_api(model_to_dict(user)),
            'token': auth_token
        }

        return Response(data, status=status.HTTP_202_ACCEPTED)
