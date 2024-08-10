using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ParcialCompis.Tokens;

namespace ParcialCompis
{
    internal class AnalizadorLexico
    {
        private string input;
        private int posicion;

        private static readonly Dictionary<string, TokenType> tokenPatterns = new Dictionary<string, TokenType>
        {
            { @"\bif\b", TokenType.If },
            { @"\bthen\b", TokenType.Then },
            { @"\belse if\b", TokenType.ElseIf },
            { @"\belse\b", TokenType.Else },
            { @"\b[0-9]+\b", TokenType.NUM },
            { @"\b[a-zA-Z_][a-zA-Z0-9_]*\b", TokenType.ID }
        };

        public AnalizadorLexico(string input)
        {
            this.input = input;
            this.posicion = 0;
        }


        public List<Token> Tokenizador()
        {
            var tokens = new List<Token>();
            while (posicion < input.Length)
            {
                if (char.IsWhiteSpace(input[posicion]))
                {
                    posicion++;
                    continue;
                }

                bool matched = false;

                foreach (var pattern in tokenPatterns)
                {
                    var regex = new Regex($"^({pattern.Key})");
                    var match = regex.Match(input.Substring(posicion));

                    if (match.Success)
                    {
                        tokens.Add(new Token(pattern.Value, match.Value));
                        posicion += match.Value.Length;
                        matched = true;
                        break;
                    }
                }

                if (!matched)
                {
                    throw new Exception($"Token no esperado en la posición: {posicion}");
                }
            }

            tokens.Add(new Token(TokenType.EOF, string.Empty));
            return tokens;
        }

    }
}
