using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace BaseLine
{
    public class IpValidation : IValidate
    {
        public TypeOfValidation.ValidationName NameOfValidation { get; set; }
        public string Details { get; set; }
        public bool PassedValidation { get; set; }

        public IPAddress IpAddress { get; set; }
        public int AddressPort { get; set; }
        
        public bool Check()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //System.Net.Sockets.TcpClient client = new TcpClient();
            try
            {
                s.Connect(IpAddress, AddressPort);
                //client.Connect(IPAddress.Parse(ipAddress), addressPort);
                return true;
            }
               
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public IEnumerable<IPEndPoint> GetAllListenerPorts()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();

            return tcpEndPoints.Where(x=>x.Port >= 0);
        }
    }
}
