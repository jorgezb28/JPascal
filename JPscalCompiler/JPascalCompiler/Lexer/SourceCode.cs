namespace JPascalCompiler.Lexer
{
    public class SourceCode
    {
        private int _row;
        private int _column;
        private string _input;
        private int _currentIndex;

        public SourceCode(string sourceCodeInput)
        {
            _input = sourceCodeInput;
            _row = 0;
            _column = 0;
            _currentIndex = 0;
        }

        public Symbol NextCharacter()
        {
            if (_currentIndex >= _input.Length)
                return new Symbol {Row = _row, Column = _column,Character = '\0'};

            Symbol newCurrentChar = new Symbol {Row = _row,Column = _column,Character = _input[_currentIndex++] };

            if (newCurrentChar.Character.Equals('\n'))
            {
                _column = 0;
                _row += 1;
            }
            else
            {
                _column += 1;
            }
            return newCurrentChar;
        }
    }
}