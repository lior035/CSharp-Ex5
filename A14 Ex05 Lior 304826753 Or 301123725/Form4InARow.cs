namespace A14_Ex05
{
     using System;
     using System.Windows.Forms;
     using System.Drawing;
     using System.Collections.Generic;
     
     public class Form4InARow : Form
     {
          private const int k_MatrixButtonWidth = 40;
          private const int k_MatrixButtonHeight = 40;          
          private const int k_SpaceBetweenButtons = 10;          
          private const int k_ColButtonHeight = 20;
          private const int k_PlayerLabelsHeight = 30;

          private int m_NumOfBoardRows;
          private int m_NumOfBoardCols;
          private string m_Player1Name;
          private string m_Player2Name;
          private FormGameSettings m_FormGameSettings = new FormGameSettings();
          private ColButton[] m_ColButtonsArray;
          private MatrixButton[,] m_BoardMatrix;
          private GameManager m_GameManager;
          private Label LabelPlayer1Name = new Label();
          private Label LabelPlayer2Name = new Label();
          
          /// <summary>
          /// This form is designed dynamically after getting the data from formGameSettings 
          /// </summary>
          public Form4InARow()
          {
               int matchType;               
               getBoardSizeMatchTypeAndPlayerNames(out matchType);
               if (m_FormGameSettings.DialogResult == DialogResult.OK)
               {
                    m_GameManager = new GameManager(m_NumOfBoardRows, m_NumOfBoardCols, matchType);
                    // $G$ SFN-012 (+5) Bonus: Events in the Logic layer are handled by the UI.
                    m_GameManager.GameOver += new GameOverEventHandler(m_GameManager_GameOver);
                    m_GameManager.ColumnFull += new ColumnFullEventHandler(m_GameManager_ColumnFull);
                    InitializeComponent();
                    this.ShowDialog();
               }               
          }

          private void InitializeComponent()
          {
               this.Text = "4 in a Row!!";
               m_ColButtonsArray = new ColButton[m_NumOfBoardCols];
               m_BoardMatrix = new MatrixButton[m_NumOfBoardRows, m_NumOfBoardCols];
               this.Width = k_SpaceBetweenButtons + (m_NumOfBoardCols * (k_MatrixButtonWidth + k_SpaceBetweenButtons)) + (2 * k_SpaceBetweenButtons);
               this.Height = ((m_NumOfBoardRows + 1) * (k_MatrixButtonWidth + k_SpaceBetweenButtons)) + (k_SpaceBetweenButtons * 3) + k_ColButtonHeight + k_PlayerLabelsHeight;
               this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
               this.StartPosition = FormStartPosition.CenterParent;
               this.MaximizeBox = false;
               generateColButtonsArray(m_NumOfBoardCols);
               generateMatrixButtons(m_NumOfBoardRows, m_NumOfBoardCols);
               addPlayerLables();
          }

          /// <summary>
          /// disabling the column button when corresponding column gets full
          /// </summary>
          /// <param name="i_IndexOfFullColumn"></param>
          private void m_GameManager_ColumnFull(int i_IndexOfFullColumn)
          {
               this.m_ColButtonsArray[i_IndexOfFullColumn].Enabled = false;
          }

          /// <summary>
          /// event handling of game over mode
          /// showing a message box asking for new game/quit and perform newgame/exit according to choice
          /// </summary>
          /// <param name="i_GameIsWon"></param>
          private void m_GameManager_GameOver(bool i_GameIsWon)
          {
               string title, caption;
               getTitleAndCaption(out title, out caption, i_GameIsWon);
               System.Windows.Forms.DialogResult dialogResult = MessageBox.Show(caption, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
               if (dialogResult == DialogResult.Yes)
               {
                    this.NewGame();
               }
               else
               {
                    this.Close();
               }
          }

          /// <summary>
          /// a FormGameSetting is shown and the data is recieved for main form settings after the form is closed
          /// </summary>
          /// <param name="o_IntMatchType">if player 2 inserted its PlayerVsPlayer otherwise on default its PlayerVsComputer</param>
          private void getBoardSizeMatchTypeAndPlayerNames(out int o_IntMatchType)
          {
               m_FormGameSettings.ShowDialog();
               m_NumOfBoardCols = m_FormGameSettings.NumOfCols;
               m_NumOfBoardRows = m_FormGameSettings.NumOfRows;
               m_Player1Name = m_FormGameSettings.Player1Name;
               if (m_FormGameSettings.IsAgainstComputer)
               {
                    m_Player2Name = "Computer";
                    o_IntMatchType = GameManager.k_PlayerVsComputer;
               }
               else
               {
                    m_Player2Name = m_FormGameSettings.Player2Name;
                    o_IntMatchType = GameManager.k_PlayerVsPlayer;
               }             
          }   
          
          private void addPlayerLables()
          {
               updateLabelsText();
               LabelPlayer1Name.AutoSize = true;
               LabelPlayer2Name.AutoSize = true;
               LabelPlayer1Name.Top = this.Height - k_PlayerLabelsHeight - k_MatrixButtonHeight;               
               LabelPlayer1Name.Left = (this.Width / 2) - 20 - (int)LabelPlayer1Name.CreateGraphics().MeasureString(LabelPlayer1Name.Text, LabelPlayer1Name.Font).Width;
               LabelPlayer2Name.Left = (this.Width / 2) + 10;
               LabelPlayer2Name.Top = LabelPlayer1Name.Top;              
               this.Controls.Add(LabelPlayer1Name);
               this.Controls.Add(LabelPlayer2Name);
          }

          private void updateLabelsText()
          {
               int player1Score, player2Score;
               this.m_GameManager.GetPlayerScoreByIndex(0, out player1Score);
               this.m_GameManager.GetPlayerScoreByIndex(1, out player2Score);
               LabelPlayer1Name.Text = string.Format("{0}: {1}", Player1Name, player1Score);
               LabelPlayer2Name.Text = string.Format("{0}: {1}", Player2Name, player2Score);
          }

          /// <summary>
          /// generating the buttons which are pressed in order to insert coins
          /// </summary>
          /// <param name="i_NumOfCols"></param>
          private void generateColButtonsArray(int i_NumOfCols)
          {
               for (int colIndex = 0; colIndex < i_NumOfCols; colIndex++)
               {
                    ColButton colButtonToGenerate = new ColButton(colIndex);

                    colButtonToGenerate.Click += new EventHandler(colButtonToGenerate_Click);
                    int ColButtonXLocation = ((colButtonToGenerate.Width + 10) * colIndex) + k_SpaceBetweenButtons;
                    colButtonToGenerate.Location = new Point(ColButtonXLocation, 15);
                    m_ColButtonsArray[colIndex] = colButtonToGenerate;
                    this.Controls.Add(colButtonToGenerate);
               }
          }
               
          /// <summary>
          /// event handling of player move(clicking on a col button)
          /// </summary>
          /// <param name="i_colButtonIndex"></param>
          private void colButtonToGenerate_Click(object sender, EventArgs e)
          {
               ColButton clickedButton = sender as ColButton;
               m_GameManager.MakeTurn(clickedButton.ButtonIndex);
          }
      
          private void generateMatrixButtons(int i_NumOfRows, int i_NumOfCols)
          {
               int colIndex, rowIndex;
               int positionButtonCol, positionButtonRow;
               for (rowIndex = 0; rowIndex < i_NumOfRows; rowIndex++)
               {
                    for (colIndex = 0; colIndex < i_NumOfCols; colIndex++)
                    {
                         MatrixButton matrixButtonToGenerate = new MatrixButton(m_GameManager.LogicGameBoard.GameBoard[rowIndex, colIndex]);                         
                         positionButtonCol = ((matrixButtonToGenerate.Width + k_SpaceBetweenButtons) * colIndex) + k_SpaceBetweenButtons;
                         positionButtonRow = ((matrixButtonToGenerate.Height + k_SpaceBetweenButtons) * rowIndex) + (k_SpaceBetweenButtons * 3) + k_ColButtonHeight;
                         matrixButtonToGenerate.Location = new Point(positionButtonCol, positionButtonRow);
                         m_BoardMatrix[rowIndex, colIndex] = matrixButtonToGenerate;
                         this.Controls.Add(matrixButtonToGenerate);
                    }
               }
          }

          public int NumOfBoardRows
          {
               get { return m_NumOfBoardRows; }
          }

          public int NumOfBoarCols
          {
               get { return m_NumOfBoardCols; }
          }

          public string Player1Name
          {
               get { return m_Player1Name; }
          }

          public string Player2Name
          {
               get { return m_Player2Name; }
          }
        
          public void FillMatrixButtonText(int i_CellRow, int i_CellCol, Board.eBoardCellTypes i_TypeToFillCell)
          {
               if (i_TypeToFillCell == Board.eBoardCellTypes.Circle)
               {
                    m_BoardMatrix[i_CellRow, i_CellCol].Text = "O";
               }
               else if (i_TypeToFillCell == Board.eBoardCellTypes.X)
               {
                    m_BoardMatrix[i_CellRow, i_CellCol].Text = "X";
               }
               else
               {
                    m_BoardMatrix[i_CellRow, i_CellCol].Text = string.Empty;
               }
          }
                                                                    
          private void DisableColButton(int i_ColInput)
          {
               m_ColButtonsArray[i_ColInput].Enabled = false;
          }
        
          /// <summary>
          /// function called when new game happens
          /// game matrix of matrix buttons text is emptied and score is updated
          /// </summary>
          private void ResetForm()
          {
               for (int col = 0; col < m_NumOfBoardCols; col++)
               {
                    m_ColButtonsArray[col].Enabled = true;
                    for (int row = 0; row < m_NumOfBoardRows; row++)
                    {
                         m_BoardMatrix[row, col].Text = string.Empty;
                         updateLabelsText();
                    }
               }               
          }
          
          private void NewGame()
          {
               m_GameManager.GameStatus = GameManager.eGameStatus.Player1Turn;
               m_GameManager.LogicGameBoard.ResetBoard(m_NumOfBoardRows, m_NumOfBoardCols);
               this.ResetForm();
          }

          /// <summary>
          /// auxillary function to get message box's title and caption
          /// </summary>
          /// <param name="title"></param>
          /// <param name="o_Caption"></param>
          /// <param name="i_GameIsWon">win/tie - message is designed accordingly</param>
          private void getTitleAndCaption(out string o_Title, out string o_Caption, bool i_GameIsWon)
          {
               if (i_GameIsWon)
               {
                    o_Title = "A Win!";
                    if (m_GameManager.GameStatus == GameManager.eGameStatus.Player1Turn)
                    {
                         o_Caption = string.Format("{0} Won!!{1}Another Round?", this.Player1Name, System.Environment.NewLine);
                    }
                    else
                    {
                         o_Caption = string.Format("{0} Won!!{1}Another Round?", this.Player2Name, System.Environment.NewLine);
                    }
               }
               else
               {
                    ////tie
                    o_Title = "A Tie!";
                    o_Caption = string.Format("Tie!!{0}Another Round?", System.Environment.NewLine);
               }
          }

          /// <summary>
          /// buttons which make move when enabled and clicked
          /// generated in an array dynamically by col size input
          /// </summary>
          private class ColButton : Button
          {
               protected int m_ButtonIndex;

               public ColButton(int i_ButtonIndex)
               {
                    m_ButtonIndex = i_ButtonIndex;
                    InitializeComponent();
               }

               public int ButtonIndex
               {
                    get { return m_ButtonIndex; }
               }

               public void InitializeComponent()
               {
                    this.Name = "button" + m_ButtonIndex.ToString();
                    this.Size = new System.Drawing.Size(40, 20);
                    this.Text = (m_ButtonIndex + 1).ToString();
                    this.UseVisualStyleBackColor = true;
               }
          }

          /// <summary>
          /// button of the matrix representing the game board which is generated dynamically after getting row/col size from settings
          /// the matrix text is influenced by game logic from event created by a LogicBoardCell member
          /// </summary>
          private class MatrixButton : Button
          {
               private LogicBoardCell m_MathcingLogicCell;

               public MatrixButton(LogicBoardCell i_MatchingLogicCell)
               {
                    m_MathcingLogicCell = i_MatchingLogicCell;
                    m_MathcingLogicCell.LogicValueChanged += new LogicValueChangeEventHandler(m_MathcingLogicCell_LogicValueChanged);
                    InitializeComponent();
               }

               public void m_MathcingLogicCell_LogicValueChanged(object sender)
               {
                    LogicBoardCell logicBoardCell = sender as LogicBoardCell;
                    if (logicBoardCell.CellType == Board.eBoardCellTypes.Circle)
                    {
                         this.Text = "O";
                    }
                    else if (logicBoardCell.CellType == Board.eBoardCellTypes.X)
                    {
                         this.Text = "X";
                    }
                    else
                    {
                         this.Text = string.Empty;
                    }
               }

               public void InitializeComponent()
               {
                    this.Size = new System.Drawing.Size(k_MatrixButtonWidth, k_MatrixButtonHeight);
                    this.UseVisualStyleBackColor = true;
               }
          }
     }
}
