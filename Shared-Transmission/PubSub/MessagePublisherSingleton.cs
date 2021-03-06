﻿using System;
using System.Text;
using ZMQ;

namespace Shared_Transmission.PubSub
{
    public static class MessagePublisherSingleton
    {
        public static void SendMessage(string message)
        {
            if (!_configured)
                throw new ApplicationException("You must configure the MessagePublisherSingleton before you can send a message");

            if (!_config.TestMode)
            {
                _socket.Send(message, Encoding.Unicode);
            }
        }

        public static void Configure(MessagePublisherConfig config)
        {
            _config = config;
            _configured = true; 

            if (!_config.TestMode)
            {
                _context = new Context(1);
                _socket = _context.Socket(SocketType.PUB); 
                _socket.Bind(_config.Host);
            }
        }

        public static void Dispose()
        {
            _configured = false;

            if (!_config.TestMode)
            {
                _socket.Dispose();
                _context.Dispose();
            }
        }

        private static MessagePublisherConfig _config;
        private static Context _context;
        private static Socket _socket;
        private static bool _configured = false; 
    }
}