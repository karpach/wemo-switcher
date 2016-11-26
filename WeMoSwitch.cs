using System.ServiceModel;
using Karpach.Wemo.Switcher.WemoService;

namespace Karpach.Wemo.Switcher
{
    public class WeMoSwitch
    {
        public static WeMoSwitch ConnectTo(string deviceName)
        {
            var device = new WeMoFinder().Find(deviceName);
            return device != null ? new WeMoSwitch(device.IpAddress, device.Port) : null;
        }

        private readonly BasicServicePortTypeClient _client;

        public WeMoSwitch(string ip, int port)
        {
            _client = new BasicServicePortTypeClient();
            _client.Endpoint.Address = new EndpointAddress($"http://{ip}:{port}/upnp/control/basicevent1");
        }

        public bool IsOff()
        {
            var state = _client.GetBinaryState(new GetBinaryState());
            return state.BinaryState != "1";
        }

        public void TurnOn()
        {
            if (IsOff())
            {
                SetState("1");
            }
        }

        public void TurnOff()
        {
            if (!IsOff())
            {
                SetState("0");
            }
        }

        private void SetState(string binaryState)
        {
            _client.SetBinaryState(new SetBinaryState { BinaryState = binaryState });
        }
    }
}