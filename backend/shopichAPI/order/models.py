from django.db import models

from product.models import Product
from user.models import User


class Order(models.Model):
    order_id = models.AutoField(primary_key=True)
    user = models.ForeignKey(to=User, on_delete=models.CASCADE)
    order_date = models.DateField(blank=True, null=True)
    is_approved = models.BooleanField()

    class Meta:
        db_table = 'order'


class Orders(models.Model):
    orders_id = models.AutoField(primary_key=True)
    order_id = models.ForeignKey(to=Order, on_delete=models.CASCADE, db_column='order_id')
    product_id = models.ForeignKey(to=Product, on_delete=models.CASCADE, db_column='product_id')
    count = models.IntegerField()

    class Meta:
        db_table = 'orders'
