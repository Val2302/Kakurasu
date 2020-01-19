using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Math;
using static Kakurasu.ArrayConverter;

namespace Kakurasu
{
    class Program
    {
        static void Main( string[ ] args )
        {
            OutputEncoding = Encoding.UTF8;
            var rowsNumbers = new[ ] {
                0,6,10,5
            };

            var colsNumbers = new[ ] {
                5,9,9,3
            };
            
            var kakurasu = new Kakurasu( rowsNumbers, colsNumbers );
            kakurasu.Solve( );
            WriteLine( $"Solve is { kakurasu.IsCorrectSolution() }" );
            WriteLine();
            WriteLine( kakurasu );
            WriteLine();
                        
            ReadKey( );
            
        }
    }
}
