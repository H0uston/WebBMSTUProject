from rest_framework.serializers import ModelSerializer
from .models import Review


class ReviewSerializer(ModelSerializer):

    class Meta:
        model = Review
        fields = ['user_id', 'product_id', 'review_text', 'review_rating']