Feature: DynamicTable
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers and verify by dynamic Instance

@additionTag
Scenario: Add two numbers
	Given I have entered below numbers and verify all the numbers
	| num1 | num2 | result |
	| 10   | 20   | 30     |
	| 20   | 20   | 40     |

@VerifyScenarioTag
Scenario: Verify Scenario Context
Given i verifyed scenario example


Scenario: Enter User Details in Userform
Given i have login the application below credentail
| username    | password |
| bibek       | Pass@wd  |
| bibekananda | Pass@wd  |
And i enterd below details in the form
| fname       | lname     | salary |
| bibekananda | Panigrahi | 75000  |

Scenario: Both login and enter user details
Given i logged in and entered user details

Scenario: I checked XML Xpath and modify it
Given i  update xml file by xpath


