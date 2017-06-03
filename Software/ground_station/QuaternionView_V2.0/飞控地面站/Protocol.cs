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
            byte[] pro = new byte[23] { 0xaa, 0xaf, 0x10, 0x12, data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10], data[11], data[12], data[13], data[14], data[15], data[16], data[17], 0x00};
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
            byte[] pro = new byte[23] { 0xaa, 0xaf, 0x11, 0x12, data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10], data[11], data[12], data[13], data[14], data[15], data[16], data[17], 0x00 };
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
            byte[] pro = new byte[23] { 0xaa, 0xaf, 0x12, 0x12, data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10], data[11], data[12], data[13], data[14], data[15], data[16], data[17], 0x00 };
            DataService ds = new DataService();
            pro[22] = ds.CheckSum(pro);
            return pro;
        }
        /// <summary>
        /// 加锁
        /// </summary>
        /// <returns></returns>
        public byte[] Pro_Lock()
        {
            byte[] pro = new byte[6] { 0xaa, 0xaf, 0x01, 0x01, 0xA0, 0x00 };
            DataService ds = new DataService();
            //计算校验和
            pro[5] = ds.CheckSum(pro);
            return pro;
        }
        /// <summary>
        /// 解锁
        /// </summary>
        /// <returns></returns>
        public byte[] Pro_UnLock()
        {
            byte[] pro = new byte[6] { 0xaa, 0xaf, 0x01, 0x01, 0xA1, 0x00 };
            DataService ds = new DataService();
            //计算校验和
            pro[5] = ds.CheckSum(pro);
            return pro;
        }
    }
}
