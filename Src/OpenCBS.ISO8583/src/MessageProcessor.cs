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
using System.Threading;
using Free.iso8583.config;


namespace Free.iso8583
{
    public class MessageProcessor
    {
        private static MessageProcessor instance = new MessageProcessor();
        internal static String defaultConfigPath = Util.GetAssemblyDir(instance) + "/messagemap-config.xml";
        
        private MessageProcessor()
        {
        }

        public static MessageProcessor GetInstance()
        {
            return instance;
        }

        internal void Load(IConfigParser configParser)
        {
            try
            {
                config.MessageConfigs.Clear();
                configParser.Parse();
            }
            catch (Exception e)
            {
                Logger.GetInstance().Write(e);
                throw e;
            }
        }

        public void Load(Type messageToModelMapping)
        {
            Load(new AttributeConfigParser(messageToModelMapping));
        }

        public void Load(Stream fileConfig)
        {
            XmlConfigParser xmlConfigParser = null;
            try
            {
                xmlConfigParser = new XmlConfigParser(fileConfig);
            }
            catch (Exception e)
            {
                Logger.GetInstance().Write(e);
                throw e;
            }
            
            Load(xmlConfigParser);
        }

        internal static Stream ReadConfigFile(String configPath)
        {
            Stream fileConfig;
            try
            {
                fileConfig = File.Open(configPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception e)
            {
                MessageProcessorException ex = new MessageProcessorException("Error in reading the configuration file.", e);
                Logger.GetInstance().Write(ex);
                throw ex;
            }
            return fileConfig;
        }

        public void Load()
        {
            using (Stream fileConfig = ReadConfigFile(defaultConfigPath))
            {
                Load(fileConfig);
            }
        }

        public void Shutdown()
        {
        }

        public IProcessedMessage Send(Object model, IMessageStream stream, MessageCallback callback)
        {
            MessageProcessorWorker processThread = new MessageProcessorWorker(model, stream, callback);
            Thread thread = new Thread(processThread.SendMessage);
            processThread.ThreadId = thread.ManagedThreadId;
            thread.Start();
            return processThread;
        }

        public IProcessedMessage Send(Object model, IMessageStream stream)
        {
            return Send(model, stream, null);
        }

        public IProcessedMessage Receive(byte[] message, IMessageStream stream, MessageCallback callback)
        {
            MessageProcessorWorker processThread = new MessageProcessorWorker(message, stream, callback);
            Thread thread = new Thread(processThread.ReceiveMessage);
            processThread.ThreadId = thread.ManagedThreadId;
            thread.Start();
            return processThread;
        }

        public IProcessedMessage Receive(byte[] message, MessageCallback callback)
        {
            return Receive(message, null, callback);
        }

        public IProcessedMessage Receive(byte[] message, IMessageStream stream)
        {
            return Receive(message, stream, null);
        }
    }

    public interface IMessageStream
    {
        byte[] Send(byte[] message);
        int Receive(byte[] buffer, int offset, int count);
        void Close();
    }

    public interface IModelParsingReceiver
    {
        IParsedMessage ParsedMessage { set; }
    }

    public interface IProcessedMessage
    {
        Object ReceivedModel { get; }
        Object SentModel { get; }
        byte[] ReceivedMessage { get; }
        byte[] SentMessage { get; }
    }

    public delegate Object MessageCallback(Object model);

    internal class MessageProcessorWorker : IProcessedMessage
    {
        private static IDictionary<String, MessageProcessorWorker> _threads = new Dictionary<String, MessageProcessorWorker>();
        private MessageParser _parser;

        public String Id { get; private set; }
        public int ThreadId { get; internal set; }
        
        public Object ReceivedModel { get; private set; }
        public Object SentModel { get; private set; }
        public byte[] ReceivedMessage { get; private set; }
        public byte[] SentMessage { get; private set; }
        public IMessageStream MessageStream { get; set; }
        public MessageCallback Callback { get; set; }
        internal config.MessageToModelConfig MessageToModelConfig { get; private set; }

        public MessageProcessorWorker(byte[] receivedMessage, IMessageStream stream, MessageCallback callback)
        {
            ReceivedMessage = receivedMessage;
            MessageStream = stream;
            Callback = callback;
        }

        public MessageProcessorWorker(Object sentModel, IMessageStream stream, MessageCallback callback)
        {
            SentModel = sentModel;
            MessageStream = stream;
            Callback = callback;
        }

        private void WriteMessageProcessException(Exception e, String log)
        {
            StringBuilder sbLog = new StringBuilder(log);
            String nl = System.Environment.NewLine;
            if (e is MessageParserException)
            {
                sbLog.Append(" Message:").Append(nl);
                String message = MessageUtility.HexToReadableString(ReceivedMessage, 80);
                sbLog.Append(message);
            }
            else if (e is MessageCompilerException)
            {
                sbLog.Append(" Model:").Append(nl);
                sbLog.Append(Util.GetReadableStringFromModel(SentModel));
            }
            Logger.GetInstance().Write(e, sbLog.ToString());
        }

