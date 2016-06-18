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
	Given I have a sentence declaration 'Var S,p : String ;'
	When We parse
	Then the result should be true

Scenario: Have the string declaration sentence with initialization
	Given I have a sentence declaration 'Var str : String = 'patito';'
	When We parse
	Then the result should be true

Scenario: Have the declaration sentence with expresion initialization
	Given I have a sentence declaration 'Var number : integer = (1+5)*3.33;'
	When We parse
	Then the result should be true

Scenario: Have a multiline if sentence
	Given I have a multiline sentence declaration
	| Sentences                     |
	| if (a = 10)  then             |
	| writeln('Value of a is 10' ); |
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline if then sentence
	Given I have a multiline sentence declaration
	| Sentences                                   |
	| if (a = 10)  then                           |
	| writeln('Value of a is 10' );               |
	| else if ( a = 20 ) then                     |
	| writeln('Value of a is 20' );               |
	| else                                        |
	| writeln('None of the values is matching' ); |
	| writeln('Exact value of a is: ', a );       |
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline simple for sentence
	Given I have a multiline sentence declaration
	| Sentences          |
	| for i:= 1 to 10 do |
	| writeln(i);        |	
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline complex for sentence
	Given I have a multiline sentence declaration
	| Sentences        |
	| for i:=1 to 3 do |
	| begin            |
	| for j:=1 to 3 do |
	| begin            |
	| write(a2,s);     |
	| end;             |
	| writeln();       |
	| end;             |
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline for-in sentence
	Given I have a multiline sentence declaration
	| Sentences              |
	| for Color in TColor do |
	| DoSomething(Color);    |
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline simple while sentence
	Given I have a multiline sentence declaration
	| Sentences      |
	| while a + 6 do |
	| writeln (a);   |
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline complex while sentence
	Given I have a multiline sentence declaration
	| Sentences                 |
	| WHILE NOT true DO         |
	| BEGIN                     |
	| IF NOT true <> false THEN |
	| size := size + 1;         |
	| END;                      |  
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline complex repeat sentence
	Given I have a multiline sentence declaration
	| Sentences           |
	| repeat              |
	| DoSomethingHere(x); |
	| x := x + 1;         |
	| while a + 6 do      |
	| writeln (a);        |
	| until x = 10;       |
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline simple const declaration
	Given I have a multiline sentence declaration
	| Sentences             |
	| const                 |
	| i: Integer = 0; |
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline simple case sentence
	Given I have a multiline sentence declaration
	| Sentences                |
	| case place of            |
	| 1: ShowMessage('sds');   |
	| 2: ShowMessage(sdds);    |
	| 3: ShowMessage(sd.test); |
	| else ShowMessage(sdsd);  |
	| end;                     |
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline complex case sentence
	Given I have a multiline sentence declaration
	| Sentences                                                                      |
	| case place of                                                                  |
	| 1: begin ShowMessage('sds');                                                   |
	| ShowMessage('sds');                                                            |
	| ShowMessage('sds');                                                            |
	| end;                                                                           |
	| 2: ShowMessage(sdds);                                                          |
	| 3+expureichion(arr[expureichion(arr[4].algo[4][4].dd)]): ShowMessage(sd.test); |
	| else ShowMessage(sdsd);                                                        |
	| end;                                                                           |
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline complex record sentence
	Given I have a multiline sentence declaration
	| Sentences                          |
	| type TMember = record              |
	| firstname, lastname : string;      |
	| address: array [1 .. 3] of string; |
	| phone: string;                     |
	| birthdate: TDateTime;              |
	| paidCurrentSubscription: boolean;  |
	| end;                               |
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline simple array sentence
	Given I have a multiline sentence declaration
	| Sentences                                     |
	| TYPE                                          |
	| IntArrType = ARRAY [1 .. MaxElts] OF Integer; |
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline simple enum sentence
	Given I have a multiline sentence declaration
	| Sentences                                                            |
	| TYPE                                                                 |
	| dias = (lunes, martes, miercoles, jueves, viermes, sabado, domingo); |
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline function sentence
	Given I have a multiline sentence declaration
	| Sentences                                            |
	| (* function returning the max between two numbers *) |
	| function max(var num1, num2: integer): integer;      |
	| begin                                                |
	| var result: integer = 3;                             |
	| (* local variable declaration *)                     |
	| if (num1 > num2) then                                |
	| result := num1;                                      |
	| else                                                 |
	| result := num2;                                      |
	| end;                                                 |
	When We Parse
	Then the multiline result should be true

Scenario: Have a multiline procedure sentence
	Given I have a multiline sentence declaration
	| Sentences                                                |
	| PROCEDURE ReadArr(VAR size: Integer; VAR a: IntArrType); |
	| BEGIN                                                    |
	| size := 1;                                               |
	| WHILE NOT eof DO                                         |
	| BEGIN                                                    |
	| readln(a[size]);                                         |
	| IF NOT eof <> false THEN                                 |
	| size := size + 1;                                        |
	| END;                                                     |
	| END;                                                     |
	When We Parse
	Then the multiline result should be true