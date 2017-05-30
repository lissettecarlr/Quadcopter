/************************************************************************************
* Copyright (c) 2017 All Rights Reserved.
*命名空间：FlightControl.Model
*文件名： Protocol
*创建人： 刘勇(默认)
*创建时间：2017/5/18 15:20:49
*描述
*=====================================================================
*修改标记
*修改时间：2017/5/18 15:20:49
*修改人：XXX
*描述：
*=====================================================================
*┌──────────────────────────────────┐
*│　版权所有：仅限本人或朋友使用。│
*│　任何人不得重新反编译或用于任何商业程序或网站。│
*└──────────────────────────────────┘
************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞控地面站
{
    public class Protocol
    {
        /// <summary>
        /// ACC校准
        /// </summary>
        /// <returns></returns>
        public byte[] Pro_ACC()
        {
            byte[] pro = new byte[6] { 0xaa,0xaf,0x01,0x01,0x01,0x00};
            DataService ds = new DataService();
            //计算校验和
            pro[5] = ds.CheckSum(pro);
            return pro;
        }

        /// <summary>
        /// GYRD校准
        /// </summary>
        /// <returns></returns>
        public byte[] Pro_GYRD()
        {
            byte[] pro = new byte[6] { 0xaa, 0xaf, 0x01, 0x01, 0x02, 0x00 };
            DataService ds = new DataService();
            pro[5] = ds.CheckSum(pro);
            return pro;
        }

        /// <summary>
        /// MAG校准
        /// </summary>
        /// <returns></returns>
        public byte[] Pro_MAG()
        {
            byte[] pro = new byte[6] { 0xaa, 0xaf, 0x01, 0x01, 0x04, 0x00 };
            DataService ds = new DataService();
            pro[5] = ds.CheckSum(pro);
            return pro;
        }

        /// <summary>
        /// 请求PID
        /// </summary>
        /// <returns></returns>
        public byte[] Pro_GetPID()
        {
            byte[] pro = new byte[6] { 0xaa, 0xaf, 0x02, 0x01, 0x01, 0x00 };
            DataService ds = new DataService();
            pro[5] = ds.CheckSum(pro);
            return pro;
        }

        /// <summary>
        /// 发送遥控器数据
        /// </summary>
        /// <param name="data">THR--YAW--ROL--PIT</param>
        /// <returns></returns>
        public byte[] Pro_SendRemoteData(byte[] data)
        {
            byte[] pro = new byte[25] { 0xaa, 0xaf, 0x03, 0x14, data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7],0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            DataService ds = new DataService();
            pro[24] = ds.CheckSum(pro);
            return pro;
        }


        /// <summary>
        /// 发送PID_1
        /// </summary>
        /// <param name="data">PID数据帧</param>
        /// <returns></returns>
        public byte[] Pro_SendPID_1(byte[] data)
        {
            byte[] pro = new byte[23] { 0xaa, 0xaf, 0x0A, 0x12, data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10], data[11], data[12], data[13], data[14], data[15], data[16], data[17], 0x00};
            DataService ds = new DataService();
            pro[22] = ds.CheckSum(pro);
            return pro;
        }

        /// <summary>
        /// 发送PID_2
        /// </summary>
        /// <param name="data">PID数据帧</param>
        /// <returns></returns>
        public byte[] Pro_SendPID_2(byte[] data)
        {
            byte[] pro = new byte[23] { 0xaa, 0xaf, 0x0B, 0x12, data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10], data[11], data[12], data[13], data[14], data[15], data[16], data[17], 0x00 };
            DataService ds = new DataService();
            pro[22] = ds.CheckSum(pro);
            return pro;
        }

        /// <summary>
        /// 发送PID_3
        /// </summary>
        /// <param name="data">PID数据帧</param>
        /// <returns></returns>
        public byte[] Pro_SendPID_3(byte[] data)
        {
            byte[] pro = new byte[23] { 0xaa, 0xaf, 0x0C, 0x12, data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10], data[11], data[12], data[13], data[14], data[15], data[16], data[17], 0x00 };
            DataService ds = new DataService();
            pro[22] = ds.CheckSum(pro);
            return pro;
        }
    }
}
