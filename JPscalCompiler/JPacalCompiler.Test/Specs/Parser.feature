Feature: Parser
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Have the simple declaration sentence without initialization
	Given I have a sentence declaration 'Var  : String ;'
	When We parse
	Then the result should be true

Scenario: Have the multiple declaration sentence without initialization
	Given I have a sentence declaration 'Var S,str : String ;'
	When We parse
	Then the result should be true
