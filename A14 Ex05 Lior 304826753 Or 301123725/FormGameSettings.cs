namespace A14_Ex05
{
     using System;
     using System.Windows.Forms;
     using System.Drawing;

     public class FormGameSettings : Form
     {
          private Label label1;
          private Label label2;
          private Label label3;
          private Label label4;
          private Label label5;
          private Label label6;
          private TextBox TextboxPlayer1;
          private CheckBox CheckboxPlayer2;
          private Button ButtonStart;
          private NumericUpDown nUDRows;
          private NumericUpDown nUDCols;
          private TextBox TextboxPlayer2;

          public FormGameSettings()
          {
               InitializeComponent();
          }

          private void InitializeComponent()
          {
               this.label1 = new System.Windows.Forms.Label();
               this.label2 = new System.Windows.Forms.Label();
               this.label3 = new System.Windows.Forms.Label();
               this.label4 = new System.Windows.Forms.Label();
               this.label5 = new System.Windows.Forms.Label();
               this.label6 = new System.Windows.Forms.Label();
               this.TextboxPlayer1 = new System.Windows.Forms.TextBox();
               this.TextboxPlayer2 = new System.Windows.Forms.TextBox();
               this.CheckboxPlayer2 = new System.Windows.Forms.CheckBox();
               this.ButtonStart = new System.Windows.Forms.Button();
               this.nUDRows = new System.Windows.Forms.NumericUpDown();
               this.nUDCols = new System.Windows.Forms.NumericUpDown();
               ((System.ComponentModel.ISupportInitialize)(this.nUDRows)).BeginInit();
               ((System.ComponentModel.ISupportInitialize)(this.nUDCols)).BeginInit();
               this.SuspendLayout();
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.Location = new System.Drawing.Point(33, 24);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(44, 13);
               this.label1.TabIndex = 0;
               this.label1.Text = "Players:";
               // 
               // label2
               // 
               this.label2.AutoSize = true;
               this.label2.Location = new System.Drawing.Point(49, 49);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(48, 13);
               this.label2.TabIndex = 1;
               this.label2.Text = "Player 1:";
               // 
               // label3
               // 
               this.label3.AutoSize = true;
               this.label3.Location = new System.Drawing.Point(78, 82);
               this.label3.Name = "label3";
               this.label3.Size = new System.Drawing.Size(48, 13);
               this.label3.TabIndex = 2;
               this.label3.Text = "Player 2:";
               // 
               // label4
               // 
               this.label4.AutoSize = true;
               this.label4.Location = new System.Drawing.Point(36, 133);
               this.label4.Name = "label4";
               this.label4.Size = new System.Drawing.Size(61, 13);
               this.label4.TabIndex = 3;
               this.label4.Text = "Board Size:";
               // 
               // label5
               // 
               this.label5.AutoSize = true;
               this.label5.Location = new System.Drawing.Point(49, 163);
               this.label5.Name = "label5";
               this.label5.Size = new System.Drawing.Size(37, 13);
               this.label5.TabIndex = 4;
               this.label5.Text = "Rows:";
               // 
               // label6
               // 
               this.label6.AutoSize = true;
               this.label6.Location = new System.Drawing.Point(157, 163);
               this.label6.Name = "label6";
               this.label6.Size = new System.Drawing.Size(30, 13);
               this.label6.TabIndex = 5;
               this.label6.Text = "Cols:";
               // 
               // TextboxPlayer1
               // 
               this.TextboxPlayer1.Location = new System.Drawing.Point(137, 46);
               this.TextboxPlayer1.Name = "TextboxPlayer1";
               this.TextboxPlayer1.Size = new System.Drawing.Size(100, 20);
               this.TextboxPlayer1.TabIndex = 6;
               this.TextboxPlayer1.TextChanged += new System.EventHandler(this.TextboxPlayer1_TextChanged);
               // 
               // TextboxPlayer2
               // 
               this.TextboxPlayer2.Enabled = false;
               this.TextboxPlayer2.Location = new System.Drawing.Point(137, 79);
               this.TextboxPlayer2.Name = "TextboxPlayer2";
               this.TextboxPlayer2.Size = new System.Drawing.Size(100, 20);
               this.TextboxPlayer2.TabIndex = 7;
               this.TextboxPlayer2.Text = "[Computer]";
               this.TextboxPlayer2.TextChanged += new System.EventHandler(this.TextboxPlayer2_TextChanged);
               // 
               // CheckboxPlayer2
               // 
               this.CheckboxPlayer2.AutoSize = true;
               this.CheckboxPlayer2.Location = new System.Drawing.Point(62, 81);
               this.CheckboxPlayer2.Name = "CheckboxPlayer2";
               this.CheckboxPlayer2.Size = new System.Drawing.Size(15, 14);
               this.CheckboxPlayer2.TabIndex = 8;
               this.CheckboxPlayer2.UseVisualStyleBackColor = true;
               this.CheckboxPlayer2.CheckedChanged += new System.EventHandler(this.CheckboxPlayer2_CheckedChanged);
               // 
               // ButtonStart
               // 
               this.ButtonStart.Enabled = false;
               this.ButtonStart.Location = new System.Drawing.Point(28, 207);
               this.ButtonStart.Name = "ButtonStart";
               this.ButtonStart.Size = new System.Drawing.Size(238, 23);
               this.ButtonStart.TabIndex = 9;
               this.ButtonStart.Text = "Start!";
               this.ButtonStart.UseVisualStyleBackColor = true;
               this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
               // 
               // nUDRows
               // 
               this.nUDRows.Location = new System.Drawing.Point(95, 161);
               this.nUDRows.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
               this.nUDRows.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
               this.nUDRows.Name = "nUDRows";
               this.nUDRows.Size = new System.Drawing.Size(44, 20);
               this.nUDRows.TabIndex = 10;
               this.nUDRows.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
               // 
               // nUDCols
               // 
               this.nUDCols.Location = new System.Drawing.Point(193, 159);
               this.nUDCols.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
               this.nUDCols.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
               this.nUDCols.Name = "nUDCols";
               this.nUDCols.Size = new System.Drawing.Size(44, 20);
               this.nUDCols.TabIndex = 11;
               this.nUDCols.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
               // 
               // FormGameSettings
               // 
               this.AutoSize = true;
               this.ClientSize = new System.Drawing.Size(290, 262);
               this.Controls.Add(this.nUDCols);
               this.Controls.Add(this.nUDRows);
               this.Controls.Add(this.ButtonStart);
               this.Controls.Add(this.CheckboxPlayer2);
               this.Controls.Add(this.TextboxPlayer2);
               this.Controls.Add(this.TextboxPlayer1);
               this.Controls.Add(this.label6);
               this.Controls.Add(this.label5);
               this.Controls.Add(this.label4);
               this.Controls.Add(this.label3);
               this.Controls.Add(this.label2);
               this.Controls.Add(this.label1);
               this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
               this.MaximizeBox = false;
               this.MinimizeBox = false;
               this.Name = "FormGameSettings";
               this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
               this.Text = "Game Settings";
               ((System.ComponentModel.ISupportInitialize)(this.nUDRows)).EndInit();
               ((System.ComponentModel.ISupportInitialize)(this.nUDCols)).EndInit();
               this.ResumeLayout(false);
               this.PerformLayout();
          }

          public int NumOfCols
          {
               get { return (int)nUDCols.Value; }
          }

          public int NumOfRows
          {
               get { return (int)nUDRows.Value; }
          }

          public bool IsAgainstComputer
          {
               get { return !CheckboxPlayer2.Checked; }
          }

          /// <summary>
          /// when checkbox is checked, the user needs to enter player2 name
          /// when checkbox is unchecked, the default is computer player
          /// </summary>
          /// <param name="sender"></param>
          /// <param name="e"></param>
          private void CheckboxPlayer2_CheckedChanged(object sender, EventArgs e)
          {
               CheckBox checkboxPlayer2 = sender as CheckBox;
               if (checkboxPlayer2.Checked)
               {
                    TextboxPlayer2.Enabled = true;
                    TextboxPlayer2.Text = string.Empty;
               }
               else
               {
                    TextboxPlayer2.Enabled = false;
                    TextboxPlayer2.Text = "[Computer]";
               }
          }

          /// <summary>
          /// Both Player Names should be non-empty in order for start button to be enabled
          /// </summary>
          /// <param name="sender"></param>
          /// <param name="e"></param>
          private void TextboxPlayer1_TextChanged(object sender, EventArgs e)
          {
               ButtonStart.Enabled = true;
               if (TextboxPlayer2.Text == string.Empty || TextboxPlayer1.Text == string.Empty)
               {
                    ButtonStart.Enabled = false;
               }
          }

          private void TextboxPlayer2_TextChanged(object sender, EventArgs e)
          {
               ButtonStart.Enabled = true;
               if (TextboxPlayer2.Text == string.Empty || TextboxPlayer1.Text == string.Empty)
               {
                    ButtonStart.Enabled = false;
               }
          }

          private void ButtonStart_Click(object sender, EventArgs e)
          {
               this.DialogResult = System.Windows.Forms.DialogResult.OK;
               this.Close();
          }

          public string Player1Name 
          {
               get { return TextboxPlayer1.Text; }
          }

          public string Player2Name
          {
               get { return TextboxPlayer2.Text; }
          }
     }
}
