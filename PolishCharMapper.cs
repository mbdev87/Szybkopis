using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Szybkopis
{
    public class PolishCharMapper
    {
        private readonly Dictionary<char, char> _polishChars;
        private bool _waitingForLetter = false;

        public PolishCharMapper()
        {
            _polishChars = new Dictionary<char, char>
            {
                { 'a', 'ą' },
                { 'A', 'Ą' },
                { 'c', 'ć' },
                { 'C', 'Ć' },
                { 'e', 'ę' },
                { 'E', 'Ę' },
                { 'l', 'ł' },
                { 'L', 'Ł' },
                { 'n', 'ń' },
                { 'N', 'Ń' },
                { 'o', 'ó' },
                { 'O', 'Ó' },
                { 's', 'ś' },
                { 'S', 'Ś' },
                { 'x', 'ź' },
                { 'X', 'Ź' },
                { 'z', 'ż' },
                { 'Z', 'Ż' }
            };
        }

        public string ProcessKey(Keys key, bool isShift, bool isCtrl, bool isAlt)
        {
            if (isCtrl || isAlt) return null;

            char keyChar = KeyToChar(key, isShift);
            if (keyChar == '\0') return null;

            return ProcessPrefix(keyChar);
        }

        private string ProcessPrefix(char keyChar)
        {
            if (keyChar == ';')
            {
                if (_waitingForLetter)
                {
                    _waitingForLetter = false;
                    return ";";
                }
                else
                {
                    _waitingForLetter = true;
                    return "";
                }
            }
            else if (keyChar == ':')
            {
                _waitingForLetter = false;
                return null;
            }
            else
            {
                if (_waitingForLetter)
                {
                    _waitingForLetter = false;
                    if (_polishChars.TryGetValue(keyChar, out char accentedChar))
                    {
                        return accentedChar.ToString();
                    }
                    else
                    {
                        return keyChar.ToString();
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        private char KeyToChar(Keys key, bool isShift)
        {
            if (key >= Keys.A && key <= Keys.Z)
            {
                char baseChar = (char)('a' + (key - Keys.A));
                return isShift ? char.ToUpper(baseChar) : baseChar;
            }
            if (key == Keys.OemSemicolon)
            {
                return isShift ? ':' : ';';
            }
            return '\0';
        }
    }
}