# Dot.net logging best practices w/ serilog-example
Serilog example for better logging in dot.net app

## What to log?
Types of logging to worry about.
* Usage
* Performance
* Error
* Diagnostic/Debug

### Right information in the log
* where from product/layer/location/hostname
* who is in play (user/customer)
* what else? (time spent / parameters / browers info / other important case by case info)

## Strategy
Create global logger for each type (usage / performance / error / diagnostic) and have methods in them
