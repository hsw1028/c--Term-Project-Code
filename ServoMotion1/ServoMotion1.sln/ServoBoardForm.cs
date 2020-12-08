using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AjinMotion;

namespace ServoMotion1
{
    public partial class ServoBoardForm : Form
    {
        public ServoBoardForm()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void ServoBoardForm_Load(object sender, EventArgs e)
        {

        }
        private void btnOpenBoard_Click(object sender, EventArgs e)
        {
            if (ServoCmd.BoardOpen() == 1) MessageBox.Show("보드 열기 성공", "Servo Message",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnCloseBoard_Click(object sender, EventArgs e)
        {
            if (ServoCmd.BoardClose() == 0) MessageBox.Show("보드 닫기 실패", "Servo Message",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnStatus_Click(object sender, EventArgs e)
        {
            int BoardCount = -1;
            int AxisCount = -1;
            ServoCmd.GetBoardInfo(ref BoardCount, ref AxisCount);
            labBoardNo.Text = Convert.ToString(BoardCount);
            labAxisNo.Text = Convert.ToString(AxisCount);
            if (ServoCmd.GetBoardStatus(BoardCount - 1) == 1) labBoardStatus.Text = "OK";
            else labBoardStatus.Text = "Not Ready";
        }
        private void btnLoadPara_Click(object sender, EventArgs e)
        {
            if (ServoCmd.LoadParameters("init_para.mot") == 1) MessageBox.Show("Parameters 열기 성공", "Servo Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Parameters 열기 실패", "Servo Message", MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        }
    }
}
