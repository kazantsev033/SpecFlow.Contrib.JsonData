Feature: Users

@DataSource:TestData/Users.json
Scenario: Get users form Json
	Given I got user <Login>, <FirstName>, <LastName>
	Then user should match to snapshoot

@DataSource:TestData/Users.json
@DataSet:users
Scenario: Get users form Json with DataSet
	Given I got user <Login>, <FirstName>, <LastName>
	Then user should match to snapshoot

@DataSource:TestData/Users.xlsx
@DataSet:Users
Scenario: Get users form Excel
	Given I got user <Login>, <FirstName>, <LastName>
	Then user should match to snapshoot

@DataSource:TestData/Users.csv
Scenario: Get users form Csv
	Given I got user <Login>, <FirstName>, <LastName>
	Then user should match to snapshoot