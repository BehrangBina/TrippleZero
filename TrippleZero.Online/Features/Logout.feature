Feature: Logout

A logged in user can logout

@StandardUserAuthenticated @Online @Logout
Scenario: A logged in user can logout
	Given The Dropdown Exists
	When The user clicks on the Dropdown
	Then The user sees the Logout button
	When The user clicks on the Logout button
	Then The user is logged out

