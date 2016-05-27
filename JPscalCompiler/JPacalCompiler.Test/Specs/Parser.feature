Feature: Parser
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Have the simple declaration sentence without initialization
	Given I have a sentence declaration 'Var s : String ;'
	When We parse
	Then the result should be true

Scenario: Have the multiple declaration sentence without initialization
	Given I have a sentence declaration 'Var S, : String ;'
	When We parse
	Then the result should be true

Scenario: Have the string declaration sentence with initialization
	Given I have a sentence declaration 'Var str : String = 'patito';'
	When We parse
	Then the result should be true

Scenario: Have the declaration sentence with expresion initialization
	Given I have a sentence declaration 'Var number : integer = (1+5)*3;'
	When We parse
	Then the result should be true