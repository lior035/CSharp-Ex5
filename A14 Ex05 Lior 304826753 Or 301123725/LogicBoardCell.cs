using System;
using System.Collections.Generic;
using System.Text;

namespace A14_Ex05
{
    // $G$ NTT-999 (0) You should adhere to the .NET standard when using events. The event's signature should be in the form of void (object sender, EventArgs e), when e can also be a class which is derived from EventArgs.
     public delegate void LogicValueChangeEventHandler(object sender);
     
     /// <summary>
     /// this class is the component of the logic board of the game
     /// when the logical value is chanced it notifies the corresponding interface button and cause it to change its text according
     /// to the new logical value
     /// </summary>
     public class LogicBoardCell
     {
          private Board.eBoardCellTypes m_CellType;

          public event LogicValueChangeEventHandler LogicValueChanged;

          public LogicBoardCell()
          {
               m_CellType = Board.eBoardCellTypes.Empty;
          }

          public Board.eBoardCellTypes CellType
          {
               get { return m_CellType; }
               set
               {
                    m_CellType = value;
                    // $G$ CSS-999 (-5) The method which is responsible for raising the event should be constructed with the format of OnXXX (while XXX is the event's name).
                    if (LogicValueChanged != null)
                    {
                         LogicValueChanged.Invoke(this);
                    }
               }
          }
     }
}
