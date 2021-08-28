# HelloWorldWeb 
## How to deploy to Heroku
1. Create heroku account
2. Create application
3. Choose container registry as deployment method
4. Make sure application works locally


Login to heroku
```
heroku login
heroku container:login
```

Push container
```
heroku container:push -a app-dragos web
```

Release the container
```
heroku container:release -a app-dragos web

Link to application : http://app-dragos.herokuapp.com