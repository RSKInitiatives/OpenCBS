/**
 *  Distributed as part of Free.iso8583
 *  
 *  Free.iso8583 is ISO 8583 Message Processor library that makes message parsing/compiling esier.
 *  It will convert ISO 8583 message to a model object and vice versa. So, the other parts of
 *  application will only do the rest effort to make business process done.
 *  
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 2.1 of the License or (at your option) any later version. 
 *  See http://gnu.org/licenses/lgpl.html
 *
 *  Developed by AT Mulyana (atmulyana@yahoo.com) 2009-2015
 *  The latest update can be found at sourceforge.net
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;
using System.Net.Security;

namespace Free.iso8583
{
    public class MessageClient : IMessageStream, IModelParsingReceiver
    {
        private TcpClient _client = null;
        private Stream _stream = null;

        private MessageClient()
        {
        }
        
        public MessageClient(String server, int port, Object model, MessageCallback callback)
        {
            Server = server;
            Port = port;
            Model = model;
            Callback = callback;

            IsSslEnabled = false;
            ClientCertificates = null;
            SslProtocol = SslProtocols.Default;
            IsCertificateRevocationChecked = true;
            CertificateValidationCallback = null;
            CertificateSelectionCallback = null;
            ReadTimeout = Timeout.Infinite;
            WriteTimeout = Timeout.Infinite;
        }

        public MessageClient(String server, int port, Object model) : this(server, port, model, null)
        {
        }

        public String Server { get; set; }
        public int Port { get; set; }
        public Object Model { get; set; }
        public MessageCallback Callback { get; set; }

        public bool IsSslEnabled { get; set; }
        public X509CertificateCollection ClientCertificates { get; set; }
        public SslProtocols SslProtocol { get; set; }
        public bool IsCertificateRevocationChecked { get; set; }
        public RemoteCertificateValidationCallback CertificateValidationCallback { get; set; }
        public LocalCertificateSelectionCallback CertificateSelectionCallback { get; set; }
        
        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }

        public IProcessedMessage SendModel()
        {
            return MessageProcessor.GetInstance().Send(Model, this, Callback);
        }

        #region IModelParsingReceiver Members
        public IParsedMessage ParsedMessage { get; set; }
        #endregion
        
        private ManualResetEvent _SendBytesEvent = new ManualResetEvent(false);
        private Object SendBytesCallback(Object model)
        {
            try
            {
                if (Callback != null)
                {
                    if (Callback.Target is IModelParsingReceiver)
                        ((IModelParsingReceiver)Callback.Target).ParsedMessage = ParsedMessage;
                    Callback(model);
                }
            }
            catch (Exception ex)
            { 
                Logger.GetInstance().Write(ex, model==null ? "Error in Callback (possibly, cannot process null parameter)." : "");
            }
            finally
            {
                _SendBytesEvent.Set(); //Ends SendBytes process
            }
            return null; //avoids sending back to server when calling MessageProcessor.GetInstance().Receive
        }
        public IProcessedMessage SendBytes(byte[] message)
        {
            _SendBytesEvent.Reset();
            byte[] response;
            try
            {
                response = Send(message);
            }
            catch (Exception ex)
            {
                Close();
                Logger.GetInstance().Write(ex, "An error happens when connecting to the server"
                    + (IsSslEnabled ? " or An error in SSL connection." : "."));

                if (Callback != null)
                {
                    try { Callback(null); } //Notifies the caller that there is an error but prepare for a new error
                    catch (Exception ex2)
                    {
                        Logger.GetInstance().Write(ex2, "Error in Callback (possibly, cannot process null parameter).");
                    }
                }
                
                return null;
            }
            if (response == null) return null;
            IProcessedMessage msg = MessageProcessor.GetInstance().Receive(response, this, this.SendBytesCallback);
            _SendBytesEvent.WaitOne();
            return msg;
        }
        public void SendBytes(object message) //For asynchronous process using: new Thread(messageClient.SendBytes)
        {
            SendBytes((byte[])message);
        }

        private static bool RemoteCertificateValidationCallback(Object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private void CheckSocket()
        {
            if (_client == null)
            {
                _client = new TcpClient(Server, Port);
            }
            
            if (_stream == null)
            {
                if (IsSslEnabled)
                {
                    SslStream sslStream = new SslStream(_client.GetStream(), false,
                        CertificateValidationCallback == null ? MessageClient.RemoteCertificateValidationCallback
                            : CertificateValidationCallback,
                        CertificateSelectionCallback);
                    sslStream.AuthenticateAsClient(Server, ClientCertificates, SslProtocol, IsCertificateRevocationChecked);
                    _stream = sslStream;
                }
                else
                {
                    _stream = _client.GetStream();
                }
                _stream.ReadTimeout = this.ReadTimeout;
                _stream.WriteTimeout = this.WriteTimeout;
            }
        }

        #region IMessageStream Members
        public byte[] Send(byte[] message)
        {
            CheckSocket();
            _stream.Write(message, 0, message.Length);

            /**** This commented is the buggy code if the server doesn't close the socket.
             **** We need the length of message actually to determine the end of message
            IList<byte> replyBytes = new List<byte>();
            byte[] buffer = new byte[2048];
            int numberOfBytesRead = 0;
            do
            {
                numberOfBytesRead = _stream.Read(buffer, 0, buffer.Length);
                for (int i = 0; i < numberOfBytesRead; i++) replyBytes.Add(buffer[i]);
            }
            while (numberOfBytesRead > 0);
            return replyBytes.Count > 0 ? replyBytes.ToArray() : null;*/

            return new byte[0]; //let MessageProcessor get the reply
        }

        public int Receive(byte[] buffer, int offset, int count)
        {
            CheckSocket();
            return _stream.Read(buffer, offset, count);
        }

        public void Close()
        {
            if (_stream != null) _stream.Close();
            if (_client != null) _client.Close();
            _stream = null;
            _client = null;
        }
        #endregion
    }
}
