using System;
using System.Collections.Generic;
using System.Linq;

namespace U3DTools
{
    public class MsgBuffer: IDisposable
    {
        private List<byte> m_data;
        private int m_msgLength = 0;

        public MsgBuffer(){
            m_data = new List<byte>();
        }

        public void AddData(IEnumerable<byte> bytes)
        {
            if(bytes==null)
                throw new ArgumentNullException(nameof(bytes));

            m_data.AddRange(bytes);

            ParsingMsg();
        }

        private void ParsingMsg()
        {
            while (m_data.Count>4&&(HasLength==false||HasLength==true && m_msgLength<=m_data.Count))
            {
                if(!HasLength)
                {
                    ParsingLength();
                }
                if(HasLength && m_data.Count>=m_msgLength)
                {
                    ParsingBody();
                }
            }
        }

        private bool HasLength=>m_msgLength>0;

        private void ParsingLength()
        {
            if(m_data.Count<4)
                return;
            
            m_msgLength = BitConverter.ToInt32(m_data.Take(4).ToArray(),0);
        }

        private void ParsingBody()
        {
            if(m_data.Count<m_msgLength)
                return;
            
            var msgdata = m_data.Skip(4).Take(m_msgLength).ToArray();
            //解密
            if(decode!=null){
                msgdata = decode(msgdata);
            }
            System.Console.WriteLine("获得一条完整的消息");
            m_data.RemoveRange(0,m_msgLength);
            m_msgLength = -1;
            OnCompletionMsg?.Invoke(msgdata);
        }

        public Func<byte[],byte[]> decode;
        public Action<byte[]> OnCompletionMsg;


        public void Dispose()
        {
            m_data.Clear();
            m_msgLength = -1;
        }
    }


}