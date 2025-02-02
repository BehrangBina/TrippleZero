Feature: ProductPurchase

Logged in User Can Purchase a Product

@StandardUserAuthenticated @Online @ProductPurchase
Scenario: Logged in User Can Purchase a Product
	Given I am in the inventory page
	When From products list I Add to cart "Sauce Labs Fleece Jacket"
	Then I add to cart button should change to remove
	And I should see the cart badge with 1
	When I click the cart button
	Then I should see the product in the cart
	And I should see the product price
	And I should see the checkout button
	When I click the checkout button
	Then I am on "checkout-step-one" page
	When I fill the checkout form
		| First Name | Last Name | Postal Code |
		| John       | Doe       | 3000        |
	And I click the continue button
	Then I am on "checkout-step-two" page
	# TBI Validatation
	Then I click on the finish button
	Then I should see the checkout-complete page