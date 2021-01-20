Feature: Open product page
	Scenario Outline: User register and authorize, open's product page
		Given register atributes
		
		When I register
		Then Registered Email should be correct
		
		When I login
		Then Status should be ok
		
		When I get on main page
		Then I see all the products
		
		When I open about page of a product
		Then I see productInfo and reviews for this product