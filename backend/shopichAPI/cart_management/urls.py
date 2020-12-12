from django.urls import path

from cart_management.views import CartManagementView

urlpatterns = [
    path('', CartManagementView.as_view({'get': 'get_cart', 'post': 'post'})),
    path('<int:orders_id>', CartManagementView.as_view({'delete': 'delete'})),
]