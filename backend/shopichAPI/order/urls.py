from django.urls import path

from order.views import OrderListView

urlpatterns = [
    path('', OrderListView.as_view({'get': 'get_orders', 'post': 'post', 'delete': 'delete'})),
    path('<int:order_id>', OrderListView.as_view({'get': 'get_order'})),
]