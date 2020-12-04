from django.shortcuts import render
from rest_framework.generics import ListCreateAPIView, RetrieveDestroyAPIView, RetrieveUpdateDestroyAPIView
from .models import Review
from .serializers import ReviewSerializer
from rest_framework import permissions


class ReviewList(ListCreateAPIView):
    serializer_class = ReviewSerializer
    permission_classes = (permissions.IsAuthenticated, )

    def perform_create(self, serializer):
        serializer.save(user_id=self.request.user)

    def get_queryset(self):  # TODO make get (AllowAny)
        return Review.objects.filter(user_id=self.request.user)


class ReviewDetailView(RetrieveUpdateDestroyAPIView):  # TODO delete method, actually, we already have it
    serializer_class = ReviewSerializer
    permission_classes = (permissions.IsAuthenticated, )
    lookup_field = "id"

    def get_queryset(self):
        return Review.objects.filter(user_id=self.request.user)
