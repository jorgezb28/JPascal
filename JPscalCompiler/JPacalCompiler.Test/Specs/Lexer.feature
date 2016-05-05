Feature: Lexer
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Source code is empty
	Given I have an input of ''
	When We Tokenize
	Then the result should be 
		| Type		| Lexeme   | Column | Row |
		| EOF	    | $	       |   0    | 0   |
