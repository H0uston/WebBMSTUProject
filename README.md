### Цель работы, решаемая проблема/предоставляемая возможность
Создать интернет магазин
### Краткий перечень функциональных требований
* Регистрация
* Вход в систему
* Выход из системы
* Просмотр списка товаров
* Добавление товара в корзину
* Отправка заказа
* Редактирование информации о себе пользователем
* Просмотр информации о товаре
* Просмотр списка категорий товаров
* Оставление отзыва о товаре
* Поиск товаров по названию
* Поиск товаров по категории
### Use-case диаграмма
![Use-case.png](/images/Use-case.png)
### ER-диаграмма
![ER.png](/images/ER.png)
### Технологический стек
#### Frontend
* Javascript / Typescript
* React
* Redux
#### Backend
* Django
* PostgreSQL
### Схема базы данных
![Database schema.png](/images/Database_schema.png)

### MoodBoard
https://www.behance.net/collection/180954729/WEB-project

### Макет пользовательского интерфейса в Figma
https://www.figma.com/proto/S9dUjadAPo6xLq7oz1p00j/Task-3?node-id=1%3A2&viewport=258%2C353%2C0.2662522792816162&scaling=scale-down-width

### Отчёт по Apache

#### Без балансировки
./ab.exe -n 10000 -c 10 http://localhost:443/api/v1/products/
This is ApacheBench, Version 2.3 <$Revision: 1879490 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 1000 requests
Completed 2000 requests
Completed 3000 requests
Completed 4000 requests
Completed 5000 requests
Completed 6000 requests
Completed 7000 requests
Completed 8000 requests
Completed 9000 requests
Completed 10000 requests
Finished 10000 requests


Server Software:        nginx/1.18.0
Server Hostname:        localhost
Server Port:            443

Document Path:          /api/v1/products/
Document Length:        4137 bytes

Concurrency Level:      10
Time taken for tests:   6.184 seconds
Complete requests:      10000
Failed requests:        0
Total transferred:      43030000 bytes
HTML transferred:       41370000 bytes
Requests per second:    1617.12 [#/sec] (mean)
Time per request:       6.184 [ms] (mean)
Time per request:       0.618 [ms] (mean, across all concurrent requests)
Transfer rate:          6795.39 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.2      0       1
Processing:     2    4   2.1      4      86
Waiting:        2    4   2.0      4      85
Total:          2    4   2.0      4      86

Percentage of the requests served within a certain time (ms)
  50%      4
  66%      4
  75%      4
  80%      4
  90%      5
  95%      5
  98%      5
  99%      6
 100%     86 (longest request)

#### C балансировкой
 ./ab.exe -n 10000 -c 10 http://localhost:443/api/v1/products/
This is ApacheBench, Version 2.3 <$Revision: 1879490 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 1000 requests
Completed 2000 requests
Completed 3000 requests
Completed 4000 requests
Completed 5000 requests
Completed 6000 requests
Completed 7000 requests
Completed 8000 requests
Completed 9000 requests
Completed 10000 requests
Finished 10000 requests


Server Software:        nginx/1.18.0
Server Hostname:        localhost
Server Port:            443

Document Path:          /api/v1/products/
Document Length:        4137 bytes

Concurrency Level:      10
Time taken for tests:   6.764 seconds
Complete requests:      10000
Failed requests:        0
Total transferred:      43030000 bytes
HTML transferred:       41370000 bytes
Requests per second:    1619.33 [#/sec] (mean)
Time per request:       6.228 [ms] (mean)
Time per request:       0.777 [ms] (mean, across all concurrent requests)
Transfer rate:          6834.43 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.2      0       2
Processing:     2    4   2.1      5      94
Waiting:        2    4   2.0      5      94
Total:          2    4   2.0      6      95

Percentage of the requests served within a certain time (ms)
  50%      5
  66%      5
  75%      5
  80%      6
  90%      6
  95%      6
  98%      6
  99%      7
 100%     96 (longest request)
