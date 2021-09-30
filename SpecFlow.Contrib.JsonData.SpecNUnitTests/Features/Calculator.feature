Feature: Calculator
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](SpecFlow.Contrib.JsonData.SpecNUnitTests/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@DataSource:TestData/Users.xlsx
Scenario: Test Excel
	Given the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 120

@DataSource:TestData/Users.csv
Scenario: Test CSV
	Given the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 120

@DataSource:TestData/Users.json
Scenario: Test Json
	Given the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 120