# HackerNewsStories
HackerNewsStories
It is .NET 6 based WebAPI containing swagger documentation for one end point.
The get endpoints accept integer as the count of best stories for which one details

Assumption Made
1. Since the endpoint for best stories just returns ids , I assume we pick in any order the ids.
2. The user will always ask for count of stories which are less then or equal to present in the endpoint of beststoriesId

Input
![image](https://github.com/smandia/HackerNewsStories/assets/3833082/9e9b2e5d-233e-4534-8e42-da3a6e44d54a)
![image](https://github.com/smandia/HackerNewsStories/assets/3833082/b88db6ad-07ac-45b1-9a70-f840db70cdd6)



Things I would have done more if I had time
0. I would have divided the solution in multiple projects 
1. My VS 2022 crashed multiple times otherwise I would have written more tests , I could complete tests for few places only
2. I would have used RetryPolicy  and circuit breaker ploicy in my http client, AutoMapper for mapping 
3. I would have increased the lifetime  of httpclient from the pool
4. I would have added globla exception handler as a middleware
5. I would have added some kind of authentication and authorization(in form of JWT /OAuthetc)
6. I would have added a Redirect mechanism in webapi
7. I would included more robust exception handling in parallel for each async
8. I would have added more logging
   
