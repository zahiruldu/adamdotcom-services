using System;
using System.ServiceModel.Channels;
using System.IO;
using System.ServiceModel.Web;
using AdamDotCom.Common.Service.Infrastructure.CSV;
using AdamDotCom.Common.Service.Infrastructure.JSONP;

namespace AdamDotCom.Common.Service.Infrastructure
{
    public class CustomEncoderFactory : MessageEncoderFactory
    {
        CustomEncoder encoder;
        public CustomEncoderFactory()
        {
            encoder = new CustomEncoder();
        }

        public override MessageEncoder Encoder
        {
            get
            {
                return encoder;
            }
        }

        public override MessageVersion MessageVersion
        {
            get { return encoder.MessageVersion; }
        }

        class CustomEncoder : MessageEncoder
        {
            private MessageEncoder encoder;

            private JSONPEncoder _jsonpEncoder;
            private JSONPEncoder JsonpEncoder
            {
                get
                {
                    if (_jsonpEncoder == null)
                    {
                        _jsonpEncoder = new JSONPEncoder();
                    }
                    return _jsonpEncoder;
                }
            }

            private CSVEncoder _csvEncoder;
            private CSVEncoder CsvEncoder
            {
                get
                {
                    if (_csvEncoder == null)
                    {
                        _csvEncoder = new CSVEncoder();
                    }
                    return _csvEncoder;
                }
            }

            public CustomEncoder()
            {
                WebMessageEncodingBindingElement element = new WebMessageEncodingBindingElement();
                encoder = element.CreateMessageEncoderFactory().Encoder;
            }

            public override string ContentType
            {
                get
                {
                    //Note: this is some type of hack, I'm initially setting the ContentType for CSV in the CSVBehavior class
                    WebOperationContext.Current.OutgoingResponse.ContentType = encoder.ContentType;
                    return encoder.ContentType;
                }
            }

            public override string MediaType
            {
                get
                {
                    return encoder.MediaType;
                }
            }

            public override MessageVersion MessageVersion
            {
                get
                {
                    return encoder.MessageVersion;
                }
            }

            public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
            {
                return encoder.ReadMessage(buffer, bufferManager, contentType);
            }

            public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
            {
                return encoder.ReadMessage(stream, maxSizeOfHeaders, contentType);
            }

            public override void WriteMessage(Message message, Stream stream)
            {
            }

            public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
            {
                if (message.IsJSONPBehavior())
                {
                    return JsonpEncoder.WriteMessage(encoder, message, maxMessageSize, bufferManager, messageOffset);
                }
                if (message.IsCSVBehavior())
                {
                    return CsvEncoder.WriteMessage(encoder, message, maxMessageSize, bufferManager, messageOffset);
                }
                
                return encoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);
            }

            public override bool IsContentTypeSupported(string contentType)
            {
                return encoder.IsContentTypeSupported(contentType);
            }
        }
    }
}