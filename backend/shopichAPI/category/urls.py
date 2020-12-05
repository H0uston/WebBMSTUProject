from django.urls import path
from .views import CategoryList, CategoryDetailView

urlpatterns = [
    path('', CategoryList.as_view()),
    path('<int:category_id>', CategoryDetailView.as_view())
]