CREATE DATABASE shop;

CREATE TABLE role
(
    role_id integer NOT NULL,
    role_name text NOT NULL,
    role_description text,

    PRIMARY KEY (role_id)
);

INSERT INTO role VALUES (1, 'admin', 'administrator');
INSERT INTO role VALUES (2, 'moderator', 'moderator');
INSERT INTO role VALUES (3, 'user', 'user');

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
    role_id integer NOT NULL REFERENCES role,
    CHECK(user_id > 0),
    CHECK(user_index > 0),

    PRIMARY KEY (user_id)
);

INSERT INTO "user" VALUES (1, 'a@mail.ru', 'a', '88005553535', 'Peter', 'Vdovin', 'Moscow', 'Lenina', '22', '8', '14088', '1999-08-30', 1);
INSERT INTO "user" VALUES (2, 'b@mail.ru', 'b', '88001234567', 'Zeynal', 'Zeynalov', 'Derbent', 'Gagarina', '67', '1', '88041', '2000-03-25', 2);
INSERT INTO "user" VALUES (3, 'c@mail.ru', 'c', '88002345678', 'Azamat', 'Sanginov', 'Asia', 'Hromosom', '47', '1337', '10101', '2010-04-01', 2);
INSERT INTO "user" VALUES (4, 'd@mail.ru', 'd', '88003456789', 'George', 'Kornilaev', 'Is-Swieqi', 'Triq Il-Terz', '89', '135', '80808', '1999-11-12', 3);
INSERT INTO "user" VALUES (5, 'e@mail.ru', 'e', '88004567890', 'Eugenie', 'Ulasik', 'Ankara', 'Pushkina', '45', '54', '70707', '1999-04-09', 3);
INSERT INTO "user" VALUES (6, 'f@mail.ru', 'f', '88005678901', 'Konstantin', 'Golik', 'Berlin', 'Kolotushkina', '65', '34', '80808', '2000-01-01', 3);

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

INSERT INTO product VALUES (1, 'Belarussian Potato', 11.11, true, 0);
INSERT INTO product VALUES (2, 'Samara Tomato', 22.22, true, 20);
INSERT INTO product VALUES (3, 'Greek Olives', 100, false, 10);
INSERT INTO product VALUES (4, 'Pork', 250, true, 0);
INSERT INTO product VALUES (5, 'Chicken', 500.50, true, 0);
INSERT INTO product VALUES (6, 'Milk', 45, false, 5);
INSERT INTO product VALUES (7, 'Ural Potato', 70, false, 7);
INSERT INTO product VALUES (8, 'Kefir', 77.4, true, 0);
INSERT INTO product VALUES (9, 'Salmon', 540, true, 15);
INSERT INTO product VALUES (10, 'Mexican Apple', 455, false, 5);
INSERT INTO product VALUES (11, 'Korean Pear', 332, true, 0);

/* Просмотр списка категорий товаров */
CREATE TABLE category
(
    category_id integer NOT NULL,
    category_name text NOT NULL,
    category_description text,

    PRIMARY KEY (category_id)
);

INSERT INTO category VALUES (1, 'Vegetables', 'Vegetables');
INSERT INTO category VALUES (2, 'Fruits', 'Fruits');
INSERT INTO category VALUES (3, 'Meat', 'Meat');
INSERT INTO category VALUES (4, 'Fish', 'Fish');
INSERT INTO category VALUES (5, 'Dairy', 'Dairy');

CREATE TABLE categories
(
    product_id integer NOT NULL REFERENCES product,
    category_id integer NOT NULL REFERENCES category
);

INSERT INTO categories VALUES (1, 1);
INSERT INTO categories VALUES (2, 1);
INSERT INTO categories VALUES (3, 1);
INSERT INTO categories VALUES (4, 3);
INSERT INTO categories VALUES (5, 3);
INSERT INTO categories VALUES (6, 5);
INSERT INTO categories VALUES (7, 1);
INSERT INTO categories VALUES (8, 5);
INSERT INTO categories VALUES (9, 4);
INSERT INTO categories VALUES (10, 2);
INSERT INTO categories VALUES (11, 2);

CREATE TABLE "order"
(
    order_id integer NOT NULL,
    user_id integer NOT NULL REFERENCES "user",
    order_date date,
    is_approved boolean NOT NULL,

    PRIMARY KEY (order_id)
);

INSERT INTO "order" VALUES (1, 4, '2020-11-11', true);
INSERT INTO "order" VALUES (2, 4, '2020-11-12', false);
INSERT INTO "order" VALUES (3, 5, '2020-10-10', true);
INSERT INTO "order" VALUES (4, 5, '2020-09-09', false);
INSERT INTO "order" VALUES (5, 5, '2020-08-08', true);
INSERT INTO "order" VALUES (6, 5, '2020-10-11', false);

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

INSERT INTO review VALUES (1, 4, 2, 'Замечательные помидоры. Вся семья пришла есть. За уши не оттянешь.', '2020-11-11', 3);
INSERT INTO review VALUES (2, 4, 4, 'Вай мясо пожарили. Сочное, вкусное, пальчики оближешь.', '2020-11-12', 7);
INSERT INTO review VALUES (3, 4, 2, 'Ауф памидорычи.', '2020-11-14', 9);


/* Добавление товара в корзину */
/* Отправка заказа */
CREATE TABLE orders
(
    order_id integer NOT NULL REFERENCES "order",
    product_id integer NOT NULL REFERENCES product,
    count integer NOT NULL
);

INSERT INTO orders VALUES (2, 2, 24);
INSERT INTO orders VALUES (2, 4, 1);
INSERT INTO orders VALUES (2, 5, 63);
INSERT INTO orders VALUES (6, 6, 4);
INSERT INTO orders VALUES (6, 8, 7);
INSERT INTO orders VALUES (6, 9, 16);
INSERT INTO orders VALUES (6, 11, 65);

DROP TABLE role CASCADE;
DROP TABLE "user" CASCADE;
DROP TABLE product CASCADE;
DROP TABLE category CASCADE;
DROP TABLE categories CASCADE;
DROP TABLE "order" CASCADE;
DROP TABLE review CASCADE;
DROP TABLE orders CASCADE;

