from django.shortcuts import render
from django.http import HttpResponse
from rest_framework.generics import get_object_or_404
from rest_framework.response import Response
from rest_framework.views import APIView
from rest_framework.mixins import ListModelMixin
from rest_framework.generics import GenericAPIView
from rest_framework.generics import ListCreateAPIView, RetrieveUpdateDestroyAPIView

from Shopich import models, serializers

# Create your views here.

def index(request):
    return HttpResponse("<h1>Index</h1>")

class OrderView(ListCreateAPIView):
    queryset = models.Order.objects.all()
    serializer_class = serializers.OrderSerializer

    def perform_create(self, serializer):
        user = get_object_or_404(models.User, user_id=self.request.data.get('user'))
        return serializer.save(user = user)

class SingleOrderView(RetrieveUpdateDestroyAPIView):
    queryset = models.Order.objects.all()
    serializer_class = serializers.OrderSerializer

class UserView(ListCreateAPIView):
    queryset = models.User.objects.all()
    serializer_class = serializers.UserSerializer

    def perform_create(self, serializer):
        user_role = get_object_or_404(models.Role, role_id=self.request.data.get('user_role'))
        return serializer.save(user_role = user_role)

class SingleUserView(RetrieveUpdateDestroyAPIView):
    queryset = models.User.objects.all()
    serializer_class = serializers.UserSerializer
