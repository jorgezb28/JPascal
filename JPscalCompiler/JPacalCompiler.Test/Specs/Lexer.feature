﻿Feature: Lexer
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

Scenario: Source code is an Id with digit
	Given I have an input of 'a1'
	When We Tokenize
	Then the result should be 
		| Type | Lexeme | Column | Row |
		| Id   | a1     | 0      | 0   |
		| EOF  | $      | 2      | 0   |

Scenario: Source code is an string Id
	Given I have an input of 'patito'
	When We Tokenize
	Then the result should be 
		| Type | Lexeme | Column | Row |
		| Id   | patito | 0      | 0   |
		| EOF  | $      | 6      | 0   |

Scenario: Source code is an reserved word
	Given I have an input of 'type'
	When We Tokenize
	Then the result should be 
		| Type | Lexeme | Column | Row |
		| Type | type   | 0      | 0   |
		| EOF  | $      | 4      | 0   |

Scenario: Source code is an integer
	Given I have an input of '2016'
	When We Tokenize
	Then the result should be 
		| Type    | Lexeme | Column | Row |
		| Integer | 2016   | 0      | 0   |
		| EOF     | $      | 4      | 0   |

Scenario: Source code is a float
	Given I have an input of '1024.633' 
	When We Tokenize
	Then the result should be 
		| Type  | Lexeme   | Column | Row |
		| Float | 1024.633 | 0      | 0   |
		| EOF   | $        | 8      | 0   |

Scenario: Source code have two id separated by space
	Given I have an input of 'var myArray'
	When We Tokenize
	Then the result should be 
		| Type | Lexeme  | Column | Row |
		| Var  | var     | 0      | 0   |
		| Id   | myArray | 4      | 0   |
		| EOF  | $       | 11     | 0   |

Scenario: Source code have an aritmethic operation with reserved word
	Given I have an input of '10 mod 5'
	When We Tokenize
	Then the result should be 
		| Type    | Lexeme | Column | Row |
		| Integer | 10     | 0      | 0   |
		| OpMod   | mod    | 3      | 0   |
		| Integer | 5      | 7      | 0   |
		| EOF     | $      | 8      | 0   |

Scenario: Source code have an logic operation 
	Given I have an input of 'true and false'
	When We Tokenize
	Then the result should be 
		| Type  | Lexeme | Column | Row |
		| True  | true   | 0      | 0   |
		| OpAnd | and    | 5      | 0   |
		| False | false  | 9      | 0   |
		| EOF   | $      | 14     | 0   |

Scenario: Source code have an sum aritmethic operation 
	Given I have an input of '10 + 5'
	When We Tokenize
	Then the result should be 
		| Type    | Lexeme | Column | Row |
		| Integer | 10     | 0      | 0   |
		| OpSum   | +      | 3   | 0 |
		| Integer | 5      | 5      | 0   |
		| EOF     | $      | 6      | 0   |

Scenario: Source code have an mult aritmethic operation 
	Given I have an input of '10 * 5'
	When We Tokenize
	Then the result should be 
		| Type    | Lexeme | Column | Row |
		| Integer | 10     | 0      | 0   |
		| OpMult  | *      | 3      | 0   |
		| Integer | 5      | 5      | 0   |
		| EOF     | $      | 6      | 0   |

Scenario: Source code have an unidimensional array 
	Given I have an input of 'type vector = array[ 0 .. 24] of float;'
	When We Tokenize
	Then the result should be 
		| Type           | Lexeme | Column | Row |
		| Type           | type   | 0      | 0   |
		| Id             | vector | 5      | 0   |
		| OpEquals       | =      | 12     | 0   |
		| Array          | array  | 14     | 0   |
		| PsOpenBracket  | [      | 19     | 0   |
		| Integer        | 0      | 21     | 0   |
		| PsArrayRange   | ..     | 23     | 0   |
		| Integer        | 24     | 26     | 0   |
		| PsCloseBracket | ]      | 28     | 0   |
		| Of             | of     | 30     | 0   |
		| Float          | float  | 33     | 0   |
		| PsSentenseEnd  | ;      | 38     | 0   |
		| EOF            | $      | 39     | 0   |

Scenario: Source code has an record
	Given I have the next record definition:
	| Sentences       |
	| type            |
	| Books = record  |
	| Title : string; |
	| end;            |
	When We tokenize
	Then the multiline result should be
         | Type          | Lexeme | Column | Row |
         | Type          | type   | 0      | 0   |
         | Id            | Books  | 0      | 1   |
         | OpEquals      | =      | 6      | 1   |
         | Record        | record | 8      | 1   |
         | Id            | Title  | 0      | 2   |
         | PsColon       | :      | 6      | 2   |
         | String        | string | 8      | 2   |
         | PsSentenseEnd | ;      | 14     | 2   |
         | End           | end    | 0      | 3   |
         | PsSentenseEnd | ;      | 3      | 3   |
         | EOF           | $      | 4      | 3   |
		 