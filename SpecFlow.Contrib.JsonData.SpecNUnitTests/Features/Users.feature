Feature: Users

@DataSource:TestData/Users.json
@JsonArray:users
Scenario: Get users form Json
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