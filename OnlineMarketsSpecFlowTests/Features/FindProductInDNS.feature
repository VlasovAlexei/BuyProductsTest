@DNS
Feature: FindProductInDNS

Scenario: TestSite_DNS
	Given I am open site with url "https://www.dns-shop.ru/"
	When I selected "//div[contains(@class, 'menu')]//input[@name='q']" and find product by name "iPhone 12"
	And I click on product "//div[@data-id='product'][1]" 
	And remembered "//a[contains(@class,'catalog-product')]//span[contains(text(),'iPhone 12')]" of product
	And I click on "//div[@data-id='product'][1]//button[contains(@class,'buy-btn')]" 
	And I click on  "//span[@class='cart-link__lbl']" button
	Then I validate "//div[@class = 'cart-items__product-name']//a[contains(text(),'iPhone 12')]" the only one