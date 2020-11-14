CREATE DATABASE shop;

CREATE TABLE role
(
    role_id integer NOT NULL,
    role_name text NOT NULL,
    role_description text,

    PRIMARY KEY (role_id)
);

/* Регистрация */
/* Вход в систему */
/* Выход из системы */
/* Редактирование информации о себе пользователем */
CREATE TABLE "user"
(
    user_id integer NOT NULL,
    user_email text NOT NULL,
    user_password text NOT NULL,
    user_phone text,
    user_name text,
    user_surname text,
    user_city text,
    user_street text,
    user_house text,
    user_flat text,
    user_index integer,
    user_birthday date,
    user_role integer NOT NULL REFERENCES role,
    CHECK(user_id > 0),
    CHECK(user_index > 0),

    PRIMARY KEY (user_id)
);

/* Просмотр списка товаров */
/* Просмотр информации о товаре */
/* Поиск товаров по названию */
CREATE TABLE product
(
    product_id integer NOT NULL,
    product_name text NOT NULL,
    product_price float NOT NULL,
    product_availability boolean NOT NULL,
    product_discount integer,

    PRIMARY KEY (product_id)
);

/* Просмотр списка категорий товаров */
CREATE TABLE category
(
    category_id integer NOT NULL,
    category_name text NOT NULL,
    category_description text,

    PRIMARY KEY (category_id)
);

CREATE TABLE categories
(
    product_id integer NOT NULL REFERENCES product,
    category_id integer NOT NULL REFERENCES category
);

CREATE TABLE "order"
(
    order_id integer NOT NULL,
    user_id integer NOT NULL REFERENCES "user",
    order_date date,
    is_approved boolean NOT NULL,

    PRIMARY KEY (order_id)
);

/* Оставление отзыва о товаре */
CREATE TABLE review
(
    review_id integer NOT NULL,
    user_id integer NOT NULL REFERENCES "user",
    product_id integer NOT NULL REFERENCES product,
    review_text text,
    review_date date NOT NULL,
    review_rating integer NOT NULL, /* from 0 to 10 */

    PRIMARY KEY (review_id)
);

/* Добавление товара в корзину */
/* Отправка заказа */
CREATE TABLE orders
(
    order_id integer NOT NULL REFERENCES "order",
    product_id integer NOT NULL REFERENCES product
);

DROP TABLE role CASCADE;
DROP TABLE "user" CASCADE;
DROP TABLE product CASCADE;
DROP TABLE category CASCADE;
DROP TABLE categories CASCADE;
DROP TABLE "order" CASCADE;
DROP TABLE review CASCADE;
DROP TABLE orders CASCADE;

