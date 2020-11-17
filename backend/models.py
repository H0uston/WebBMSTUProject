# This is an auto-generated Django model module.
# You'll have to do the following manually to clean this up:
#   * Rearrange models' order
#   * Make sure each model has one field with primary_key=True
#   * Make sure each ForeignKey and OneToOneField has `on_delete` set to the desired behavior
#   * Remove `managed = False` lines if you wish to allow Django to create, modify, and delete the table
# Feel free to rename the models, but don't rename db_table values or field names.
from django.db import models


class AuthGroup(models.Model):
    name = models.CharField(unique=True, max_length=150)

    class Meta:
        managed = False
        db_table = 'auth_group'


class AuthGroupPermissions(models.Model):
    group = models.ForeignKey(AuthGroup, models.DO_NOTHING)
    permission = models.ForeignKey('AuthPermission', models.DO_NOTHING)

    class Meta:
        managed = False
        db_table = 'auth_group_permissions'
        unique_together = (('group', 'permission'),)


class AuthPermission(models.Model):
    name = models.CharField(max_length=255)
    content_type = models.ForeignKey('DjangoContentType', models.DO_NOTHING)
    codename = models.CharField(max_length=100)

    class Meta:
        managed = False
        db_table = 'auth_permission'
        unique_together = (('content_type', 'codename'),)


class AuthUser(models.Model):
    password = models.CharField(max_length=128)
    last_login = models.DateTimeField(blank=True, null=True)
    is_superuser = models.BooleanField()
    username = models.CharField(unique=True, max_length=150)
    first_name = models.CharField(max_length=150)
    last_name = models.CharField(max_length=150)
    email = models.CharField(max_length=254)
    is_staff = models.BooleanField()
    is_active = models.BooleanField()
    date_joined = models.DateTimeField()

    class Meta:
        managed = False
        db_table = 'auth_user'


class AuthUserGroups(models.Model):
    user = models.ForeignKey(AuthUser, models.DO_NOTHING)
    group = models.ForeignKey(AuthGroup, models.DO_NOTHING)

    class Meta:
        managed = False
        db_table = 'auth_user_groups'
        unique_together = (('user', 'group'),)


class AuthUserUserPermissions(models.Model):
    user = models.ForeignKey(AuthUser, models.DO_NOTHING)
    permission = models.ForeignKey(AuthPermission, models.DO_NOTHING)

    class Meta:
        managed = False
        db_table = 'auth_user_user_permissions'
        unique_together = (('user', 'permission'),)


class Categories(models.Model):
    product = models.ForeignKey('Product', models.DO_NOTHING, blank=True, null=True)
    category = models.ForeignKey('Category', models.DO_NOTHING, blank=True, null=True)

    class Meta:
        managed = False
        db_table = 'categories'


class Category(models.Model):
    category_id = models.IntegerField(primary_key=True)
    category_name = models.TextField()
    category_description = models.TextField(blank=True, null=True)

    class Meta:
        managed = False
        db_table = 'category'


class DjangoAdminLog(models.Model):
    action_time = models.DateTimeField()
    object_id = models.TextField(blank=True, null=True)
    object_repr = models.CharField(max_length=200)
    action_flag = models.SmallIntegerField()
    change_message = models.TextField()
    content_type = models.ForeignKey('DjangoContentType', models.DO_NOTHING, blank=True, null=True)
    user = models.ForeignKey(AuthUser, models.DO_NOTHING)

    class Meta:
        managed = False
        db_table = 'django_admin_log'


class DjangoContentType(models.Model):
    app_label = models.CharField(max_length=100)
    model = models.CharField(max_length=100)

    class Meta:
        managed = False
        db_table = 'django_content_type'
        unique_together = (('app_label', 'model'),)


class DjangoMigrations(models.Model):
    app = models.CharField(max_length=255)
    name = models.CharField(max_length=255)
    applied = models.DateTimeField()

    class Meta:
        managed = False
        db_table = 'django_migrations'


class DjangoSession(models.Model):
    session_key = models.CharField(primary_key=True, max_length=40)
    session_data = models.TextField()
    expire_date = models.DateTimeField()

    class Meta:
        managed = False
        db_table = 'django_session'


class Order(models.Model):
    order_id = models.IntegerField(primary_key=True)
    user = models.ForeignKey('User', models.DO_NOTHING)
    order_date = models.DateField(blank=True, null=True)
    is_approved = models.BooleanField()

    class Meta:
        managed = False
        db_table = 'order'


class Orders(models.Model):
    order = models.ForeignKey(Order, models.DO_NOTHING)
    product = models.ForeignKey('Product', models.DO_NOTHING)
    count = models.IntegerField()

    class Meta:
        managed = False
        db_table = 'orders'


class Product(models.Model):
    product_id = models.IntegerField(primary_key=True)
    product_name = models.TextField()
    product_price = models.FloatField()
    product_availability = models.BooleanField()
    product_discount = models.IntegerField(blank=True, null=True)

    class Meta:
        managed = False
        db_table = 'product'


class Review(models.Model):
    review_id = models.IntegerField(primary_key=True)
    user = models.ForeignKey('User', models.DO_NOTHING)
    product = models.ForeignKey(Product, models.DO_NOTHING)
    review_text = models.TextField(blank=True, null=True)
    review_date = models.DateField()
    review_rating = models.IntegerField()

    class Meta:
        managed = False
        db_table = 'review'


class Role(models.Model):
    role_id = models.IntegerField(primary_key=True)
    role_name = models.TextField()
    role_description = models.TextField(blank=True, null=True)

    class Meta:
        managed = False
        db_table = 'role'


class User(models.Model):
    user_id = models.IntegerField(primary_key=True)
    user_email = models.TextField()
    user_password = models.TextField()
    user_phone = models.TextField(blank=True, null=True)
    user_name = models.TextField(blank=True, null=True)
    user_surname = models.TextField(blank=True, null=True)
    user_city = models.TextField(blank=True, null=True)
    user_street = models.TextField(blank=True, null=True)
    user_house = models.TextField(blank=True, null=True)
    user_flat = models.TextField(blank=True, null=True)
    user_index = models.IntegerField(blank=True, null=True)
    user_birthday = models.DateField(blank=True, null=True)
    user_role = models.ForeignKey(Role, models.DO_NOTHING, db_column='user_role')

    class Meta:
        managed = False
        db_table = 'user'
