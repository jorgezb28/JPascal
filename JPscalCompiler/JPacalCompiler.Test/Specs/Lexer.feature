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
		| Type         | Lexeme | Column | Row |
		| ReservedWord | type   | 0      | 0   |
		| EOF          | $      | 4      | 0   |

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
		| Type         | Lexeme  | Column | Row |
		| ReservedWord | var     | 0      | 0   |
		| Id           | myArray | 4      | 0   |
		| EOF          | $       | 11     | 0   |

Scenario: Source code have an aritmethic operation with reserved word
	Given I have an input of '10 mod 5'
	When We Tokenize
	Then the result should be 
		| Type     | Lexeme | Column | Row |
		| Integer  | 10     | 0      | 0   |
		| Operator | mod    | 3      | 0   |
		| Integer  | 5      | 7      | 0   |
		| EOF      | $      | 8      | 0   |

Scenario: Source code have an logic operation 
	Given I have an input of 'true and false'
	When We Tokenize
	Then the result should be 
		| Type         | Lexeme | Column | Row |
		| ReservedWord | true   | 0      | 0   |
		| Operator     | and    | 5      | 0   |
		| ReservedWord | false  | 9      | 0   |
		| EOF          | $      | 14     | 0   |

Scenario: Source code have an sum aritmethic operation 
	Given I have an input of '10 + 5'
	When We Tokenize
	Then the result should be 
		| Type     | Lexeme | Column | Row |
		| Integer  | 10     | 0      | 0   |
		| Operator | +      | 3      | 0   |
		| Integer  | 5      | 5      | 0   |
		| EOF      | $      | 6      | 0   |

Scenario: Source code have an mult aritmethic operation 
	Given I have an input of '10 * 5'
	When We Tokenize
	Then the result should be 
		| Type     | Lexeme | Column | Row |
		| Integer  | 10     | 0      | 0   |
		| Operator | *      | 3      | 0   |
		| Integer  | 5      | 5      | 0   |
		| EOF      | $      | 6      | 0   |

Scenario: Source code have an unidimensional array 
	Given I have an input of 'type vector = array [ 0..24] of real;'
	When We Tokenize
	Then the result should be 
		| Type              | Lexeme | Column | Row |
		| ReservedWord      | type   | 0      | 0   |
		| Id                | vector | 5      | 0   |
		| Operator          | =      | 12     | 0   |
		| ReservedWord      | array  | 14     | 0   |
		| PunctuationSymbol | [      | 19     | 0   |
		| Integer           | 0      | 21     | 0   |
		| PunctuationSymbol | ..     | 22     | 0   |
		| EOF      | $      | 6      | 0   |