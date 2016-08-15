using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using NLog;
using NUnit.Framework;

namespace BaseLine.UnitTests
{
    class UnitTesting
    {
        

        // Single IP/Port check
        [Test]
        public void ShouldValidateCheckingIpAddress()
        {
            //Logger Log = LogManager.GetCurrentClassLogger();

            IpValidation x1 = new IpValidation();
            x1.IpAddress = IPAddress.Parse("127.0.0.1");
            x1.AddressPort = 80;

            Log.Logger.Debug("");
            Log.Logger.Error("lkjasdflkjsadlfkj");

            Assert.That(x1.Check());
        }

        // Multiple Ports on Single IP address check
        public static IList<IpValidation> ValidPorts = new List<IpValidation>();

        [Test]
        public void TestAllPortsIP()
        {
            
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            for (int i = 0; i < 256; i++)
            {
                Debug.WriteLine(String.Format("Checking Port: {0}", i));
                IpValidation IpRange = new IpValidation();



                Thread chkIpPort = new Thread(() =>
                                                  {
                                                      CheckIpAddress(ip, i);
                                                  });
                chkIpPort.Start();
            }

            Log.Logger.Debug(String.Format("Number of Valid Ports: {0}",ValidPorts.Count));
            
        }

        public static void CheckIpAddress(IPAddress IpAddress, int Port)
        {
            Log.Logger.Debug(String.Format("Checking Port: {0} for IP Address: {1}", Port, IpAddress));
            IpValidation IpRange = new IpValidation();
            IpRange.IpAddress = IpAddress;
            IpRange.AddressPort = Port;
            if (IpRange.Check())
            {
                ValidPorts.Add(new IpValidation()
                {
                    NameOfValidation = TypeOfValidation.ValidationName.PortCheck,
                    Details = String.Format("{0}:{1}",IpAddress.ToString(), Port),
                    PassedValidation = true,
                });
            }
        }


        [Test]
        public void SimpleTest()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();
        }
    }
}
