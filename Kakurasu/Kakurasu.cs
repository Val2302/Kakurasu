using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Kakurasu.ArrayConverter;
using static System.Math;
using static System.Console;

namespace Kakurasu
{
    public class Kakurasu
    {
        private int[] _rowsNumbers;
        private int[] _colsNumbers;

        private bool[,] _solve;
        public int RowCount => _rowsNumbers.Length;
        public int ColCount => _colsNumbers.Length;

        public Kakurasu( int[] rowsNumbers, int[] colsNumbers )
        {
            if ( rowsNumbers is null || colsNumbers is null )
            {
                throw new ArgumentNullException( "Arguments is null in constructor Kakurasu" );
            }

            const int MAX_SiZE = 50;

            if ( rowsNumbers.Length == 0 || colsNumbers.Length == 0 || rowsNumbers.Length > MAX_SiZE || colsNumbers.Length > MAX_SiZE )
            {
                throw new ArgumentOutOfRangeException( $@"Lengths arguments constructor Kakurasu is 0 or its "">"" then { MAX_SiZE }" );
            }

            _rowsNumbers = rowsNumbers;
            _colsNumbers = colsNumbers;
        }

        public bool[,] Solve()
        {
            var listAllSolutions = GenerateListSolutions();
            List<List<int>> allRowsSolutions = null, allColsSolutions = null;
            DistributeAllSolutionsByItems();
            FilterSolutionsByNumbers();
            return MergeSolution();

            List<int>[] GenerateListSolutions()
            {
                long count = RowCount > ColCount ? RowCount : ColCount;
                long rowCount = ( long ) Pow( 2, count ) - 1;
                var listVariants = new List<int>[ rowCount ];
                var binNumber = new BinNumber( 1 );

                for ( long i = 0; i < rowCount; i++, binNumber++ )
                {
                    listVariants[ i ] = AddVariant( i );
                }

                return listVariants;

                List<int> AddVariant( long row )
                {
                    var variant = new List<int>( binNumber.Count );

                    for ( var i = 0; i < binNumber.Count; i++ )
                    {
                        if ( binNumber[ i ] != 0 )
                        {
                            variant.Add( i + 1 );
                        }
                    }

                    return variant;
                }
            }

            void DistributeAllSolutionsByItems()
            {
                Parallel.Invoke( () => FindAllRowsSolutions(), () => FindAllColsSolutions() );

                void FindAllRowsSolutions()
                {
                    allRowsSolutions = new List<List<int>>();

                    foreach ( var rowNumber in _rowsNumbers )
                    {
                        if ( rowNumber > 0 )
                        {
                            foreach ( var variantOfNumber in listAllSolutions )
                            {
                                if ( rowNumber == variantOfNumber.Sum( ( e ) => e ) )
                                {
                                    allRowsSolutions.Add( CompileSolution( variantOfNumber ) );
                                }
                            }
                        }
                        else
                        {
                            allRowsSolutions.Add( null );
                        }
                    }

                    List<int> CompileSolution( List<int> numbers )
                    {
                        var solution = new List<int>();
                        int col;

                        foreach ( var number in numbers )
                        {
                            col = number - 1;

                            if ( _colsNumbers[ col ] != 0 )
                            {
                                solution.Add( number );
                            }
                        }

                        return solution;
                    }
                }

                void FindAllColsSolutions()
                {
                    allColsSolutions = new List<List<int>>();
                    
                    foreach ( var colNumber in _colsNumbers )
                    {
                        if ( colNumber > 0 )
                        {
                            foreach ( var variantOfNumber in listAllSolutions )
                            {
                                if ( colNumber == variantOfNumber.Sum( ( e ) => e ) )
                                {
                                    allColsSolutions.Add( CompileSolution( variantOfNumber ) );
                                }
                            }
                        }
                        else
                        {
                            allColsSolutions.Add( null );
                        }
                    }

                    List<int> CompileSolution( List<int> numbers )
                    {
                        var solution = new List<int>();
                        int col;

                        foreach ( var number in numbers )
                        {
                            col = number - 1;

                            if ( _rowsNumbers[ col ] != 0 )
                            {
                                solution.Add( number );
                            }
                        }

                        return solution;
                    }
                }
            }

            void FilterSolutionsByNumbers()
            {
                Parallel.Invoke( () => FilterRowsSolutions(), () => FilterColsSolutions() );

                void FilterRowsSolutions()
                {
                    foreach ( var number in _colsNumbers )
                    {
                        foreach ( var solution in allRowsSolutions )
                        {
                            if ( solution != null )
                            {
                                if ( solution.Sum() != number )
                                {
                                    solution.Clear();
                                }
                            }
                        }
                    }
                }

                void FilterColsSolutions()
                {
                    foreach ( var number in _rowsNumbers )
                    {
                        foreach ( var solution in allColsSolutions )
                        {
                            if ( solution != null )
                            {
                                if ( solution.Sum() == number )
                                {
                                    solution.Clear();
                                }
                            }
                        }
                    }
                }
            }

            bool[,] MergeSolution()
            {
                _solve = new bool[ RowCount, ColCount ];
                Parallel.Invoke( () => AddRowsSolution(), () => AddColsSolution() );
                return _solve;

                void AddRowsSolution()
                {
                    int i, j;
                    int col;

                    for ( i = 0; i < RowCount; i++ )
                    {
                        if ( allRowsSolutions[ i ] != null )
                        {
                            for ( j = 0; j < allRowsSolutions[ i ].Count; j++ )
                            {
                                col = allRowsSolutions[ i ][ j ] - 1;
                                _solve[ i, col ] = true;
                            }
                        }                        
                    }
                }

                void AddColsSolution()
                {
                    int i, j;
                    int row;

                    for ( i = 0; i < RowCount; i++ )
                    {
                        if ( allColsSolutions[ i ] != null )
                        {
                            for ( j = 0; j < allColsSolutions[ i ].Count; j++ )
                            {
                                row = allColsSolutions[ i ][ j ] - 1;
                                _solve[ row, i ] = true;
                            }
                        }
                    }
                }
            }
        }

        public bool IsCorrectSolution()
        {
            if ( _solve is null )
            {
                throw new Exception( @"The task was not solved" );
            }

            var isCorrectRows = false;
            var isCorrectCols = false;
            Parallel.Invoke( () => CheckingRows(), () => CheckingCols() );
            return isCorrectRows == true && isCorrectCols == true;

            void CheckingRows()
            {
                int i, j;
                int sum, countCorrectRows;

                for ( i = countCorrectRows = 0; i < RowCount; i++ )
                {
                    for ( j = sum = 0; j < ColCount; j++ )
                    {
                        sum += _solve[ i, j ] ? j + 1 : 0;
                    }

                    countCorrectRows += sum == _rowsNumbers[ i ] ? 1 : 0;
                }

                isCorrectRows = countCorrectRows == RowCount;
            }

            void CheckingCols()
            {
                int i, j;
                int sum, countCorrectCols;

                for ( j = countCorrectCols = 0; j < ColCount; j++ )
                {
                    for ( i = sum = 0; i < RowCount; i++ )
                    {
                        sum += _solve[ i, j ] ? i + 1 : 0;
                    }

                    countCorrectCols += sum == _colsNumbers[ j ] ? 1 : 0;
                }

                isCorrectCols = countCorrectCols == ColCount;
            }
        }

        public override string ToString()
        {
            return Convert( _solve, ( i ) => i ? '█' : ' ' );
        }
    }
}
