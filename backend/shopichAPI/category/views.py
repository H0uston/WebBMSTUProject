from django.core.serializers import serialize
from django.http import JsonResponse
from django.shortcuts import render
from rest_framework import status
from rest_framework.generics import ListCreateAPIView, RetrieveDestroyAPIView, RetrieveUpdateDestroyAPIView
from rest_framework.response import Response

from .models import Category
from .serializers import CategorySerializer
from rest_framework.permissions import IsAuthenticated
from user.models import User
from product.models import Product
from datetime import datetime


class CategoryList(ListCreateAPIView):
    serializer_class = CategorySerializer

    def get_queryset(self):
        return Category.objects.filter()


class CategoryDetailView(ListCreateAPIView):  # TODO delete method, actually, we already have it
    serializer_class = CategorySerializer

    def get_queryset(self):
        category_id = self.kwargs['category_id']
        return Category.objects.filter(category_id=category_id)
