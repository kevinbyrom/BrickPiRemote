using System;
using System.Configuration;


namespace BrickPiRemote
{
    public static class UdpClientConfig
    {
        public static string Host { get { return ConfigurationManager.AppSettings["UdpClientHost"];  } }
        public static int Port { get { return int.Parse(ConfigurationManager.AppSettings["UdpClientPort"]); } }
    }
}
