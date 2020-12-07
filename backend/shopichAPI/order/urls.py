from django.urls import path

from order.views import OrderListView

urlpatterns = [
    path('', OrderListView.as_view()),
    path('<int:pk>', OrderListView.as_view()),
]