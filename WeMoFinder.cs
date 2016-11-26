using System;
using System.Collections.Generic;
using System.Linq;
using UPNPLib;


namespace Karpach.Wemo.Switcher
{
    class WeMoFinder
    {
        public WeMoDevice Find(string name)
        {
            return Find().FirstOrDefault(x => x.Name == name);
        }

        private IEnumerable<WeMoDevice> Find()
        {
            UPnPDeviceFinder finder = new UPnPDeviceFinder();
            UPnPDevices found = finder.FindByType("urn:Belkin:device:controllee:1", 0);

            var devices = found.Cast<UPnPDevice>().Select(device =>
            {
                var uri = new Uri(device.PresentationURL);                
                return new WeMoDevice
                {
                    Name = device.FriendlyName,
                    IpAddress = uri.Host,
                    Port = uri.Port
                };
            });

            return devices.ToList();
        }
    }
}