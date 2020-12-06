from django.urls import path
from .views import UserDetailView

urlpatterns = [
    path('', UserDetailView.as_view({'get': 'get_user_details', 'put': 'change_user_details'})),
]