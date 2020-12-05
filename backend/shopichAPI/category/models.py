from django.db import models


class Category(models.Model):
    category_id = models.AutoField(primary_key=True)
    category_name = models.CharField(max_length=64)
    category_description = models.TextField(blank=True, null=True)

    class Meta:
        db_table = 'category'
