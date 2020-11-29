from django.urls import path
from .views import ReviewList, ReviewDetailView

urlpatterns = [
    path('', ReviewList.as_view()),
    path('<int:id>', ReviewDetailView.as_view())
]