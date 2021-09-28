@Citilink
Feature: FindProductInCitilink

Scenario: TestSite_Citilink
	Given I am open site with url "https://www.citilink.ru/"
	When I selected "//div[contains(@class, 'MainHeader__search')]//input[@name='text']" and find product by name "iPhone 12"
	And I click on product "//div[@data-index='1']//a[contains(@class,' ProductCardVertical__link')]" 
	And remembered "//h1[contains(text(),'iPhone 12')]" of product
	And I get product name from product details page and put it in scenario context by name 'productNameFromProductPage'
	And I click on "//div[contains(@class,'buy-button')]//button[contains(@class,'Button_with-icon')]" 
	And I click on  "//div[@class='UpsaleBasket__header-link']//button[contains(@class,'UpsaleBasket__order')]" button
	Then I validate "//a[@class='ProductCardForBasket__name  Link js--Link Link_type_default'][contains(text(), 'iPhone 12')]" the only one
	And I get product name from basket page and put it in scenario context by name 'productNameFromBasketPage'
	Then I validate two scenario contexts have equal text 'productNameFromProductPage', 'productNameFromBasketPage'
	Then I validate the name of product in basket "//a[@class='ProductCardForBasket__name  Link js--Link Link_type_default'][contains(text(), 'iPhone 12')]" equals selected name  "//h1[contains(@class,'ProductHeader__title')]" 