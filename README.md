# Dot.net logging best practices w/ serilog-example
Serilog example of how to set up better logging in dot.net app.  
Example will show setting up a global logging, breaking out logging
into different categories, and creating a framework for project to follow
to ensure right information is logged for right event.

### Required Setup / Environment
* LocalDB (some manual steps -> after initial create add stored proc)
	* TODO add stored proc into migrations
	* TODO add update to table column to create error into migrations
* dot.net framework

### What to log?
Types of logging to worry about.
* Usage
* Performance
* Error
* Diagnostic/Debug

### Right information in the log
* where from product/layer/location/hostname
* who is in play (user/customer)
* what else? (time spent / parameters / browers info / other important case by case info)

### Strategy
Create global logger for each type (usage / performance / error / diagnostic) and have methods in them to log to the 
correct log / sink.

### Exception Handling
* Try to use global exception handlers in application
	* avoid exception logic noise in application if possible 
* Try to make them add value (can add in additional derived info / stored proc info )

## Projects
| project | description | 
| - | - |
| Logging.Core | helper methods for basis of setting up global loging framework | 
| Logging.Data | example of extending database task logging | 
| Logging.web | hepler methods related to mvc or web app logging - getting users / etc | 
| LoggingConsole | console app to demo loggin core priciples |
| TodoMvc | sample mvc project to practice logging on | 
---
## MVC Logging
* create utility method 
* apply performance globaly
* apply global exception handling
* add usage and diagnostics

## MVC utility Methods
Similar to the logging.core project log methods are put in this project to help for web and mvc logging.
[helpers.cs](Logging/Logging.Web/Helpers.cs) to get all user / location / session data in log detail

## Apply performance Tracking
Adding performance through [global filerters](Logging/TodoMvc/App_Start/FilterConfig.cs).

## Apply global error handler/logging
Add global error logger in the [Global.asax](Logging/TodoMvc/Global.asax).  
* Errors will redirect to a custom page as well as have the error id.