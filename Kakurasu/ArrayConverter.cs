using System;

namespace Kakurasu
{
    public static class ArrayConverter
    {
        public static string Convert<T> ( T[ ] array, Func<T, char> convert = null )
        {
            if ( array is null )
            {
                throw new ArgumentNullException( @"In ArrayConverter.Convert() argument ""array"" is '=' null" );
            }

            var headerNumbers = GenerateHeaderNumbers( );
            var dataRow = convert is null ? GenerateRow( ) : GenerateConvertRow( );
            var bottomLine = GenerateBottomLine( );

            return headerNumbers + dataRow + bottomLine;

            string GenerateHeaderNumbers ( )
            {
                var upperLine = "╔═";
                var middleLine = "║ ";
                var lowerLine = "╠═";

                for ( int i = 1; i < array.Length; i++ )
                {
                    upperLine += "╦═";
                    middleLine += "║" + i % 10;
                    lowerLine += "╬═";
                }

                upperLine += "╗\n";
                middleLine += "║\n";
                lowerLine += "╣\n";

                return upperLine + middleLine + lowerLine;
            }

            string GenerateRow ( )
            {
                var row = "║" + array[ 0 ];

                for ( var i = 1; i < array.Length; i++ )
                {
                    row += "│" + array[ i ];
                }

                return row + "║\n";
            }

            string GenerateConvertRow ( )
            {
                var row = "║" + convert( array[ 0 ] );

                for ( var i = 1; i < array.Length; i++ )
                {
                    row += "│" + convert( array[ i ] );
                }

                return row + "║\n";
            }

            string GenerateBottomLine ( )
            {
                var line = "╚═";

                for ( int i = 1; i < array.Length; i++ )
                {
                    line += "╩═";
                }

                line += "╝\n";

                return line;
            }
        }

        public static string Convert<T> ( T[ , ] array, Func<T, char> convert = null )
        {
            if ( array is null )
            {
                throw new ArgumentNullException( @"In ArrayConverter.Convert() argument ""array"" is '=' null" );
            }

            var rowCount = array.GetLength( 0 );
            var colCount = array.GetLength( 1 );
            var headerNumbers = GenerateHeaderNumbers( );
            var dataRow = convert is null ? GenerateRows( ) : GenerateConvertRows( );
            var bottomLine = GenerateBottomLine( );

            return headerNumbers + dataRow + bottomLine;

            string GenerateHeaderNumbers ( )
            {
                var upperLine = "╔═";
                var middleLine = "║ ";
                var lowerLine = "╠═";

                for ( int i = 0; i < colCount; i++ )
                {
                    upperLine += "╦═";
                    middleLine += "║" + i % 10;
                    lowerLine += "╬═";
                }

                upperLine += "╗\n";
                middleLine += "║\n";
                lowerLine += "╣\n";

                return upperLine + middleLine + lowerLine;
            }

            string GenerateRows ( )
            {
                var horizontalLine = GenerateHorizontalLine( );
                int numberRow;
                int i, j;

                var text = "║0║" + array[ 0, 0 ];

                for ( j = 1; j < colCount; j++ )
                {
                    text += "│" + array[ 0, j ];
                }

                text += "║\n";

                for ( i = 1; i < rowCount; i++ )
                {
                    numberRow = i % 10;
                    text += horizontalLine + "║" + numberRow + "║" + array[ i, 0 ];

                    for ( j = 1; j < colCount; j++ )
                    {
                        text += "│" + array[ i, j ];
                    }

                    text += "║\n";
                }

                return text;
            }

            string GenerateConvertRows ( )
            {
                var horizontalLine = GenerateHorizontalLine( );
                int numberRow;
                int i, j;

                var text = "║0║" + convert( array[ 0, 0 ] );

                for ( j = 1; j < colCount; j++ )
                {
                    text += "│" + convert( array[ 0, j ] );
                }

                text += "║\n";

                for ( i = 1; i < rowCount; i++ )
                {
                    numberRow = i % 10;
                    text += horizontalLine + "║" + numberRow + "║" + convert( array[ i, 0 ] );

                    for ( j = 1; j < colCount; j++ )
                    {
                        text += "│" + convert( array[ i, j ] );
                    }

                    text += "║\n";
                }

                return text;
            }

            string GenerateHorizontalLine ( )
            {
                var line = "╠═╬─";

                for ( int i = 1; i < colCount; i++ )
                {
                    line += "┼─";
                }

                return line + "╣\n";
            }

            string GenerateBottomLine ( )
            {
                var line = "╚═";

                for ( int i = 0; i < colCount; i++ )
                {
                    line += "╩═";
                }

                line += "╝\n";

                return line;
            }
        }
    }
}
