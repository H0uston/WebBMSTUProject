from django.db import models
from user.models import User
from product.models import Product


class Review(models.Model):
    review_id = models.AutoField(primary_key=True)
    user_id = models.ForeignKey(to=User, on_delete=models.DO_NOTHING, db_column='user_id')
    product_id = models.ForeignKey(to=Product, on_delete=models.DO_NOTHING, db_column='product_id')
    review_text = models.TextField(blank=True, null=True)
    review_date = models.DateField()
    review_rating = models.IntegerField()

    class Meta:
        db_table = 'review'

