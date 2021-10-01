# SpecFlow.Contrib.JsonData
This package is actualy based on [SpecFlow.ExternalData](https://docs.specflow.org/projects/specflow/en/latest/Guides/externaldata.html) and supports all features described there. 

I extended it to support Json files as data source

## Usage example
Feature
```gherkin
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
```

Json file
```json
{
    "users": [
        {
            "Login": "ivanov",
            "FirstName": "Ivan",
            "LastName": "Ivanov"
        },
        {
            "Login": "petrov",
            "FirstName": "Petr",
            "LastName": "Petrov"
        }
    ]
}
```

## Tags
The following tags can be used for json files:
- **@DataSource:path-to-file** - Specify file path. The path is a **relative path** to the folder of the feature files
- **@DataSet:data-set-name** - Specify array of json objects. By default, used first array
