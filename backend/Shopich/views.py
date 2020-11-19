from django.shortcuts import render
from django.http import HttpResponse
from rest_framework.generics import get_object_or_404
from rest_framework.response import Response
from rest_framework.views import APIView
from rest_framework.mixins import ListModelMixin
from rest_framework.generics import GenericAPIView
from rest_framework.generics import ListCreateAPIView, RetrieveUpdateDestroyAPIView
from rest_framework import permissions
from Shopich.models import User

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

class ProductAPIView(APIView):
    parameters = {"current": 1, "size": 10}

    def get(self, request, product_id=None):
        if product_id is not None:
            return self.get_specific(request, product_id)

        data = {}
        request_data = request.query_params.dict()
        for param in self.parameters:
            if param in request_data:
                data[param] = request_data[param]
            else:
                data[param] = self.parameters[param]

        # products = repositories.get_products(data) TODO
        products = [{"id": 1, "name": "Milk"}, {"id": 2, "name": "Banana"}]
        return Response({"data": products})

    def get_specific(self, request, product_id):
        print(product_id)

        # product = repositories.get_product(product_id) TODO

        product = {"id": product_id, "name": "Milk"}
        return Response({"data": product})


class CategoryAPIView(APIView):
    parameters = {"current": 1, "size": 10}

    def get(self, request, category_id=None):
        if category_id is not None:
            return self.get_specific(request, category_id)

        data = {}
        request_data = request.query_params.dict()
        for param in self.parameters:
            if param in request_data:
                data[param] = request_data[param]
            else:
                data[param] = self.parameters[param]

        # categories = repositories.get_categories(data) TODO
        categories = [{"id": 1, "name": "Fruits"}, {"id": 2, "name": "Vegetables"}]
        return Response({"data": categories})

    def get_specific(self, request, category_id):
        print(category_id)

        # category = repositories.get_category(product_id) TODO

        category = {"id": category_id, "name": "Fruit"}
        return Response({"data": category})


class ReviewAPIView(APIView):
    def get(self, request, product_id):
        # review = repositories.get_reviews(review_id) TODO
        reviews = [{"id": product_id, "name": "Anna"}, {"id": 2, "name": "Masha"}]
        return Response({"data": reviews})

    def delete(self, request, product_id):
        # repositories.delele_review(product_id)
        return Response()

    def post(self, request):
        print(request.data)
        return Response()

    def put(self, request):
        print(request.data)
        return Response()
