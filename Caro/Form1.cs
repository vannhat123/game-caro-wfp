using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caro
{
    public partial class Form1 : Form
    {

        #region Properties
        CheckBoardManager ChessBoard;
        #endregion
        public Form1()
        {
            InitializeComponent();

            ChessBoard = new CheckBoardManager(pnlChessBoard, textPlayerName,ptbMark);
            ChessBoard.EndedGame +=ChessBoard_EndedGame;
            ChessBoard.PlayerMarked += ChessBoard_PlayerMarked;


            pgbCoolDown.Step = Cons.COOL_DOWN_STEP;
            pgbCoolDown.Maximum = Cons.COOL_DOWN_TIME;
            pgbCoolDown.Value = 0;

            tmCoolDown.Interval = Cons.COOL_DOWN_INTRVAL;

            NewGame();
        }

        #region Methods
        void NewGame()
        {
            pgbCoolDown.Value = 0;
            tmCoolDown.Stop();
            undoToolStripMenuItem.Enabled = true;
            ChessBoard.DrawChessBoard();
        }

        void Quit()
        {
            Application.Exit();
        }

        void Undo()
        {
            ChessBoard.Undo();
        }


        void Endgame()
        {
            tmCoolDown.Stop();
            pnlChessBoard.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
            MessageBox.Show("Ket thuc");
        }

        void ChessBoard_PlayerMarked(object sender, EventArgs e)
        {
            tmCoolDown.Start();
            pgbCoolDown.Value = 0;
        }

         void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            Endgame();
        }

        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            pgbCoolDown.PerformStep();

            if (pgbCoolDown.Value >= pgbCoolDown.Maximum)
            {
                
                Endgame(); 
            }
        }

      

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Ban co chac muon thoat", "Thong bao", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
                e.Cancel = true;


        }

        #endregion

    }
}
