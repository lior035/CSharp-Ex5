namespace A14_Ex05
{
     using System.Collections;
     using System.Collections.Generic;

     public class GameLogic
     {
          private static readonly int sr_WinningStreakSize = 4;
          private static readonly int sr_LineRadiusToCheck = 3;

          /// <summary>
          /// this function checks whether there is a winning four a line
          /// </summary>
          /// <param name="i_LineOfEnums">generated for 4 directions to check 4 in a line streak</param>
          /// <param name="i_CurrCellEnum">type of cell according to player</param>
          /// <returns></returns>
          private static bool isFourInLine(Board.eBoardCellTypes[] i_LineOfEnums, Board.eBoardCellTypes i_CurrCellEnum)
          {
               bool fourInLineFound = false;
               int streakOfSameElement = 0;

               for (int i = 0; i < (sr_LineRadiusToCheck * 2) + 1; i++)
               {
                    if (i_CurrCellEnum.Equals(i_LineOfEnums[i]))
                    {
                         streakOfSameElement++;
                         if (streakOfSameElement == sr_WinningStreakSize)
                         {
                              fourInLineFound = true;
                              break;
                         }
                    }
                    else
                    {
                         streakOfSameElement = 0;
                    }
               }

               return fourInLineFound;
          }

          /// <summary>
          /// this function checks whether the move performed is a winning move
          /// it checks 4 in a row for 4 directions horizontal/vertical/right-cross/left-cross
          /// </summary>
          /// <param name="i_CurrentInsertCellCol">col of move</param>
          /// <param name="i_GameBoard"></param>
          /// <returns></returns>
          private static bool isWinningMove(int i_CurrentInsertCellCol, Board i_GameBoard)
          {
               int currentInsertCellRow = i_GameBoard.FirstAvailablePoisitionInCol[i_CurrentInsertCellCol] - 1;
               Board.eBoardCellTypes currentInsertCellEnum = i_GameBoard.GetCellEnum(i_GameBoard.RowSize - 1 - currentInsertCellRow, i_CurrentInsertCellCol);
               bool result = isFourInLine(generateLineOfCellEnumsFromBoardByDirection(i_GameBoard, Board.eBoardDirections.Horizontal, i_CurrentInsertCellCol), currentInsertCellEnum)
                          || isFourInLine(generateLineOfCellEnumsFromBoardByDirection(i_GameBoard, Board.eBoardDirections.Vertical, i_CurrentInsertCellCol), currentInsertCellEnum)
                          || isFourInLine(generateLineOfCellEnumsFromBoardByDirection(i_GameBoard, Board.eBoardDirections.RightCross, i_CurrentInsertCellCol), currentInsertCellEnum)
                          || isFourInLine(generateLineOfCellEnumsFromBoardByDirection(i_GameBoard, Board.eBoardDirections.LeftCross, i_CurrentInsertCellCol), currentInsertCellEnum);

               return result;
          }

          /// <summary>
          /// aux function for 4 in a row detection
          /// generating a row of board enums according to direction input
          /// </summary>
          /// <param name="i_GameBoard"></param>
          /// <param name="i_Direction"></param>
          /// <param name="i_CurrentInsertCellCol"></param>
          /// <returns></returns>
          private static Board.eBoardCellTypes[] generateLineOfCellEnumsFromBoardByDirection(Board i_GameBoard, Board.eBoardDirections i_Direction, int i_CurrentInsertCellCol)
          {
               int rowSize = i_GameBoard.RowSize;
               int currentInsertCellRow = i_GameBoard.FirstAvailablePoisitionInCol[i_CurrentInsertCellCol] - 1;
               int startOfLineRow, startOfLineCol, rowDirection, colDirection;
               Board.eBoardCellTypes[] result = new Board.eBoardCellTypes[(sr_LineRadiusToCheck * 2) + 1];

               for (int i = 0; i < result.Length; i++)
               {
                    result[i] = Board.eBoardCellTypes.Empty;
               }

               switch (i_Direction)
               {
                    case Board.eBoardDirections.Horizontal:
                         startOfLineCol = i_CurrentInsertCellCol - 3;
                         startOfLineRow = currentInsertCellRow;
                         rowDirection = 0;
                         colDirection = 1;
                         break;
                    case Board.eBoardDirections.Vertical:
                         startOfLineCol = i_CurrentInsertCellCol;
                         startOfLineRow = currentInsertCellRow - 3;
                         rowDirection = 1;
                         colDirection = 0;
                         break;
                    case Board.eBoardDirections.RightCross:
                         startOfLineCol = i_CurrentInsertCellCol - 3;
                         startOfLineRow = currentInsertCellRow - 3;
                         rowDirection = 1;
                         colDirection = 1;
                         break;
                    case Board.eBoardDirections.LeftCross:
                         startOfLineCol = i_CurrentInsertCellCol - 3;
                         startOfLineRow = currentInsertCellRow + 3;
                         rowDirection = -1;
                         colDirection = 1;
                         break;
                    default:
                         startOfLineRow = startOfLineCol = rowDirection = colDirection = 0;
                         break;
               }

               for (int i = 0; i < (sr_LineRadiusToCheck * 2) + 1; i++)
               {
                    int currentCellToCheckRow = startOfLineRow + (i * rowDirection);
                    int currentCellToCheckCol = startOfLineCol + (i * colDirection);
                    if (i_GameBoard.CheckIsInsideBoardBoundaries(currentCellToCheckRow, currentCellToCheckCol))
                    {
                         result[i] = i_GameBoard.GetCellEnum(rowSize - 1 - currentCellToCheckRow, currentCellToCheckCol);
                    }
               }

               return result;
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="i_ColInput"></param>
          /// <param name="i_GameBoard"></param>
          /// <returns>returns true if move is performed within column boundaries and in a non-full column</returns>
          public static bool IsLegalMove(int i_ColInput, Board i_GameBoard)
          {
               bool result;

               if (i_ColInput < 0 || i_ColInput > i_GameBoard.ColSize - 1)
               {
                    result = false;
               }
               else
               {
                    int indexOfFirstAvailableRowInInputCol = i_GameBoard.FirstAvailablePoisitionInCol[i_ColInput];
                    result = indexOfFirstAvailableRowInInputCol < i_GameBoard.RowSize;
               }

               return result;
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="i_ColInput"></param>
          /// <param name="i_GameBoard"></param>
          /// <param name="o_GameIsWon"></param>
          /// <returns>returns true is win or tie occur</returns>
          public static bool IsGameOver(int i_ColInput, Board i_GameBoard, out bool o_GameIsWon)
          {
               bool result;

               o_GameIsWon = isWinningMove(i_ColInput, i_GameBoard);
               result = o_GameIsWon || i_GameBoard.IsBoardFull();
               
               return result;
          }

          /// <summary>
          /// part of the computer move algorithm
          /// it checks the response of the opponent, checking if there is a possible win after move performed
          /// </summary>
          /// <param name="i_GameManager"></param>
          /// <param name="o_WinningMoveIndex"></param>
          /// <returns></returns>
          public static bool WinningMoveExists(GameManager i_GameManager, out int o_WinningMoveIndex)
          {
               bool result = false;

               o_WinningMoveIndex = 0;
               for (int col = 0; col < i_GameManager.LogicGameBoard.ColSize; col++)
               {
                    if (GameLogic.IsLegalMove(col, i_GameManager.LogicGameBoard))
                    {
                         i_GameManager.InsertMoveToGameBoard(col);
                         if (GameLogic.isWinningMove(col, i_GameManager.LogicGameBoard))
                         {
                              result = true;
                              o_WinningMoveIndex = col;
                         }

                         i_GameManager.EraseMoveFromGameBoard(col);
                         if (result)
                         {
                              break;
                         }
                    }
               }

               return result;
          }
     }
}