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
            InputEncoding = Encoding.UTF8;

            var rowsNumbers = new[ ] {
                6,1,0,1
            };

            var colsNumbers = new[ ] {
                7,1,1,0
            };
            /*
            var kakurasu = new Kakurasu( rowsNumbers, colsNumbers );
            kakurasu.Solve( );
            WriteLine( $"Solve is { kakurasu.IsCorrectSolution() }" );
            WriteLine();
            WriteLine( kakurasu );
            WriteLine();
            */

            //var vars = FindVariants( 8, 8 );

            ReadKey( );
            
            List<List<int>> FindVariants( int number, int itemsCount )
            {
                var variants = new List<List<int>>( VariantsCount( number ) );
                variants.Add( new List<int> { number } );
                int value;
                List<int> numbers;
                var count = ( int ) Ceiling( number / 2.0 );

                for ( var numberDec = number - 1; numberDec >= count; numberDec -- )
                {   
                    value = number - numberDec;
                    numbers = new List<int>();



                    numbers.Add( numberDec );
                    variants.Add( numbers );
                }

                return variants;
            }

            int VariantsCount( int number )
            {
                switch ( number )
                {
                    case 1:
                        return 1;
                    case 2:
                        return 1;
                    case 3:
                        return 2;
                    case 4:
                        return 2;
                    default:
                        return number - 2;
                }
            }

        }
    }
}
