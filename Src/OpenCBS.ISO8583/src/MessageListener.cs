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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Free.iso8583.config;

namespace Free.iso8583
{
    public class ListeningEventArgs : EventArgs
    {
        private IPAddress _ipAddress;
        private int _port;
        private DateTime _startTime;

        public ListeningEventArgs(IPAddress ipAddress, int port, DateTime startTime)
        {
            _ipAddress = ipAddress;
            _port = port;
            _startTime = startTime;
        }

        public IPAddress IPAddress { get { return _ipAddress; } }
        public int Port { get { return _port; } }
        public DateTime StartTime { get { return _startTime; } }
    }
    
    internal class MessageListenerSync
    {
        internal int NumOfConn { get; set;}
        internal bool IsAcceptingConn { get; set; }
    }

    public class MessageListener
    {
        public const int PORT = 3107;
        public const int MAX_CONNECTIONS = 50;

        internal MessageListenerSync Sync = new MessageListenerSync { NumOfConn = 0, IsAcceptingConn = false };
        internal ManualResetEvent TcpClientConnectedEvent = new ManualResetEvent(false);

        private static volatile bool _isLoaded = false;
        private TcpListener _server = null;
        private volatile bool _stopped = false;
        private int _port = 0;
        private int _maxConn = -1;
        
        public MessageListener()
        {
            SslCertificate = null;
            SslProtocol = SslProtocols.Default;
            IsClientCertificateRequired = false;
            IsCertificateRevocationChecked = true;
            CertificateValidationCallback = null;
            ReadTimeout = Timeout.Infinite;
            WriteTimeout = Timeout.Infinite;
        }

        public MessageListener(String sslCertificateFile) : this(sslCertificateFile, null)
        {
        }

        public MessageListener(String sslCertificateFile, SecureString certificatePassword) : this()
        {
            try
            {
                if (sslCertificateFile == null) return;
                byte[] certData = File.ReadAllBytes(sslCertificateFile);
                SslCertificate = new X509Certificate2();
                if (certificatePassword == null)
                    SslCertificate.Import(certData, "", X509KeyStorageFlags.MachineKeySet);
                else
                    SslCertificate.Import(certData, certificatePassword, X509KeyStorageFlags.MachineKeySet);
            }
            catch (Exception ex)
            {
                throw new MessageListenerException("Failed to read certificate file. " + ex.Message, ex);
            }
        }

        public event EventHandler<ListeningEventArgs> StartListeningEvent;

        public String IPAddress { get; set; }
        public int Port { get { return _port <= 0 ? PORT : _port; } set { _port = value; } }
        public int MaxConnections { get { return _maxConn < 0 ? MAX_CONNECTIONS : _maxConn; } set { _maxConn = value; } }
        
        public X509Certificate SslCertificate { get; set; }
        public SslProtocols SslProtocol { get; set; }
        public bool IsClientCertificateRequired { get; set; }
        public bool IsCertificateRevocationChecked { get; set; }
        public RemoteCertificateValidationCallback CertificateValidationCallback { get; set; }
        
        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }

        private static IConfigParser _configParser = null;
        public static String ConfigPath { get; private set; }
        public static void SetConfigPath(String path, String baseDir)
        {
            bool isAbsolute = false;
            try
            {
                isAbsolute = Path.IsPathRooted(path);
            }
            catch (ArgumentException ex)
            {
                throw new MessageListenerException("Parameter 'path' is invalid.", ex);
            }
            if (isAbsolute //To add volume/drive to the rooted path if necessary.
                || String.IsNullOrEmpty(baseDir)) //If baseDir is null, path relative to the current working dir
            {
                MessageListenerException ex = null;
                path = GetAbsolutePath(path, "path", out ex);
                if (ex != null) throw ex;
                ConfigPath = path;
                return;
            }

            isAbsolute = false;
            try
            {
                isAbsolute = Path.IsPathRooted(baseDir);
            }
            catch (ArgumentException ex)
            {
                throw new MessageListenerException("Parameter 'baseDir' is invalid.", ex);
            }
            if (!isAbsolute)
            {
                throw new MessageListenerException("Parameter 'baseDir' must be absolute.");
            }
            MessageListenerException exc = null;
            baseDir = GetAbsolutePath(baseDir, "baseDir", out exc); //To add volume/drive to the path if necessary
            if (exc != null) throw exc;
            baseDir = baseDir.Replace('\\','/');
            if (!baseDir.EndsWith("/")) baseDir += "/";
            Uri baseUri = new Uri(baseDir.StartsWith("/") ? "file:" + baseDir : "file:///" + baseDir);
            path = path.Replace('\\', '/');
            Uri uri;
            try
            {
                uri = new Uri(baseUri, path);
            }
            catch (Exception ex)
            {
                throw new MessageListenerException("Combination 'baseDir' and 'path' yields invalid path.", ex);
            }
            ConfigPath = uri.AbsolutePath;
        }
        public static void SetConfigPath(String path)
        {
            SetConfigPath(path, null);
        }
        private static String GetAbsolutePath(String path, String paramName,
            out MessageListenerException exception)
        {
            exception = null;
            try
            {
                path = Path.GetFullPath(path);
            }
            catch (ArgumentException ex)
            {
                exception = new MessageListenerException("Parameter '" + paramName + "' is invalid.", ex);
            }
            catch (System.Security.SecurityException ex)
            {
                exception = new MessageListenerException("No permission to get absolute path.", ex);
            }
            catch (NotSupportedException ex)
            {
                exception = new MessageListenerException("Path format in parameter '" + paramName
                    + "' is not supported.", ex);
            }
            catch (PathTooLongException ex)
            {
                exception = new MessageListenerException("Path in parameter '" + paramName
                    + "' is too long.", ex);
            }
            return path;
        }

