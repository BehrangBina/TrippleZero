# TrippleZero Online Automation Framework

## Overview

The TrippleZero Online Automation Framework is designed to automate the end-to-end testing of the TrippleZero online shopping platform. It leverages modern tools and practices to ensure scalability, maintainability, and robustness.

## Assumptions
1. Login
   - Only Testing with Stardard User 
2. ProductPurchase
   - Only Cover one Product to cart at this stage
   - We know what are the required validation in checkout step 2
       - Product name
       - Product price
       - Information label and text
       - Shipping Label and text
       - Total Element  (excluding tax calculation)

## Project Structure
```
TrippleZero.Online/
├── Pages/
│   ├── CartPage.cs
│   ├── CheckoutCompletePage.cs
│   ├── CheckoutPage.cs
│   └── InventoryPage.cs
├── StepDefinitions/
│   └── ProductPurchaseStepDefinitions.cs
├── Utils/
│   └── Endpoint.cs
│   └── TestBase.cs
├── TrippleZero.Online.csproj
TrippleZero.Common/
├── EnvironmentManager.cs
├── TrippleZero.Common.csproj
tests/
├── TrippleZero.Online.Tests/
│   ├── Features/
│   │   └── ProductPurchase.feature
│   ├── Hooks/
│   │   └── Hooks.cs
│   ├── StepDefinitions/
│   │   └── ProductPurchaseStepDefinitions.cs
│   └── TrippleZero.Online.Tests.csproj
.gitignore
README.md
```
### TrippleZero.Online

This project contains the step definitions and page objects for the automation tests. It uses the following structure:

- **StepDefinitions**: Contains the step definitions for the BDD scenarios.
- **Pages**: Contains the page objects representing the different pages of the application.
- **Utils**: Contains utility classes and methods used across the project.

## Framework Design:

1.	**FluentAssertions**
*	Purpose: Provides a more readable and fluent way to write assertions in tests.
*	Usage: Used in step definitions and page objects to assert conditions in a human-readable manner.
*	Example: currentUrl.Should().Contain(pageName, "Current url does not contain the expected page name");
2.	**Microsoft.Extensions.Logging**
*	Purpose: Provides a logging framework to log information, warnings, and errors.
*	Usage: Used in step definitions and page objects to log important information and errors for debugging and tracking purposes.
*	Example: _logger.LogInformation("Checking if user is in the inventory page");
3.	**Reqnroll**
*	Purpose: A BDD framework for .NET that uses Gherkin syntax for writing test scenarios.
*	Usage: Used to define feature files and step definitions for BDD-style tests.
*	Example: Feature: ProductPurchase
*	Purpose: A library for handling HTTP requests and responses.
*	Usage: Used for making HTTP requests and handling responses, typically in utility classes or step definitions that require API interactions.
*	Example: var response = await _httpClient.GetAsync("api/endpoint");
4.	**Xunit.Abstractions**
*	Purpose: Provides interfaces for xUnit test framework, particularly for output logging.
*	Usage: Used in step definitions to log test output using the ITestOutputHelper interface.
*	Example: public ProductPurchaseStepDefinitions(ScenarioContext scenarioContext, ITestOutputHelper output)
5.	**Playwright**
*	Purpose: A browser automation library for end-to-end testing.
*	Usage: Used to interact with web pages, perform actions, and verify conditions in the browser.
*	Example: await _page.ClickAsync("#button");



### TrippleZero.Common

This project contains common utilities and configurations used by the TrippleZero.Online project.

**Can be used in multiple projects: Example Web/Mobile/API**

- **EnvironmentManager**: Manages the configuration settings by loading them from JSON files, environment variables, and command-line arguments.


## Key Features

### Scalability

1. **Modular Design**: The framework is designed with a modular structure, separating step definitions, page objects, and utilities. This allows for easy addition of new features and tests without affecting existing code.
2. **Configuration Management**: The `EnvironmentManager` class provides a centralized way to manage configuration settings, making it easy to switch environments and manage different configurations.
3. **Data-Driven Testing**: Parameterized tests and data tables allow for running the same test with different data sets, making it easy to scale tests for various scenarios and inputs.
4. **Consistent Practices**: Consistent naming conventions, error handling, and logging practices ensures that the codebase remains clean, readable, and maintainable as it grows.
5. **Behavior-Driven Development (BDD)**:Using BDD with Gherkin syntax makes the tests readable and understandable by non-technical stakeholders, facilitating collaboration and ensuring that the tests align with business requirements.

By adhering to these best practices, the TrippleZero Online Automation Framework ensures that test cases are well-structured, maintainable, and scalable, making it easier to add new tests, maintain existing ones, and ensure the overall quality of the automation framework.

### Maintainability

1. **Error Handling**: The framework includes comprehensive error handling with meaningful logging, making it easier to diagnose and fix issues.
2. **Consistent Naming Conventions**: The code follows consistent naming conventions, improving readability and reducing the likelihood of errors.
3. **Code Documentation**: XML documentation comments are added to methods, providing clear explanations of their purpose and usage.
4. **ScenarioContext Usage**: Constants are used for `ScenarioContext` keys to avoid magic strings and potential typos, ensuring consistency and reducing errors.

 
## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022

#### Report
```
✅ Native Support: Works seamlessly with Reqnroll.
✅ Visual & Interactive: Generates easy-to-read HTML reports.
✅ Integrates with CI/CD: Can be used in Azure DevOps, Jenkins, GitHub Actions.
✅ dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
```
### Installation

1. Clone the repository:
2. Open the solution in Visual Studio 2022.

### Running Tests
 ``` run run.bat in cli ```
1. Build the solution: 
* dotnet clean 
* donet build
2. Run the tests:
* dotnet test --filter Category=Online --logger:trx
* livingdoc  feature-folder features --output TestResult/TestReport.html


## Contributing

Contributions are welcome! Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Commit your changes and push them to your fork.
4. Create a pull request with a description of your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For any questions or issues, please contact [Behrang Bina](mailto:BehrangBina@hotmail.com).


   