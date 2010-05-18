using System;
using System.ServiceModel.Channels;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Json;

namespace AdamDotCom.Common.Service.Infrastructure.JSONP
{
    public class JSONPEncoder
    {
        public ArraySegment<byte> WriteMessage(MessageEncoder encoder, Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter sw = new StreamWriter(stream);

            string methodName = null;
            if (message.Properties.ContainsKey(JSONPBehavior.Name))
            {
                methodName = ((JSONPMessageProperty) (message.Properties[JSONPBehavior.Name])).MethodName;
            }

            if (methodName != null)
            {
                sw.Write(methodName + "( ");
                sw.Flush();
            }

            XmlWriter writer = JsonReaderWriterFactory.CreateJsonWriter(stream);
            message.WriteMessage(writer);
            writer.Flush();
            if (methodName != null)
            {
                sw.Write(" );");
                sw.Flush();
            }

            byte[] messageBytes = stream.GetBuffer();
            int messageLength = (int) stream.Position;
            int totalLength = messageLength + messageOffset;
            byte[] totalBytes = bufferManager.TakeBuffer(totalLength);
            Array.Copy(messageBytes, 0, totalBytes, messageOffset, messageLength);

            ArraySegment<byte> byteArray = new ArraySegment<byte>(totalBytes, messageOffset, messageLength);
            writer.Close();

            return byteArray;
        }
    }
}