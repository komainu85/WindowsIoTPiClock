using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.Devices.PointOfService;
using Windows.Security.Cryptography.Core;
using Adafruit_LEDBackpack;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace PiClock
{
    public sealed class StartupTask : IBackgroundTask
    {

        private AlphaNumericFourCharacters _alphaNumericFourCharacters = null;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var backgroundTask = taskInstance.GetDeferral();

            _alphaNumericFourCharacters = new AlphaNumericFourCharacters(0x70);

            ConfigureDisplay();

            while (true)
            {
                UpdateTime();
                await Task.Delay(TimeSpan.FromSeconds(30)); // need a better way to do this
            }
        }

        private void ConfigureDisplay()
        {
            _alphaNumericFourCharacters.SetBlinkRate(0);
            _alphaNumericFourCharacters.SetBrightness(15);
            _alphaNumericFourCharacters.ClearDisplay();
        }

        public void UpdateTime()
        {
            _alphaNumericFourCharacters.WriteCharacters(DateTime.Now.Hour.ToString().ToCharArray().First(), DateTime.Now.Hour.ToString().ToCharArray().Last(), DateTime.Now.Minute.ToString().ToCharArray().First(), DateTime.Now.Minute.ToString().ToCharArray().Last());
            _alphaNumericFourCharacters.WriteDisplay();
        }


    }


}

