from datetime import datetime

from django.forms import model_to_dict
from rest_framework import status
from rest_framework.permissions import IsAuthenticated
from rest_framework.response import Response
from rest_framework.viewsets import ModelViewSet

from cart_management.serializers import CartSerializer
from order.models import Order, Orders
from order.serializers import OrderSerializer, OrdersSerializer
from product.models import Product


class CartManagementView(ModelViewSet):
    permission_classes = [IsAuthenticated]
    serializer_class = CartSerializer

    def get_cart(self, request):
        current = int(self.request.GET.get('current', 1))
        size = int(self.request.GET.get('size', 5))

        order = Order.objects.filter(user_id=self.request.user.user_id, is_approved=False)

        if not order.exists():
            return Response(data=[], status=status.HTTP_400_BAD_REQUEST)

        if len(order) > 1:
            print("ERROR CART HAVE MORE THAN ONE ORDER") # TODO
            return Response(status=status.HTTP_500_INTERNAL_SERVER_ERROR)

        [order] = order

        product_ids = Orders.objects.filter(order_id=order.order_id)
        final_result = []

        for product_id in list(product_ids.values()):
            print("Product_id", product_id)
            product = Product.objects.get(product_id=product_id["product_id_id"]) # TODO
            final_result.append({"product": model_to_dict(product), "count": product_id["count"]})

        filtered_result = final_result[(current - 1) * size:current * size]

        return Response(data=filtered_result, status=status.HTTP_200_OK)

    def post(self, request, *args, **kwargs):
        order = Order.objects.filter(user_id=self.request.user.user_id, is_approved=False)

        if len(order) > 1:
            print("ERROR CART HAVE MORE THAN ONE ORDER")  # TODO
            return Response(status=status.HTTP_500_INTERNAL_SERVER_ERROR)

        if not order.exists():
            order_serializer = OrderSerializer(data={
                "user": request.user.user_id,
                "order_date": datetime.now().date(),
                "is_approved": False
            })
            if order_serializer.is_valid():
                order = order_serializer.save()
        else:
            [order] = order

        result = {"order_id": order.order_id,
                  "product_id": request.data["product_id"],
                  "count": request.data["count"]}

        serializer = OrdersSerializer(data=result)

        if serializer.is_valid():
            serializer.save()
            return Response(data=result, status=status.HTTP_201_CREATED)

        return Response(status=status.HTTP_500_INTERNAL_SERVER_ERROR)

    def patch(self, request):
        order = Order.objects.filter(user_id=self.request.user.user_id, is_approved=False)

        if not order.exists() or len(order) > 1:
            print("ERROR CART HAVE MORE THAN ONE ORDER")  # TODO
            return Response(status=status.HTTP_500_INTERNAL_SERVER_ERROR)

        [order] = order

        orders = Orders.objects.get(order_id=order.order_id, product_id=request.data["product_id"])
        orders.count = request.data["count"]
        orders.save()

        return Response(data=request.data["count"], status=status.HTTP_202_ACCEPTED)

    def delete(self, request, orders_id):
        if Orders.objects.filter(orders_id=orders_id).exists():
            Orders.objects.get(orders_id=orders_id).delete()
            return Response(status=status.HTTP_204_NO_CONTENT)
        return Response({"message": "object does not exist"}, status=status.HTTP_400_BAD_REQUEST)



