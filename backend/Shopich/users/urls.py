from django.conf.urls import url
from django.urls import path

from .views import CreateUserAPIView, UserRetrieveUpdateAPIView, authenticate_user

urlpatterns = [
    url(r'^create/$', CreateUserAPIView.as_view()),
    url(r'^update/$', UserRetrieveUpdateAPIView.as_view()),
    path('obtain_token/', authenticate_user),
]