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

Document Path:          /api/v1/users
Document Length:        42 bytes

Concurrency Level:      100
Time taken for tests:   219.321 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      19600000 bytes
HTML transferred:       4200000 bytes
Requests per second:    455.95 [#/sec] (mean)
Time per request:       219.321 [ms] (mean)
Time per request:       2.193 [ms] (mean, across all concurrent requests)
Transfer rate:          87.27 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   0.5      1      24
Processing:     6  216 141.0    214    1209
Waiting:        5  215 140.9    212    1208
Total:          6  217 141.0    214    1209

Percentage of the requests served within a certain time (ms)
  50%    214
  66%    298
  75%    340
  80%    364
  90%    402
  95%    426
  98%    459
  99%    491
 100%   1209 (longest request)
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

Document Path:          /api/v1/users
Document Length:        42 bytes

Concurrency Level:      100
Time taken for tests:   220.427 seconds
Complete requests:      100000
Failed requests:        0
Non-2xx responses:      100000
Total transferred:      19600000 bytes
HTML transferred:       4200000 bytes
Requests per second:    453.66 [#/sec] (mean)
Time per request:       220.427 [ms] (mean)
Time per request:       2.204 [ms] (mean, across all concurrent requests)
Transfer rate:          86.83 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   0.5      1      27
Processing:     6  217 221.9     91    1100
Waiting:        5  216 221.9     90    1099
Total:          7  218 221.9     92    1101

Percentage of the requests served within a certain time (ms)
  50%     92
  66%    239
  75%    369
  80%    442
  90%    599
  95%    674
  98%    734
  99%    768
 100%   1101 (longest request)
 ```
 
 ## Вывод
 
 При увеличении количества конкурирующих запросов к серверу ухудшаются результаты тестирования.

 Использование балансировки позволяет улучшить результаты.
