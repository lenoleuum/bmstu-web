# Нагрузочное тестирование с использованием ApacheBenchmark

## Без балансировки

```
ab -n 100000 -c 100 http://localhost/api/v1/users
```
```
This is ApacheBench, Version 2.3 <$Revision: 1901567 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 10000 requests
Completed 20000 requests
Completed 30000 requests
Completed 40000 requests
Completed 50000 requests
Completed 60000 requests
Completed 70000 requests
Completed 80000 requests
Completed 90000 requests
Completed 100000 requests
Finished 100000 requests


Server Software:        nginx/1.22.0
Server Hostname:        localhost
Server Port:            80

Document Path:          /api/v1/types
Document Length:        42 bytes

Concurrency Level:      100
Time taken for tests:   207.819 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      19600000 bytes
HTML transferred:       4200000 bytes
Requests per second:    481.19 [#/sec] (mean)
Time per request:       207.819 [ms] (mean)
Time per request:       2.078 [ms] (mean, across all concurrent requests)
Transfer rate:          92.10 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   0.4      1      24
Processing:     6  205 134.9    199    1633
Waiting:        5  203 134.8    198    1632
Total:          6  206 134.9    200    1633

Percentage of the requests served within a certain time (ms)
  50%    200
  66%    272
  75%    310
  80%    331
  90%    374
  95%    401
  98%    448
  99%    531
 100%   1633 (longest request)
 ```
 ```
ab -n 100000 -c 150 http://localhost/api/v1/users
``` 
```
This is ApacheBench, Version 2.3 <$Revision: 1901567 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 10000 requests
Completed 20000 requests
Completed 30000 requests
Completed 40000 requests
Completed 50000 requests
Completed 60000 requests
Completed 70000 requests
Completed 80000 requests
Completed 90000 requests
Completed 100000 requests
Finished 100000 requests


Server Software:        nginx/1.22.0
Server Hostname:        localhost
Server Port:            80

Document Path:          /api/v1/types
Document Length:        42 bytes

Concurrency Level:      150
Time taken for tests:   224.952 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      19600000 bytes
HTML transferred:       4200000 bytes
Requests per second:    444.54 [#/sec] (mean)
Time per request:       337.428 [ms] (mean)
Time per request:       2.250 [ms] (mean, across all concurrent requests)
Transfer rate:          85.09 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   0.4      1      23
Processing:     6  333 217.6    314    1943
Waiting:        5  332 217.5    312    1941
Total:          7  334 217.6    315    1944

Percentage of the requests served within a certain time (ms)
  50%    315
  66%    428
  75%    498
  80%    536
  90%    617
  95%    674
  98%    769
  99%    864
 100%   1944 (longest request)
```
 
 ## С балансировкой (3 бэкенда в соотношении 2:1:1 первый бэкенд - основной)
 
```
ab -n 100000 -c 100 http://localhost/api/v1/users
``` 
```
This is ApacheBench, Version 2.3 <$Revision: 1901567 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 10000 requests
Completed 20000 requests
Completed 30000 requests
Completed 40000 requests
Completed 50000 requests
Completed 60000 requests
Completed 70000 requests
Completed 80000 requests
Completed 90000 requests
Completed 100000 requests
Finished 100000 requests


Server Software:        nginx/1.22.0
Server Hostname:        localhost
Server Port:            80

Document Path:          /api/v1/types
Document Length:        42 bytes

Concurrency Level:      100
Time taken for tests:   199.912 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      19600000 bytes
HTML transferred:       4200000 bytes
Requests per second:    500.22 [#/sec] (mean)
Time per request:       199.912 [ms] (mean)
Time per request:       1.999 [ms] (mean, across all concurrent requests)
Transfer rate:          95.75 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   0.5      1      28
Processing:     6  197 195.3     90    1089
Waiting:        6  195 195.2     88    1089
Total:          6  198 195.3     91    1090

Percentage of the requests served within a certain time (ms)
  50%     91
  66%    233
  75%    326
  80%    384
  90%    519
  95%    604
  98%    669
  99%    710
 100%   1090 (longest request)
 ```
 ```
ab -n 100000 -c 100 http://localhost/api/v1/users
``` 
```
This is ApacheBench, Version 2.3 <$Revision: 1901567 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 10000 requests
Completed 20000 requests
Completed 30000 requests
Completed 40000 requests
Completed 50000 requests
Completed 60000 requests
Completed 70000 requests
Completed 80000 requests
Completed 90000 requests
Completed 100000 requests
Finished 100000 requests


Server Software:        nginx/1.22.0
Server Hostname:        localhost
Server Port:            80

Document Path:          /api/v1/types
Document Length:        42 bytes

Concurrency Level:      150
Time taken for tests:   206.076 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      19600000 bytes
HTML transferred:       4200000 bytes
Requests per second:    485.26 [#/sec] (mean)
Time per request:       309.115 [ms] (mean)
Time per request:       2.061 [ms] (mean, across all concurrent requests)
Transfer rate:          92.88 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   0.5      1      27
Processing:     6  305 247.1    240    1642
Waiting:        6  303 247.1    238    1641
Total:          7  306 247.1    241    1642

Percentage of the requests served within a certain time (ms)
  50%    241
  66%    320
  75%    410
  80%    472
  90%    671
  95%    825
  98%   1017
  99%   1108
 100%   1642 (longest request)
```
 
 ## Вывод
 
 При использовании балансировки увеличилось количество запросов в секунду (Requests per second), уменьшилось среднее время, затраченное на один запрос (Time per request) и увеличилась скорость передачи данных (Transfer rate). Таким образом, можно сделать вывод, что использование балансировки позволяет улучшить отзывчивость сервера.
 
 Также стоит отметить, что при увеличении количества конкурирующих запросов к серверу ухудшаются результаты нагрузочного тестирования (многим запросам приходилось ждать обработки несколько секунд, что может быть связано с тем, что веб-сервер помещает запросы в очередь ожидания).
