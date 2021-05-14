using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Geek.Server
{
    //�����������ݿ���
    [BsonIgnoreExtraElements(true, Inherited = true)]//���Դ���ɾ�����ֶ�[���ݿ������ֶ�]
    public abstract class BaseMessage : BaseState, IMessage
    {
        public virtual int Read(byte[] buffer, int offset)
        {
            return offset;
        }

        public virtual int Write(byte[] buffer, int offset)
        {
            return offset;
        }

        public virtual int GetMsgId()
        {
            return 0;
        }

        public byte[] Serialize()
        {
            return writeAsBuffer(512);
        }

        byte[] writeAsBuffer(int size)
        {
            var data = new byte[size];
            var offset = 0;
            offset = this.Write(data, offset);
            if(offset <= data.Length)
            {
                if(offset < data.Length)
                {
                    var ret = new byte[offset];
                    Array.Copy(data, 0, ret, 0, offset);
                    data = ret;
                }
                return data;
            }
            else
            {
                return writeAsBuffer(offset);
            }
        }

        public void Deserialize(byte[] data)
        {
            this.Read(data, 0);
        }
    }
}