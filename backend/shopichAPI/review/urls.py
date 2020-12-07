from django.urls import path
from .views import ReviewManageView

urlpatterns = [
    path('', ReviewManageView.as_view()),
    path('<int:pk>', ReviewManageView.as_view()),
]