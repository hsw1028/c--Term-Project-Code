using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Digital_IO;
using AjinMotion;


namespace ServoMotion1
{
    public partial class Form1 : Form
    {
        MX_Com d_io = new MX_Com();
        SeqControl seq;
        private uint upStatus1, upStatus2, upStatus3, upStatus4;
        private double dpVelocity1, dpVelocity2, dpPos1, dpPos2, setVel1, setVel2, setPos1, setPos2, setPos3, setPos4;
        string vel1, vel2, pos1, pos2, s, t;
        public double red, white;
        List<int> Time2 = new List<int>();
        int[] Selected = new int[3];
        private int _minutes, _seconds, _cseconds, _data, k, m; //timer 시간 
        public bool a34, a38, a39; // 버튼을 bool형식으로 변환

        public Form1()
        {
            InitializeComponent();
            seq = new SeqControl(d_io);
            UpdateForm();
        }
        private void UpdateForm()
        {
            vel1 = Convert.ToString(dpVelocity1);
            vel2 = Convert.ToString(dpVelocity2);
            pos1 = Convert.ToString(dpPos1);
            pos2 = Convert.ToString(dpPos2);
            ServoCmd.Read_Status1(0, ref upStatus1);
            ServoCmd.Read_HomeStatus1(0, ref upStatus3);
            ServoCmd.Read_Status2(1, ref upStatus2);
            ServoCmd.Read_HomeStatus2(1, ref upStatus4);
            ServoCmd.Read_Vel1(0, ref dpVelocity1);
            ServoCmd.Read_Vel2(1, ref dpVelocity2);
            ServoCmd.Read_Pos1(0, ref dpPos1);
            ServoCmd.Read_Pos2(1, ref dpPos2);
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            s = Convert.ToString(k);
            button11.Text = s;
            t = Convert.ToString(m);
            button13.Text = t;
            
            #region 레드 워크 각 공정 논리
            if (d_io.GetPLCBit(Contact.X01) == true && d_io.GetPLCBit(Contact.Y20) == true)
            {
                seq01.BackColor = Color.Red;
            }
            if (d_io.GetPLCBit(Contact.X0F) == true && seq01.BackColor == Color.Red)
            {
                seq02.BackColor = Color.Red;
            }
            if (upStatus1 == 0 && upStatus2 == 0 && d_io.GetPLCBit(Contact.X0F) == true && dpPos2 > 400000 && seq02.BackColor == Color.Red)
            {
                seq03.BackColor = Color.Red;
            }
            if (d_io.GetPLCBit(Contact.X06) == true && d_io.GetPLCBit(Contact.Y26) == true && seq03.BackColor == Color.Red)
            {
                seq04.BackColor = Color.Red;
            }
            if (d_io.GetPLCBit(Contact.X06) == true && d_io.GetPLCBit(Contact.Y26) == true && d_io.GetPLCBit(Contact.X09) == true && d_io.GetPLCBit(Contact.Y28) == true && seq04.BackColor == Color.Red)
            {
                seq05.BackColor = Color.Red;
            }
            if (d_io.GetPLCBit(Contact.X07) == true && d_io.GetPLCBit(Contact.Y27) == true && dpPos2 > 400000 && d_io.GetPLCBit(Contact.X0F) == false && seq05.BackColor == Color.Red)
            {
                seq06.BackColor = Color.Red;
            }
            if (upStatus1 == 0 && upStatus2 == 0  && dpPos2 < 150000 && seq06.BackColor == Color.Red)
            {
                seq07.BackColor = Color.Red;
            }
            if (d_io.GetPLCBit(Contact.X06) == true && d_io.GetPLCBit(Contact.Y26) == true && seq07.BackColor == Color.Red)
            {
                seq08.BackColor = Color.Red;
            }
            if (d_io.GetPLCBit(Contact.X07) == true && d_io.GetPLCBit(Contact.Y27) == true && d_io.GetPLCBit(Contact.X08) == true && d_io.GetPLCBit(Contact.Y21) == true && seq08.BackColor == Color.Red)
            {
                seq09.BackColor = Color.Red;
            }
            /*if (upStatus1 == 0 && upStatus2 == 0 && dpPos1 < 1 && seq09.BackColor == Color.Red)
            {
                seq10.BackColor = Color.Red;
            }*/
            #endregion
            #region 화이트 워크 각 공정 논리
            if (d_io.GetPLCBit(Contact.X03) == true && d_io.GetPLCBit(Contact.Y22) == true)
            {
                seq11.BackColor = Color.White;
            }
            if (d_io.GetPLCBit(Contact.X0F) == true && seq11.BackColor == Color.White)
            {
                seq12.BackColor = Color.White;
            }
            if (upStatus1 == 0 && upStatus2 == 0 && d_io.GetPLCBit(Contact.X0F) == true && dpPos2 > 400000 && seq12.BackColor == Color.White)
            {
                seq13.BackColor = Color.White;
            }
            if (d_io.GetPLCBit(Contact.X06) == true && d_io.GetPLCBit(Contact.Y26) == true && seq13.BackColor == Color.White)
            {
                seq14.BackColor = Color.White;
            }
            if (d_io.GetPLCBit(Contact.X06) == true && d_io.GetPLCBit(Contact.Y26) == true && d_io.GetPLCBit(Contact.X09) == true && d_io.GetPLCBit(Contact.Y28) == true && seq14.BackColor == Color.White)
            {
                seq15.BackColor = Color.White;
            }
            if (d_io.GetPLCBit(Contact.X07) == true && d_io.GetPLCBit(Contact.Y27) == true && dpPos2 > 400000 && d_io.GetPLCBit(Contact.X0F) == false && seq15.BackColor == Color.White)
            {
                seq16.BackColor = Color.White;
            }
            if (upStatus1 == 0 && upStatus2 == 0 && dpPos2 < 270000 && seq16.BackColor == Color.White)
            {
                seq17.BackColor = Color.White;
            }
            if (d_io.GetPLCBit(Contact.X06) == true && d_io.GetPLCBit(Contact.Y26) == true && seq17.BackColor == Color.White)
            {
                seq18.BackColor = Color.White;
            }
            if (d_io.GetPLCBit(Contact.X07) == true && d_io.GetPLCBit(Contact.Y27) == true && d_io.GetPLCBit(Contact.X08) == true && d_io.GetPLCBit(Contact.Y21) == true && seq18.BackColor == Color.White)
            {
                seq19.BackColor = Color.White;
            }
            /*if (upStatus1 == 0 && upStatus2 == 0 && dpPos1 < 1 && seq19.BackColor == Color.White)
            {
                seq20.BackColor = Color.White;
            }*/
            #endregion
            #region 콤보박스
            if (comboBox1.SelectedText =="100퍼센트출력")
            {
                setVel1 = 60000;
                setVel2 = 150000;
            }
            else if (comboBox1.SelectedText == "50퍼센트출력")
            {
                setVel1 = 30000;
                setVel2 = 75000;
            }
            else if (comboBox1.SelectedText == "30퍼센트출력")
            {
                setVel1 = 18000;
                setVel2 = 45000;
            }
            #endregion
            for (int j = 1; j < Time2.Count(); j++)
            {
                if (k == 0 && d_io.GetPLCBit(Contact.X01) == true)
                {
                    setPos1 = 41562;
                    setPos2 = 88082;
                }
                else if (k == 1 && d_io.GetPLCBit(Contact.X01) == true)
                {
                    setPos1 = 41562;
                    setPos2 = 146345;
                }
                else if (k == 2 && d_io.GetPLCBit(Contact.X01) == true)
                {
                    setPos1 = 89903;
                    setPos2 = 88082;
                }
                else if (k == 3 && d_io.GetPLCBit(Contact.X01) == true)
                {
                    setPos1 = 89903;
                    setPos2 = 146345;
                }
                if (seq01.BackColor == Color.Red && d_io.GetPLCBit(Contact.X01) == true)
                {
                    seq.Conveyer_belt_backward();
                    seq.Cyl1_down();
                }
                if (seq02.BackColor == Color.Red && d_io.GetPLCBit(Contact.X0F) == true)
                {
                    seq.Conveyer_belt_stop();
                    ServoCmd.Move_set_pose(1, 405666, setVel2, 50000, 50000);
                    ServoCmd.Move_set_pose(0, -31956, setVel1, 30000, 30000);
                }
                if (seq03.BackColor == Color.Red)
                {
                    seq.Move_Cy1down();
                    seq.Gripper_Open();
                }
                if (seq04.BackColor == Color.Red)
                {
                    seq.Gripper_Close();
                }
                if (seq05.BackColor == Color.Red)
                {
                    seq.Move_Cy1up();
                }
                if (seq06.BackColor == Color.Red && dpPos2 > 400000)
                {
                    ServoCmd.Move_set_pose(1, setPos2, setVel2, 50000, 50000);   //123456값 다시 설정하기
                    ServoCmd.Move_set_pose(0, setPos1, setVel1, 30000, 30000);      // 23456값 다시 설정하기
                }
                if (seq07.BackColor == Color.Red)
                {
                    seq.Move_Cy1down();
                }
                if (seq08.BackColor == Color.Red)
                {
                    seq.Gripper_Open();
                    seq.Move_Cy1up();
                    s = Convert.ToString(k);
                    button11.Text = s;
                    if (setPos1 == 41562 && setPos2 == 88082)
                    {
                        label18.Text = "R";
                        k = 1;
                    }
                    else if (setPos1 == 41562 && setPos2 == 146345)
                    {
                        label19.Text = "R";
                        k = 2;
                    }
                    else if (setPos1 == 89903 && setPos2 == 88082)
                    {
                        label25.Text = "R";
                        k = 3;
                    }
                    else if (setPos1 == 89903 && setPos2 == 146345)
                    {
                        label24.Text = "R";
                        k = 4;
                    }
                    label18.ForeColor = Color.Red;
                    label19.ForeColor = Color.Red;
                    label24.ForeColor = Color.Red;
                    label25.ForeColor = Color.Red;
                }
                if (seq09.BackColor == Color.Red)
                {
                    seq.Gripper_Close();
                    if (k != red && upStatus1 == 0 && upStatus2 == 0)
                    {
                        seq.Cyl1_up();
                    }
                    else
                    {
                        seq01.BackColor = Color.Chartreuse;
                        seq02.BackColor = Color.Chartreuse;
                        seq03.BackColor = Color.Chartreuse;
                        seq04.BackColor = Color.Chartreuse;
                        seq05.BackColor = Color.Chartreuse;
                        seq06.BackColor = Color.Chartreuse;
                        seq07.BackColor = Color.Chartreuse;
                        seq08.BackColor = Color.Chartreuse;
                        seq09.BackColor = Color.Chartreuse;
                        label26.Text = "Red공정 완료!";
                        button11.BackColor = Color.Red;
                        seq.Cyl2_up();
                        break;
                    }
                    seq01.BackColor = Color.Chartreuse;
                    seq02.BackColor = Color.Chartreuse;
                    seq03.BackColor = Color.Chartreuse;
                    seq04.BackColor = Color.Chartreuse;
                    seq05.BackColor = Color.Chartreuse;
                    seq06.BackColor = Color.Chartreuse;
                    seq07.BackColor = Color.Chartreuse;
                    seq08.BackColor = Color.Chartreuse;
                    seq09.BackColor = Color.Chartreuse;
                }
            } //red워크 작업
            for (int c = 1; c < Time2.Count(); c++)
            {
                if (m == 0 && d_io.GetPLCBit(Contact.X03) == true)
                {
                    setPos3 = 41562;
                    setPos4 = 206194;
                }
                else if (m == 1 && d_io.GetPLCBit(Contact.X03) == true)
                {
                    setPos3 = 41562;
                    setPos4 = 265571;
                }
                else if (m == 2 && d_io.GetPLCBit(Contact.X03) == true)
                {
                    setPos3 = 89903;
                    setPos4 = 206194;
                }
                else if (m == 3 && d_io.GetPLCBit(Contact.X03) == true)
                {
                    setPos3 = 89903;
                    setPos4 = 265571;
                }
                if (seq11.BackColor == Color.White && d_io.GetPLCBit(Contact.X03) == true)
                {
                    seq.Conveyer_belt_backward();
                    seq.Cyl2_down();
                }
                if (seq12.BackColor == Color.White && d_io.GetPLCBit(Contact.X0F) == true)
                {
                    seq.Conveyer_belt_stop();
                    ServoCmd.Move_set_pose(1, 405666, setVel2, 50000, 50000);
                    ServoCmd.Move_set_pose(0, -31956, setVel1, 30000, 30000);
                }
                if (seq13.BackColor == Color.White)
                {
                    seq.Move_Cy1down();
                    seq.Gripper_Open();
                }
                if (seq14.BackColor == Color.White)
                {
                    seq.Gripper_Close();
                }
                if (seq15.BackColor == Color.White)
                {
                    seq.Move_Cy1up();
                }
                if (seq16.BackColor == Color.White && dpPos2 > 400000)
                {
                    ServoCmd.Move_set_pose(1, setPos4, setVel2, 50000, 50000);   //123456값 다시 설정하기
                    ServoCmd.Move_set_pose(0, setPos3, setVel1, 30000, 30000);      // 23456값 다시 설정하기
                }
                if (seq17.BackColor == Color.White)
                {
                    seq.Move_Cy1down();
                }
                if (seq18.BackColor == Color.White)
                {
                    seq.Gripper_Open();
                    seq.Move_Cy1up();
                    t = Convert.ToString(m);
                    button13.Text = t;
                    if (setPos3 == 41562 && setPos4 == 206194)
                    {
                        label20.Text = "W";
                        m = 1;
                    }
                    else if (setPos3 == 41562 && setPos4 == 265571)
                    {
                        label21.Text = "W";
                        m = 2;
                    }
                    else if (setPos3 == 89903 && setPos4 == 206194)
                    {
                        label23.Text = "W";
                        m = 3;
                    }
                    else if (setPos3 == 89903 && setPos4 == 265571)
                    {
                        label22.Text = "W";
                        m = 4;
                    }
                    label20.ForeColor = Color.Black;
                    label21.ForeColor = Color.Black;
                    label22.ForeColor = Color.Black;
                    label23.ForeColor = Color.Black;
                }
                if (seq19.BackColor == Color.White)
                {
                    seq.Gripper_Close();
                    if (m != white && upStatus1 == 0 && upStatus2 == 0)
                    {
                        seq.Cyl2_up();
                        seq11.BackColor = Color.Chartreuse;
                        seq12.BackColor = Color.Chartreuse;
                        seq13.BackColor = Color.Chartreuse;
                        seq14.BackColor = Color.Chartreuse;
                        seq15.BackColor = Color.Chartreuse;
                        seq16.BackColor = Color.Chartreuse;
                        seq17.BackColor = Color.Chartreuse;
                        seq18.BackColor = Color.Chartreuse;
                        seq19.BackColor = Color.Chartreuse;
                    }
                    else
                    {
                        ServoCmd.Move_set_pose(1, 0, setVel2, 50000, 50000);   //123456값 다시 설정하기
                        ServoCmd.Move_set_pose(0, 0, setVel1, 30000, 30000);      // 23456값 다시 설정하기);
                        seq11.BackColor = Color.Chartreuse;
                        seq12.BackColor = Color.Chartreuse;
                        seq13.BackColor = Color.Chartreuse;
                        seq14.BackColor = Color.Chartreuse;
                        seq15.BackColor = Color.Chartreuse;
                        seq16.BackColor = Color.Chartreuse;
                        seq17.BackColor = Color.Chartreuse;
                        seq18.BackColor = Color.Chartreuse;
                        seq19.BackColor = Color.Chartreuse;
                        label27.Text = "White공정 완료!";
                        button13.BackColor = Color.White;
                        break;
                    }
                }
            } 
            #region TextBox 메세지
            Servo1SpeedTextBox.Text = vel1;
            Servo2SpeedTextBox.Text = vel2;
            Position1TextBox.Text = pos1;
            Position2TextBox.Text = pos2;

            if (upStatus1 == 0 && upStatus3 == 1)
            {
                Home1TextBox.Text = "홈 이송 완료";
            }
            else if(upStatus1 == 1 && upStatus3 == 0)
            {
                Home1TextBox.Text = "홈 이송 중";
            }
            else
            {
                Home1TextBox.Text = "";
            }
            if (upStatus2 == 0 && upStatus4 == 1)
            {
                Home2TextBox.Text = "홈 이송 완료";
            }
            else if (upStatus2 == 1 && upStatus4 == 0)
            {
                Home1TextBox.Text = "홈 이송 중";
            }
            else
            {
                Home2TextBox.Text = "";
            }
            if (upStatus1 == 1)
            {
                Servo1TextBox.Text = "Servo Motor1 이동중";
            }
            if (upStatus1 == 0)
            {
                Servo1TextBox.Text = "Servo Motor1 정지";
            }
            if (upStatus2 == 1)
            {
                Servo2TextBox.Text = "Servo Motor2 이동중";
            }
            if (upStatus2 == 0)
            {
                Servo2TextBox.Text = "Servo Motor2 정지";
            }        //서보모터 이동상태 메세지 창   

            #endregion
            #region 공정 Flow 버튼 색상
            button1.BackColor = (d_io.GetPLCBit(Contact.X01) == true && d_io.GetPLCBit(Contact.Y20) == true) ? Color.Chartreuse : Color.Red;
            button2.BackColor = (d_io.GetPLCBit(Contact.X03) == true && d_io.GetPLCBit(Contact.Y22) == true) ? Color.Chartreuse : Color.Red;
            button3.BackColor = (d_io.GetPLCBit(Contact.Y39)) == true ? Color.Chartreuse : Color.Red;
            button4.BackColor = (d_io.GetPLCBit(Contact.X06) == true && d_io.GetPLCBit(Contact.Y26) == true) ? Color.Chartreuse : Color.Red;
            button8.BackColor = (d_io.GetPLCBit(Contact.X08) == true && d_io.GetPLCBit(Contact.Y21) == true) ? Color.Chartreuse : Color.Red;
            button7.BackColor = (d_io.GetPLCBit(Contact.X05) == true && d_io.GetPLCBit(Contact.Y25) == true) ? Color.Chartreuse : Color.Red;
            button6.BackColor = (upStatus1 == 1) ? Color.Chartreuse : Color.Red;
            button5.BackColor = (upStatus2 == 1) ? Color.Chartreuse : Color.Red;
            button12.BackColor = (d_io.GetPLCBit(Contact.X0F) == true) ? Color.Chartreuse : Color.Red;
            button10.BackColor = (upStatus1 == 0 && upStatus3 == 1) ? Color.Chartreuse : Color.Red;
            button9.BackColor = (upStatus2 == 0 && upStatus4 == 1) ? Color.Chartreuse : Color.Red;
            #endregion
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 50;
            timer1.Start();
            comboBox1.Items.Add("30퍼센트출력");
            comboBox1.Items.Add("50퍼센트출력");
            comboBox1.Items.Add("100퍼센트출력");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            d_io.UpdatePLCRead();
            d_io.UpdatePLCWrite();
            UpdateForm();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            IncreaseData();
            IncreaseCSeconds();
            _data = _data + 1;
            Time2.Add(_data);
            Time2.Count();
            UpdateForm();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Selected[0] = comboBox1.SelectedIndex;
        }
        #region 버튼 컨트롤
        private void button18_Click(object sender, EventArgs e)
        {
            seq.Gripper_Open();
        } //수정 필요 X
        private void button17_Click(object sender, EventArgs e)
        {
            seq.Move_Cy1down();
        } //수정 필요 X
        private void button16_Click(object sender, EventArgs e)
        {
            seq.Move_Cy1up();
        } //수정 필요 X
        private void button14_Click_1(object sender, EventArgs e)
        {
            seq.Gripper_Close();
        } //수정 필요 X
        private void button3_Click(object sender, EventArgs e)
        {
            a34 = false;
            try
            {
                red = double.Parse(textBox1.Text);
                white = double.Parse(textBox2.Text);
                timer2.Enabled = true;
                timer2.Start();
                if (d_io.GetPLCBit(Contact.X0E) == true)
                {
                    seq.Cyl1_up();
                }
            }
            catch
            {
                MessageBox.Show("설정값을 확인하십시오.", "Setting Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button15_Click(object sender, EventArgs e)
        {
            timer2.Start();
            timer2.Enabled = true;
        } 
        private void btnInitServoBoard_Click(object sender, EventArgs e)
        {
            ServoBoardForm servoboardform = new ServoBoardForm();
            servoboardform.ShowDialog();
            servoboardform.Dispose();
        } //수정 필요 X
        private void btnClose_Click(object sender, EventArgs e)
        {
            int error_code;
            if ((error_code = d_io.PLC_Close()) == 0)
            {
                MessageBox.Show("PLC 닫기 성공", "PLC Message", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                btnClose.Enabled = false; // 닫기 버튼 비활성화
                btnOpen.Enabled = true; // 열기 버튼 활성화
            }
            else
            {
                MessageBox.Show("PLC 닫기 실패 0x" + Convert.ToString(error_code, 16),
                "PLC Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } //수정 필요 X
        private void btnOpen_Click(object sender, EventArgs e)
        {
            int error_code;
            do
            {
                if ((error_code = d_io.PLC_Open(1, "")) == 0)
                {
                    MessageBox.Show("PLC 열기 성공", "PLC Message", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    btnClose.Enabled = true; // 닫기 버튼 활성화
                    btnOpen.Enabled = false; // 열기버튼 비활성화
                    d_io.PLC_Run(1); // PLC Stop 모드
                    break;
                }
                else
                {
                    DialogResult result = MessageBox.Show("PLC 통신 열기 실패 0x" +
                    Convert.ToString(error_code, 16) + " 다시 시도하시겠습니까?",
                    "PLC Message", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (DialogResult.Yes != result)
                    { // 다시시도 안함
                        break;
                    }
                }
            }
            while (true);
        } //수정 필요 X
        private void btnServoOff_Click(object sender, EventArgs e)
        {
            int ret;
            Color OffColor = new Color();
            OffColor = Color.FromArgb(240, 240, 240);
            if (chkAxis0.Checked == true)
            {
                ret = ServoCmd.SetServoOff(0);
                if (ret == 1) chkAxis0.BackColor = OffColor;
            }
            if (chkAxis1.Checked == true)
            {
                ret = ServoCmd.SetServoOff(1);
                if (ret == 1) chkAxis1.BackColor = OffColor;
            }
        } //수정 필요 X
        private void btnServoOn_Click(object sender, EventArgs e)
        {
            int ret;
            Color OnColor = new Color();
            OnColor = Color.FromArgb(255, 0, 0);
            if (chkAxis0.Checked == true)
            {
                ret = ServoCmd.SetServoOn(0);
                if (ret == 1) chkAxis0.BackColor = OnColor;
            }
            if (chkAxis1.Checked == true)
            {
                ret = ServoCmd.SetServoOn(1);
                if (ret == 1) chkAxis1.BackColor = OnColor;
            }
        } //수정 필요 X
        private void ResetButton_Click(object sender, EventArgs e)
        {
            seq.Cyl1_down();
            seq.Cyl2_down();
            seq.Move_Cy1up();
            seq.Gripper_Close();
            seq.Conveyer_belt_stop();
            button11.BackColor = Color.Gray;
            button13.BackColor = Color.Gray;
            ServoCmd.Move_Home1(0);
            ServoCmd.Move_Home2(1);
            comboBox1.SelectedIndex = 0;
            timer2.Stop();
            timer2.Enabled = false;
            _data = 0;
            Time2.Clear();
            k = 0;
            m = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            button11.Text = "";
            button13.Text = "";
            label18.Text = "";
            label19.Text = "";
            label20.Text = "";
            label21.Text = "";
            label22.Text = "";
            label23.Text = "";
            label24.Text = "";
            label25.Text = "";
            label26.Text = "Red 워크 작업량";
            label27.Text = "White 워크 작업량";

        }
        private void EmergencyButton_Click(object sender, EventArgs e)
        {
            seq.Gripper_Stop();
            seq.Conveyer_belt_stop();
            seq.Move_Cy1up();
            seq.Gripper_Open();
            ServoCmd.Move_Stop(0);
            ServoCmd.Move_Stop(1);
            timer2.Stop();
            timer2.Enabled = false;
        }
        private void btnJogMinus_MouseUp(object sender, MouseEventArgs e)
        {
            ServoCmd.Jog_Stop(0);
            ServoCmd.Jog_Stop(1);
        } //수정 필요 X
        private void btnJogMinus_MouseDown(object sender, MouseEventArgs e)
        {

            if (chkAxis0.Checked == true)
            {
                ServoCmd.Jog_Minus(0);
            }
            if (chkAxis1.Checked == true)
            {
                ServoCmd.Jog_Minus(1);
            }
        } //수정 필요 X
        private void btnJogPlus_MouseUp(object sender, MouseEventArgs e)
        {
            ServoCmd.Jog_Stop(0);
            ServoCmd.Jog_Stop(1);
        } //수정 필요 X
        private void btnJogPlus_MouseDown(object sender, MouseEventArgs e)
        {
            if (chkAxis0.Checked == true)
            {
                ServoCmd.Jog_Plus(0);
            }
            if (chkAxis1.Checked == true)
            {
                ServoCmd.Jog_Plus(1);
            }

        } //수정 필요 X
        private void btnServo2Home_Click(object sender, EventArgs e)
        {
            ServoCmd.Move_Home2(1);
        } //수정 필요 X
        private void btnServo1Home_Click(object sender, EventArgs e)
        {
            ServoCmd.Move_Home1(0);
        } //수정 필요 X
        #endregion
        #region 시간표시
        private void IncreaseData()
        {
            int _data = _cseconds + 1;
        }
        private void IncreaseCSeconds()
        {
            if (_cseconds == 9)
            {
                _cseconds = 0;
                IncreaseSeconds();
            }
            else
            {
                _cseconds++;
            }
        }
        private void IncreaseSeconds()
        {
            if (_seconds == 59)
            {
                _seconds = 0;
                IncreaseMinutes();
            }
            else
            {
                _seconds++;
            }
        }
        private void IncreaseMinutes()
        {
            _minutes++;
        }
        #endregion
    }
}
