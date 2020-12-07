from datetime import datetime

from django.shortcuts import render
from rest_framework import status
from rest_framework.generics import ListCreateAPIView
from rest_framework.permissions import IsAuthenticated
from rest_framework.response import Response

from order.models import Order
from order.serializers import OrderSerializer
from user.models import User


class OrderListView(ListCreateAPIView):
    serializer_class = OrderSerializer
    permission_classes = [IsAuthenticated]

    def get_queryset(self):
        current = int(self.request.GET.get('current', 1))
        size = int(self.request.GET.get('size', 5))

        user_id = self.request.user.user_id

        elements = Order.objects.filter(user=user_id)
        filtered_elements = elements[(current - 1) * size:current * size]

        return filtered_elements

    def post(self, request, *args, **kwargs):
        data = request.data
        data['user'] = request.user.user_id
        data['order_date'] = datetime.now().date()
        data['is_approved'] = False
        serializer = OrderSerializer(data=data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)

        return Response(serializer.errors, status=status.HTTP_401_UNAUTHORIZED)

    def delete(self, request, pk, format=None):
        if Order.objects.filter(order_id=pk).exists():
            Order.objects.get(order_id=pk).delete()
            return Response(status=status.HTTP_204_NO_CONTENT)
        return Response({"message": "object does not exist"}, status=status.HTTP_400_BAD_REQUEST)

