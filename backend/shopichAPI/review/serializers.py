from rest_framework.serializers import ListSerializer, ModelSerializer
from .models import Review


class ReviewSerializer(ModelSerializer):
    class Meta:
        model = Review
        fields = ['review_id', 'user_id', 'product_id', 'review_text', 'review_rating', 'review_date']

