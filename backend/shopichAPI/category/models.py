from django.db import models

from product.models import Product


class Category(models.Model):
    category_id = models.AutoField(primary_key=True)
    category_name = models.TextField()
    category_description = models.TextField(blank=True, null=True)

    class Meta:
        db_table = 'category'


class Categories(models.Model):
    categories_id = models.AutoField(primary_key=True)
    product = models.ForeignKey(to=Product, on_delete=models.DO_NOTHING, blank=True, db_column='product_id')
    category = models.ForeignKey(to=Category, on_delete=models.DO_NOTHING, blank=True, db_column='category_id')

    class Meta:
        db_table = 'categories'
