using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kakurasu.ArrayConverter;

namespace Kakurasu
{
    public class BinNumber : IEnumerable
    {
        private List<int> _numbers;
        public int Count => _numbers.Count;
        public int this[ int index ]
        {
            get => _numbers[ index ];
            set => _numbers[ index ] = value;
        }

        public BinNumber( int value = 0 )
        {
            if ( value < 0 || value > 1 )
            {
                throw new ArgumentOutOfRangeException( "Argument of constructor BinNumber is out of range" );
            }
            
            _numbers = new List<int> { value };
        }

        private void Add( int value )
        {
            _numbers.Add( value );
        }

        public static BinNumber operator ++( BinNumber binNumber )
        {
            if ( binNumber is null )
            {
                throw new ArgumentNullException( "Argument is null in icrement function in BinNumber" );
            }

            var result = new BinNumber();
            result._numbers.Clear();
            var temp = 1;

            foreach ( var number in binNumber._numbers )
            {
                if ( temp == 0 )
                {
                    result.Add( number );
                }
                else
                {
                    if ( number == 0 )
                    {
                        result.Add( 1 );
                        temp = 0;
                    }
                    else
                    {
                        result.Add( 0 );
                        temp = 1;
                    }
                }
            }

            if ( temp == 1 )
            {
                result.Add( 1 );
            }

            return result;            
        }

        public IEnumerator GetEnumerator()
        {
            return  _numbers.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join( ", ", _numbers ); // Convert( _numbers.ToArray() );
        }
    }
}
