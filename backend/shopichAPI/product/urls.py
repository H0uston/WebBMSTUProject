from django.conf.urls import url
from django.urls import path, re_path
from .views import ProductList, ProductDetail

urlpatterns = [
    path('', ProductList.as_view({'get': 'get_products'})),
    path('<int:product_id>', ProductDetail.as_view())
]