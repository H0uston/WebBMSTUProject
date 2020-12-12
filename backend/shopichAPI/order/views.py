from datetime import datetime

from django.forms import model_to_dict
from django.shortcuts import render
from rest_framework import status
from rest_framework.generics import ListCreateAPIView
from rest_framework.permissions import IsAuthenticated
from rest_framework.response import Response
from rest_framework.viewsets import ModelViewSet

from order.models import Order
from order.serializers import OrderSerializer
from user.models import User


class OrderListView(ModelViewSet):
    serializer_class = OrderSerializer
    permission_classes = [IsAuthenticated]

    def get_orders(self, request):
        current = int(request.GET.get('current', 1))
        size = int(request.GET.get('size', 5))

        elements = Order.objects.filter(user_id=request.user.user_id, is_approved=True)

        filtered_elements = elements[(current - 1) * size:current * size]

        return Response(data=list(filtered_elements.values()), status=status.HTTP_200_OK)

    def get_order(self, request, order_id):
        if not Order.objects.filter(order_id=order_id, user_id=request.user.user_id).exists():
            return Response({"messages": "Order does not exist"}, status=status.HTTP_400_BAD_REQUEST)
        order = model_to_dict(Order.objects.get(order_id=order_id, user_id=request.user.user_id))

        return Response(data=order, status=status.HTTP_200_OK)

    def post(self, request, *args, **kwargs):
        order = Order.objects.filter(user_id=self.request.user.user_id, is_approved=False)

        if len(order) > 1:
            print("ERROR CART HAVE MORE THAN ONE ORDER")  # TODO
            return Response(status=status.HTTP_500_INTERNAL_SERVER_ERROR)

        if order.exists():
            [order] = order
            order.is_approved = True
            order.save()

            return Response(model_to_dict(order), status=status.HTTP_201_CREATED)

        return Response(status=status.HTTP_400_BAD_REQUEST)

    def delete(self, request, order_id):  # TODO
        if Order.objects.filter(order_id=order_id, user_id=self.request.user.user_id).exists():
            Order.objects.get(order_id=order_id).delete()
            return Response(status=status.HTTP_204_NO_CONTENT)
        return Response({"message": "object does not exist"}, status=status.HTTP_400_BAD_REQUEST)

