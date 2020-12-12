from django.urls import path
from .views import ReviewManageView

urlpatterns = [
    path('', ReviewManageView.as_view()),
    path('<int:review_id>', ReviewManageView.as_view()),
]