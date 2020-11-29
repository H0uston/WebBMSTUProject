from rest_framework.serializers import ModelSerializer
from .models import Review


class ReviewSerializer(ModelSerializer):

    class Meta:
        model = Review
        fields = ['country_code', 'first_name', 'last_name', 'phone_number']