from django.db import models
from django.contrib.auth.models import User


class Review(models.Model):
    user_id = models.ForeignKey(to=User, on_delete=models.CASCADE)
    product_id = models.IntegerField()  # TODO morph into foreignkey one(product) to many(reviews) btw
    review_text = models.CharField(max_length=500)
    review_rating = models.IntegerField()  # TODO don't forget about min/max value

