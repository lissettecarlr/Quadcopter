/************************************************************************************
* Copyright (c) 2017 All Rights Reserved.
*命名空间：飞控地面站
*文件名： DataService
*创建人： 刘勇(默认)
*创建时间：2017/5/16 16:11:21
*描述
*=====================================================================
*修改标记
*修改时间：2017/5/16 16:11:21
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
    public class DataService
    {
        /// <summary>
        /// 数据和校验
        /// </summary>
        /// <param name="buffer">待检测的数据</param>
        /// <returns></returns>
        public byte CheckSum(byte[] buffer)
        {
            int num = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                num = (num + buffer[i]);
              //  Console.WriteLine("和校验输出 {0}：{1}",i,num);
            }
            buffer = BitConverter.GetBytes(num);
            return buffer[0]; 
        }
        /// <summary>
        /// 从飞控到上位机
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool Check_FC_TO_HC_Head(byte[] buffer)
        {
            bool result = false;
            if (buffer[0] == 0xaa && buffer[1] == 0xaa)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 上位机到飞控
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool Check_HC_TO_FC_Head(byte[] buffer)
        {
            bool result = false;
            if (buffer[0] == 0xaa && buffer[1] == 0xaf)
            {
                result = true;
            }
            return result;
        }
    }
}