        private static void CreateXmlConfigParser()
        {
            using (Stream fileConfig = MessageProcessor.ReadConfigFile(ConfigPath))
            {
                _configParser = new XmlConfigParser(fileConfig);
            }
        }
        public static void SetConfig(String fileConfigPath, String baseDir)
        {
            SetConfigPath(fileConfigPath, baseDir);
            CreateXmlConfigParser();
        }
        public static void SetConfig(String fileConfigPath)
        {
            SetConfig(fileConfigPath, null);
        }
        public static void SetConfig(Type messageToModelMapping)
        {
            _configParser = new AttributeConfigParser(messageToModelMapping);
        }
        
        private void DoBeginAcceptTcpClient()
        {
            MessageListenerWorker mlw = null;
            try
            {
                while (true)
                {
                    lock (Sync)
                    {
                        TcpClientConnectedEvent.Reset();
                        Sync.IsAcceptingConn = (Sync.NumOfConn < MaxConnections || MaxConnections <= 0);
                    }

                    if (Sync.IsAcceptingConn)
                    {
                        TcpClient client = _server.AcceptTcpClient();
                        if (client.Client.RemoteEndPoint is IPEndPoint)
                        {
                            IPEndPoint remoteAddr = (IPEndPoint)client.Client.RemoteEndPoint;
                            IPEndPoint localAddr = (IPEndPoint)client.Client.LocalEndPoint;
                            Logger.GetInstance().WriteLine("Accepting request from " + remoteAddr.Address + ":" + remoteAddr.Port
                                + " to " + localAddr.Address + ":" + localAddr.Port,
                                LogLevel.Notice);
                        }
                        lock (Sync)
                        {
                            Sync.NumOfConn++;
                        }
                        new Thread(this.DoAcceptTcpClientCallback).Start(client);
                    }
                    else
                    {
                        TcpClientConnectedEvent.WaitOne();
                    }
                    
                    if (_stopped) return;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance().Write(e);
                if (mlw != null) mlw.Close();
            }
            finally
            {
                Stop();
            }
        }

        private void DoAcceptTcpClientCallback(Object clientObject)
        {
            if (_stopped) return;
            MessageListenerWorker mlw = null;
            try
            {
                TcpClient client = (TcpClient)clientObject;
                mlw = new MessageListenerWorker(client, this);
                mlw.AcceptMessage();
            }
            catch (Exception e)
            {
                Logger.GetInstance().Write(e);
                if (mlw != null) mlw.Close();
            }
        }

        public void Start()
        {
            lock (MessageProcessor.GetInstance())
            {
                if (!_isLoaded)
                {
                    Logger.GetInstance().WriteLine("Loading configuration: " +
                        (String.IsNullOrEmpty(ConfigPath) ? MessageProcessor.defaultConfigPath : ConfigPath));
                    try
                    {
                        if (_configParser != null)
                        {
                            MessageProcessor.GetInstance().Load(_configParser);
                        }
                        else if (!String.IsNullOrEmpty(ConfigPath))
                        {
                            CreateXmlConfigParser();
                            MessageProcessor.GetInstance().Load(_configParser);
                        }
                        else
                        {
                            MessageProcessor.GetInstance().Load();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.GetInstance().Write(ex, "Can't load configuration, application can't continue. ");
                        return;
                    }
                }
                _isLoaded = true;
            }
            
            try
            {
                System.Net.IPAddress ip = System.Net.IPAddress.Any;
                if (!String.IsNullOrEmpty(IPAddress)) ip = Dns.GetHostAddresses(IPAddress)[0];
                _server = new TcpListener(ip, Port);
                _server.Start((int)SocketOptionName.MaxConnections);
                Logger.GetInstance().WriteLine("Starts listening port " + Port
                    + (String.IsNullOrEmpty(IPAddress) ? "" : " on address " + IPAddress)
                    + " ...");
                EventHandler<ListeningEventArgs> handler = StartListeningEvent;
                if (handler != null)
                {
                    handler(this, new ListeningEventArgs(ip, Port, DateTime.Now));
                }

                _stopped = false;
                DoBeginAcceptTcpClient();
            }
            catch (Exception e)
            {
                Logger.GetInstance().Write(e);
                Stop();
                throw e;
            }
        }

        public void Stop()
        {
            if (_stopped) return;
            RequestStop();
            MessageListenerWorker.CloseAll();
            if (_server != null) _server.Stop();
        }

        private void RequestStop()
        {
            _stopped = true;
            TcpClientConnectedEvent.Set();
        }
    }

    internal class MessageListenerWorker : IMessageStream
    {
        private static IDictionary<TcpClient, MessageListenerWorker> _tcpClients = new Dictionary<TcpClient, MessageListenerWorker>();
        private TcpClient _client = null;
        private Stream _stream = null;
        private MessageListener _messageListener;

        private MessageListenerWorker()
        {
        }

        public MessageListenerWorker(TcpClient client, MessageListener messageListener)
        {
            _client = client;
            _messageListener = messageListener;
            RegisterMe();
        }

        private void RegisterMe()
        {
            lock (((ICollection)_tcpClients).SyncRoot)
            {
                _tcpClients[_client] = this;
            }
        }

        private void UnregisterMe()
        {
            lock (((ICollection)_tcpClients).SyncRoot)
            {
                _tcpClients.Remove(_client);
            }
        }

        public void AcceptMessage()
        {
            if (_messageListener.SslCertificate == null)
            {
                _stream = _client.GetStream();
            }
            else //Use SSL for communication
            {
                RemoteCertificateValidationCallback validationCallback = _messageListener.CertificateValidationCallback;
                if (!_messageListener.IsClientCertificateRequired)
                    validationCallback = null; //Ensure the callback won't be called
                SslStream sslStream = new SslStream(_client.GetStream(), false, validationCallback);
                sslStream.AuthenticateAsServer(_messageListener.SslCertificate, _messageListener.IsClientCertificateRequired,
                    _messageListener.SslProtocol, _messageListener.IsCertificateRevocationChecked);
                _stream = sslStream;
            }
            _stream.ReadTimeout = _messageListener.ReadTimeout;
            _stream.WriteTimeout = _messageListener.WriteTimeout;
            
            /**** This commented is the buggy code if the client doesn't close the socket.
             **** We need the length of message actually to determine the end of message
                byte[] bytes = new byte[1024];
                byte[] b;
                List<byte> data = new List<byte>();
                int bytesCount;
                while ((bytesCount = _stream.Read(bytes, 0, bytes.Length)) > 0)
                {
                    if (bytesCount != bytes.Length)
                    {
                        b = new byte[bytesCount];
                        Array.Copy(bytes, 0, b, 0, bytesCount);
                    }
                    else
                    {
                        b = bytes;
                    }
                    data.AddRange(b);
                    //if (!_stream.DataAvailable) break;
                }
            
                bytes = data.ToArray();*/
            byte[] bytes = null; //Let MessageProcessor get the message bytes.
            
            MessageProcessor.GetInstance().Receive(bytes, this);
        }
        
        #region IMessageSender Members
        public byte[] Send(byte[] message)
        {
            if (_stream == null) throw new MessageListenerException("Socket has been closed.");
            
            //Logger.GetInstance().WriteLine("Sending response message " + message.Length + " bytes");
            _stream.Write(message, 0, message.Length);
            return null;
        }

        public int Receive(byte[] buffer, int offset, int count)
        {
            if (_stream == null) throw new MessageListenerException("Socket has been closed.");
            return _stream.Read(buffer, offset, count);
        }

        public void Close()
        {
            if (_stream != null) _stream.Close();
            if (_client != null)
            {
                UnregisterMe();
                _client.Close();
            }
            _stream = null;
            _client = null;

            lock (_messageListener.Sync)
            {
                _messageListener.Sync.NumOfConn--;
                if (!_messageListener.Sync.IsAcceptingConn) _messageListener.TcpClientConnectedEvent.Set();
            }
        }
        #endregion

        public static void CloseAll()
        {
            lock (((ICollection)_tcpClients).SyncRoot)
            {
                foreach (MessageListenerWorker mlw in _tcpClients.Values)
                {
                    mlw.Close();
                }
                _tcpClients.Clear();
            }
        }
    }
}