        private void NotifyError()
        {
            try  //Notifies the caller that there is an error but prepare for a new error
            {
                if (Callback != null)
                {
                    if (Callback.Target is IModelParsingReceiver) ((IModelParsingReceiver)Callback.Target).ParsedMessage = _parser;
                    Callback(null);
                }
                else if (_parser != null && _parser.ProcessModel != null)
                {
                    if (_parser.ProcessModel.Target is IModelParsingReceiver)
                        ((IModelParsingReceiver)_parser.ProcessModel.Target).ParsedMessage = _parser;
                    _parser.ProcessModel.DynamicInvoke(null);
                }
            }
            catch (Exception e)
            {
                WriteMessageProcessException(e, "Error in Callback (possibly, cannot process null parameter).");
            }
        }

        private Object ProcessModel()
        {
            Object retModel = null;
            if (Callback != null)
            {
                if (Callback.Target is IModelParsingReceiver) ((IModelParsingReceiver)Callback.Target).ParsedMessage = _parser;
                retModel = Callback(ReceivedModel);
            }
            else if (_parser.ProcessModel != null)
            {
                if (_parser.ProcessModel.Target is IModelParsingReceiver)
                    ((IModelParsingReceiver)_parser.ProcessModel.Target).ParsedMessage = _parser;
                retModel = _parser.ProcessModel.DynamicInvoke(ReceivedModel);
            }

            return retModel;
        }

        private void Parse()
        {
            _parser = new MessageParser(ReceivedMessage, this);
            _parser.Parse();
        }

        private byte[] Send(IParsedMessage pMsg)
        {
            SentMessage = null;
            if (MessageStream == null) return null;
            MessageCompiler compiler = new MessageCompiler(SentModel, pMsg);
            compiler.Compile();
            SentMessage = compiler.CompiledMessage.GetAllBytes();
            return MessageStream.Send(SentMessage);
        }

        private byte[] Receive()
        {
            if (MessageStream == null) return null;
            int readLength = config.MaskConfig.MinBytesCountToCheck;
            byte[] checkData = new byte[readLength];
            int offset = 0, numberReadData = 0;
            do
            {
                numberReadData = MessageStream.Receive(checkData, offset, readLength);
                offset += numberReadData;
                readLength -= numberReadData;
            } while (readLength > 0 && numberReadData > 0);
            
            config.MessageToModelConfig cfg = config.MessageConfigs.GetQulifiedMessageToModel(checkData);
            MessageToModelConfig = cfg;
            if (cfg == null)
            {
                throw new MessageProcessorException("Unrecognized message. Check message-to-model elements in configuration file."
                    + Environment.NewLine
                    + readLength + " first bytes of message: " + MessageUtility.HexToReadableString(checkData));
            }
            
            byte[] lengthHeader = new byte[cfg.ModelCfg.MessageCfg.LengthHeader];
            Array.Copy(checkData, lengthHeader, lengthHeader.Length);
            int messageLength = (int)MessageUtility.BytesToInt(lengthHeader);
            messageLength += lengthHeader.Length;
            
            byte[] data = new byte[messageLength];
            Array.Copy(checkData, data, checkData.Length);
            offset = checkData.Length;
            readLength = messageLength - checkData.Length;
            do
            {
                numberReadData = MessageStream.Receive(data, offset, readLength);
                offset += numberReadData;
                readLength -= numberReadData;
            } while (readLength > 0 && numberReadData > 0);
            return data;
        }

        public void ReceiveMessage()
        {
            try
            {
                RegisterMe();
                if (ReceivedMessage == null || ReceivedMessage.Length <= 0) ReceivedMessage = Receive();
                Parse();
                ReceivedModel = _parser.Model;

                SentModel = ProcessModel();

                if (SentModel != null)
                {
                    Send(_parser);
                }
                else
                {
                    SentMessage = null;
                }
            }
            catch (Exception e)
            {
                WriteMessageProcessException(e, "Cannot process the message.");
                NotifyError();
            }
            finally
            {
                if (MessageStream != null) MessageStream.Close();
                UnregisterMe();
            }
        }

        public void SendMessage()
        {
            try
            {
                RegisterMe();
                ReceivedMessage = Send(null);
                if (ReceivedMessage != null && ReceivedMessage.Length <= 0) ReceivedMessage = Receive();
                ReceivedModel = null;
                if (ReceivedMessage != null)
                {
                    Parse();
                    ReceivedModel = _parser.Model;
                }
                if (ReceivedModel != null)
                {
                    ProcessModel();
                }
            }
            catch (Exception e)
            {
                WriteMessageProcessException(e, "Cannot process the message.");
                NotifyError();
            }
            finally
            {
                if (MessageStream != null) MessageStream.Close();
                UnregisterMe();
            }
        }

        private void RegisterMe()
        {
            String id = DateTime.Now.Ticks.ToString();
            int i = 0;
            lock (((ICollection)_threads).SyncRoot)
            {
                while (_threads.ContainsKey(id + i)) i++;
                Id = id + i;
                _threads[Id] = this;
            }
        }

        private void UnregisterMe()
        {
            if (Id == null) return;
            lock (((ICollection)_threads).SyncRoot)
            {
                _threads.Remove(Id);
            }
        }

        public static MessageProcessorWorker GetThread(String id)
        {
            lock (((ICollection)_threads).SyncRoot)
            {
                if (_threads.ContainsKey(id)) return _threads[id];
                return null;
            }
        }
    }
}
