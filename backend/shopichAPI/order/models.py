from django.db import models

from user.models import User


class Order(models.Model):
    order_id = models.AutoField(primary_key=True)
    user = models.ForeignKey(to=User, on_delete=models.DO_NOTHING)
    order_date = models.DateField(blank=True, null=True)
    is_approved = models.BooleanField()

    class Meta:
        db_table = 'order'
