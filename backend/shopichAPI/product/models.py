from django.db import models


class Product(models.Model):
    product_id = models.IntegerField(primary_key=True)
    product_name = models.TextField()
    product_price = models.FloatField()
    product_availability = models.BooleanField()
    product_discount = models.IntegerField(blank=True, null=True)

    class Meta:
        db_table = 'product'
