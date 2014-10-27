namespace A14_Ex05
{
     using System.Collections.Generic;
     using System.Windows.Forms;
    
     public delegate void GameOverEventHandler(bool i_GameIsWon);

     public delegate void ColumnFullEventHandler(int i_IndexOfFullColumn);

     public class GameManager
     {          
          public const int k_MinBoardSize = 4;
          public const int k_MaxBoardSize = 8;
          public const int k_PlayerVsPlayer = 1;
          public const int k_PlayerVsComputer = 2;     
          public const int k_NumOfPlayers = 2;
          public const int k_HumanPlayer = 0;
          public const int k_ComputerPlayer = 1;
          
          public event GameOverEventHandler GameOver;

          // $G$ DSN-999 (-3) This delegate should be managed by the GameBoard object.
          public event ColumnFullEventHandler ColumnFull;
          
          private List<Player> m_PlayersArray;
          private eGameStatus m_GameStatus;
          private Board m_LogicGameBoard;
          private eMatchType m_MatchType;
                    
          public GameManager(int i_NumOfBoardRows, int i_NumOfBoardCols, int i_IntMatchType)
          {
               m_LogicGameBoard = new Board(i_NumOfBoardRows, i_NumOfBoardCols);
               m_GameStatus = eGameStatus.Player1Turn;
               m_PlayersArray = new List<Player>(k_NumOfPlayers);
               m_PlayersArray.Insert(0, new Player());
               if (i_IntMatchType == k_PlayerVsPlayer)
               {
                    m_MatchType = eMatchType.PlayerVsPlayer;
                    m_PlayersArray.Insert(1, new Player());
               }
               else
               {
                    m_MatchType = eMatchType.PlayerVsComputer;
                    m_PlayersArray.Insert(1, new Player());
               }                            
          }   

          public enum eGameStatus
          {
               Player1Turn,
               Player2Turn,
               GameOver,
          }

          public eGameStatus GameStatus
          {
               get { return m_GameStatus; }
               set { m_GameStatus = value; }
          }

          public Board LogicGameBoard
          {
               get { return m_LogicGameBoard; }
          }

          public enum eMatchType
          {
               PlayerVsPlayer,
               PlayerVsComputer,
          }

          public eMatchType MatchType
          {
               get { return m_MatchType; }
          }

          /// <summary>
          /// updates the game score according to game status
          /// </summary>
          /// <param name="i_GameIsWon"></param>
          private void updateScore(bool i_GameIsWon)
          {
               if (i_GameIsWon)
               {
                    if (m_GameStatus == eGameStatus.Player1Turn)
                    {
                         m_PlayersArray[0].IncreaseScore();
                    }
                    else
                    {
                         m_PlayersArray[1].IncreaseScore();
                    }
               }
          }

          public bool GetPlayerScoreByIndex(int i_Index, out int o_Score)
          {
               bool result = false;
               o_Score = 0;

               if (i_Index >= 0 && i_Index < m_PlayersArray.Count)
               {
                    o_Score = m_PlayersArray[i_Index].Score;
                    result = true;
               }

               return result;
          }

          public void MakeTurn(int i_Col)
          {
               Player.MakeHumanMove(this, i_Col);              
          }

          public void InsertMoveToGameBoard(int i_ColInput)
          {
               int indexOfFirstAvailableRowInInputCol = m_LogicGameBoard.FirstAvailablePoisitionInCol[i_ColInput];
               LogicBoardCell cellToInsert = m_LogicGameBoard.GameBoard[m_LogicGameBoard.RowSize - indexOfFirstAvailableRowInInputCol - 1, i_ColInput];

               if (m_GameStatus == eGameStatus.Player1Turn)
               {
                    cellToInsert.CellType = Board.eBoardCellTypes.Circle;                                        
               }
               else
               {
                    cellToInsert.CellType = Board.eBoardCellTypes.X;                    
               }

               m_LogicGameBoard.FirstAvailablePoisitionInCol[i_ColInput]++;
          }

          public void EraseMoveFromGameBoard(int i_ColInput)
          {
               int indexOfFirstAvailableRowInInputCol = m_LogicGameBoard.FirstAvailablePoisitionInCol[i_ColInput];
               m_LogicGameBoard.GameBoard[m_LogicGameBoard.RowSize - indexOfFirstAvailableRowInInputCol, i_ColInput].CellType = Board.eBoardCellTypes.Empty;               
               m_LogicGameBoard.FirstAvailablePoisitionInCol[i_ColInput]--;
          }

          public void ChangePlayerTurn()
          {
               if (m_GameStatus == eGameStatus.Player1Turn)
               {
                    m_GameStatus = eGameStatus.Player2Turn;
               }
               else if (m_GameStatus == eGameStatus.Player2Turn)
               {
                    m_GameStatus = eGameStatus.Player1Turn;
               }
          }

          /// <summary>
          /// perform the logics according to move
          /// </summary>
          /// <param name="i_ColInput"></param>
          /// <param name="o_IsGameOver"></param>
          public void PerformMoveChanges(int i_ColInput, out bool o_IsGameOver)
          {
               this.InsertMoveToGameBoard(i_ColInput);
               m_LogicGameBoard.IncreaseOccupiedCells();
               updateGameStatus(i_ColInput, out o_IsGameOver);
          }

          /// <summary>
          /// if game is over the function will update the score and invoke game over event
          /// else changes player turn
          /// </summary>
          /// <param name="i_ColInput"></param>
          /// <param name="o_IsGameOver"></param>
          private void updateGameStatus(int i_ColInput, out bool o_IsGameOver)
          {
               bool gameIsWon;

               if (GameLogic.IsGameOver(i_ColInput, m_LogicGameBoard, out gameIsWon))
               {                    
                    o_IsGameOver = true;
                    this.updateScore(gameIsWon);
                    // $G$ NTT-999 (-10) You should make sure a delegate is not null before calling it.  
                    this.GameOver.Invoke(gameIsWon);     
               }
               else
               {
                    o_IsGameOver = false;
                    if (m_LogicGameBoard.FirstAvailablePoisitionInCol[i_ColInput] >= m_LogicGameBoard.RowSize)
                    {
                        // $G$ NTT-999 (0) You should make sure a delegate is not null before calling it.  
                         this.ColumnFull.Invoke(i_ColInput);
                    }

                    this.ChangePlayerTurn();
               }
          }          
     }
}