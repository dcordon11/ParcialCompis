using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ParcialCompis.Tokens;

namespace ParcialCompis
{
    internal class AnalizadorSintactico
    {
        private Stack<string> pila;

        public AnalizadorSintactico()
        {
            pila = new Stack<string>();
        }

        public int Parse(List<Token> tokens)
        {
            pila.Push("<if_stmt>");

            int index = 0;
            while (tokens.Count != 0)
            {
                string lookahead = tokens[0].Type.ToString();

                switch (pila.Peek())
                {
                    case "If":
                    case "Then":
                    case "Else":
                    case "ElseIf":
                    case "ID":
                    case "NUM":
                        if (pila.Peek() == lookahead)
                        {
                            pila.Pop();
                            index++;
                        }
                        else
                        {
                            return 0; 
                        }
                        break;

                    case "<if_stmt>":
                        pila.Pop();
                        pila.Push("<else_part>");
                        pila.Push("<else_if_part>");
                        pila.Push("<stmt>");
                        pila.Push("Then");
                        pila.Push("<expr>");
                        pila.Push("If");
                        break;

                    case "<else_if_part>":
                        pila.Pop();
                        if (lookahead == "ElseIf")
                        {
                            pila.Push("<else_if_part>");
                            pila.Push("<stmt>");
                            pila.Push("Then");
                            pila.Push("<expr>");
                            pila.Push("ElseIf");
                        }
                        else
                        {
                            pila.Push("ε"); 
                        }
                        break;

                    case "<else_part>":
                        pila.Pop();
                        if (lookahead == "Else")
                        {
                            pila.Push("<stmt>");
                            pila.Push("Else");
                        }
                        else
                        {
                            pila.Push("ε");
                        }
                        break;

                    case "<expr>":
                    case "<stmt>":
                        pila.Pop();
                        if (lookahead == "ID" || lookahead == "NUM")
                        {
                            pila.Push(lookahead);
                        }
                        else
                        {
                            return 0; 
                        }
                        break;

                    default:
                        return 0; 
                }
            }

            return tokens.LastOrDefault()?.Type.ToString() == "EOF" ? 1 : 0;
        }

    }
}
