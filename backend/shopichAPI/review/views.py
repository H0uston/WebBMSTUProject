from django.shortcuts import render
from rest_framework.generics import ListCreateAPIView, RetrieveDestroyAPIView
from .models import Review
from .serializers import ReviewSerializer
from rest_framework import permissions


class ReviewList(ListCreateAPIView):
    serializer_class = ReviewSerializer
    permission_classes = (permissions.IsAuthenticated, )

    def perform_create(self, serializer):
        serializer.save(owner=self.request.user)

    def get_queryset(self):
        return Review.objects.filter(owner=self.request.user)


class ReviewDetailView(RetrieveDestroyAPIView): # delete method
    serializer_class = ReviewSerializer
    permission_classes = (permissions.IsAuthenticated, )
    lookup_field = "id"

    def get_queryset(self):
        return Review.objects.filter(owner=self.request.user)
