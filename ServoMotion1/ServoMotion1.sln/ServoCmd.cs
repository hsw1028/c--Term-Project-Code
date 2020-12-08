using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // MessageBox 사용
using AjinMotion; // Motion Library

namespace ServoMotion1
{
    class ServoCmd
    {
        const uint DPRAM_COMMON_CMD_SIIIH_SCAN = 0x74;
        public static int BoardOpen()
        {
            int ret = -1;
            if (IsBdOpen() == 0)
            {
                uint err = CAXL.AxlOpen(7);
                if (err == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    ret = 0;
                    SSCNET_DeviceRescan();
                }
                else ret = (int)err;
            }
            else ret = 1;
            return ret;
        }
        public static int BoardClose()
        {
            int ret = CAXL.AxlClose();
            return ret;
        }
        public static int IsBdOpen()
        {
            return CAXL.AxlIsOpened();
        }
        private static uint SSCNET_DeviceRescan()
        {
            int nBoardCnt = 0;
            double dCmdPos = 0;
            int nOldTick = 0, nNewTick = 0, nTickTime = 0;
            uint[] uSendData = new uint[22];
            uint uReturn;
            int m_lAxisNo = 0;
            CAXL.AxlOpenNoReset(7);
            CAXL.AxlGetBoardCount(ref nBoardCnt);
            if (nBoardCnt < 1)
                return (uint)AXT_FUNC_RESULT.AXT_RT_OPEN_ERROR;
            System.Array.Clear(uSendData, 0, 22);
            //'AxlSetSendBoardCommand' 함수의 첫 번째 인자는 보드 넘버입니다.
            // PC에 장착한 순서에 따라 보드 넘버는 변경됩니다.
            // EzConfig UC을 통해 보드 넘버를 알 수 있습니다.
            uReturn = CAXDev.AxlSetSendBoardCommand(0, DPRAM_COMMON_CMD_SIIIH_SCAN,
            ref uSendData[21], 0);
            // 미쓰비시 드라이버 연결을 위한 보드에 명령 전달 : AxlSetSendBoardCommand
            nOldTick = Environment.TickCount;
            // 슬레이브가 몇개 인지 학인
            while (true)
            {
                //TimeOut Check
                nNewTick = Environment.TickCount;
                nTickTime = nNewTick - nOldTick;
                if (nTickTime > 20000)
                {
                    System.Windows.Forms.MessageBox.Show("Scan Error");
                    return (uint)AXT_FUNC_RESULT.AXT_RT_OPEN_ERROR;
                }
                //Network Connect Check
                uReturn = CAXM.AxmStatusGetCmdPos(m_lAxisNo, ref dCmdPos);
                if (uReturn == 0)
                {
                    break;
                }
            }
            CAXL.AxlClose();
            CAXL.AxlOpenNoReset(7);
            return (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS;
        }
        public static void GetBoardInfo(ref int BoardCount, ref int AxisCount)
        {
            CAXL.AxlGetBoardCount(ref BoardCount); // 보드 갯수 확인
            CAXM.AxmInfoGetAxisCount(ref AxisCount); // 모든 축의 개수 확인
        }
        public static int GetBoardStatus(int BoardNo)
        {
            if (CAXL.AxlGetBoardStatus(BoardNo) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                return 1;
            else
                return -1;
        }
        public static int LoadParameters(string filename)
        {
            if (CAXM.AxmMotLoadParaAll(filename) == 0x00) return 1;
            else return -1;
        }
        public static int SetServoOn(int axis)
        {
            if (CAXM.AxmSignalServoOn(axis, (uint)0x01) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                return 1;
            else return -1;
        }
        public static int SetServoOff(int axis)
        {
            if (CAXM.AxmSignalServoOn(axis, (uint)0x00) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                return 1;
            else return -1;
        }
        public static void Jog_Plus(int axis)
        {
            CAXM.AxmMoveVel(axis, 30000, 400000, 400000);
        }
        public static void Jog_Minus(int axis)
        {
            CAXM.AxmMoveVel(axis, -30000, 400000, 400000);
        }
        public static void Jog_Stop(int axis)
        {
            CAXM.AxmMoveStop(axis, 40000);
        }
        public static void Move_Home1(int axis)
        {
            CAXM.AxmHomeSetVel(0, 30000, 10000, 10000, 5000, 40000, 10000);
            CAXM.AxmHomeSetStart(0);
        }
        public static void Move_Home2(int axis)
        {
            CAXM.AxmHomeSetVel(1, 30000, 5000, 2000, 1000, 40000, 10000);
            CAXM.AxmHomeSetStart(1);
        }
        public static void Move_Stop(int axis)
        {
            CAXM.AxmMoveEStop(axis);
            CAXM.AxmMoveSStop(axis);

        }
        public static void Move_set_pose(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel)
        {
            CAXM.AxmMoveStartPos(nAxisNo, dPos, dVel, dAccel, dDecel);
         }
        public static void Read_Pos1(int lAxisNo, ref double dpPos)
        {
            CAXM.AxmStatusGetActPos(0, ref dpPos);
        }
        public static void Read_Pos2(int lAxisNo, ref double dpPos)
        {
            CAXM.AxmStatusGetActPos(1, ref dpPos);
        }
        public static void Read_Vel1(int lAxisNo, ref double dpVelocity) 
        {
            CAXM.AxmStatusReadVel(0, ref dpVelocity);
            return;
        }
        public static void Read_Vel2(int lAxisNo, ref double dpVelocity)
        {
            CAXM.AxmStatusReadVel(1, ref dpVelocity);
            return;
        }
        public static void Read_Status1(int lAxisNo, ref uint upStatus)
        {
            CAXM.AxmStatusReadInMotion(0, ref upStatus);
        }
        public static void Read_Status2(int lAxisNo, ref uint upStatus)
        {
            CAXM.AxmStatusReadInMotion(1, ref upStatus);
        }
        public static void Read_HomeStatus1(int lAxisNo, ref uint upStatus)
        {
            CAXM.AxmHomeReadSignal(0, ref upStatus);
        }
        public static void Read_HomeStatus2(int lAxisNo, ref uint upStatus)
        {
            CAXM.AxmHomeReadSignal(1, ref upStatus);
        }
        public static void AlarmFunc1(int nAxisNo, ref uint upStatus)
        {
            CAXM.AxmSignalReadServoAlarm(nAxisNo, ref upStatus);
        }
    }
}
