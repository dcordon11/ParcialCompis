using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ParcialCompis.Tokens;

namespace ParcialCompis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido");

            Console.WriteLine("Escribe la cadena que deseas evaluar");

            Console.WriteLine("Ejemplo: if variable 20 then variable 20 else if variable 10 ");

            string cadenaPrueba = Console.ReadLine();

            

            try
            {
                AnalizadorLexico analizador = new AnalizadorLexico(cadenaPrueba);
                AnalizadorSintactico analizadorSintactico = new AnalizadorSintactico();

                List<Token> tokens = analizador.Tokenizador();

                foreach (Token token in tokens)
                {
                    Console.WriteLine($"Token: {token.Type.ToString()} Value: {token.Value}");
                }
                int resultado = analizadorSintactico.Parse(tokens);

                if (resultado == 0)
                {
                    Console.WriteLine("La expresión es sintácticamente correcta.");
                    Console.ReadLine();         
                }
                else
                {
                    Console.WriteLine("Hay un error");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de sintaxis: {ex.Message}");
                Console.ReadLine();
            }
        }


    }
    
}
