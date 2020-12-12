from django.contrib.auth import hashers
from django.db import models


def get_default_role_id():
    role = Role.objects.get(role_name="user")
    return role.role_id


class Role(models.Model):
    role_id = models.IntegerField(primary_key=True)
    role_name = models.TextField()
    role_description = models.TextField(blank=True, null=True)

    class Meta:
        db_table = 'role'


class User(models.Model):
    user_id = models.AutoField(primary_key=True)
    user_email = models.EmailField()
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
    role_id = models.ForeignKey(to=Role, on_delete=models.CASCADE, db_column='role_id')

    class Meta:
        db_table = 'user'

    def check_password(self, password):  # TODO to logic?
        return hashers.check_password(password, self.user_password)

    def is_authenticated(self):
        return True
