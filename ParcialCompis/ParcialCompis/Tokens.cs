﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialCompis
{
    internal class Tokens
    {
        public enum TokenType
        {
            If,
            Then,
            Else,
            ElseIf,
            ID,
            NUM,
            EOF
        }

        public class Token
        {
            public TokenType Type { get; set; }
            public string Value { get; set; }

            public Token(TokenType type, string value)
            {
                Type = type;
                Value = value;
            }
        }


    }
}
