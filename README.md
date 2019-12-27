# Dot.net logging best practices w/ serilog-example
Serilog example of how to set up better logging in dot.net app.  
Example will show setting up a global logging, breaking out logging
into different categories, and creating a framework for project to follow
to ensure right information is logged for right event.

### Required Setup / Environment
* LocalDB

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


---
## MVC Logging
* create utility method [helper.cs](Logging/Logging.Web/Helper.cs) to get all user / location / session data in log detail
* apply performance globaly
* apply global exception handling
* add usage and diagnostics
