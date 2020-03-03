using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Net.Http;
using System.Net;

namespace Forme
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage,INotifyPropertyChanged
    {
        public MainPage()
        {
            InitializeComponent();

            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;

            var level = (int)((Battery.ChargeLevel*100)); // returns 0.0 to 1.0 or 1.0 when on AC or no battery.
            
            var state = Battery.State;

            levelProperty.Detail = level.ToString()+"%";

            //if (level == 100)
            //{
            //    FormeNotCharging();
            //}
            //else {
            //    FormeCharging();
            //}

            switch (state)
            {
                case BatteryState.Charging:
                    chargingProperty.Detail = "Charging";
                    FormeCharging();
                    // Currently charging
                    break;
                case BatteryState.Full:
                    chargingProperty.Detail = "Full";
                    FormeNotCharging();
                    // Battery is full
                    break;
                case BatteryState.Discharging:
                case BatteryState.NotCharging:
                    chargingProperty.Detail = "Discharging";
                    FormeNotCharging();
                    break;
                case BatteryState.NotPresent:
                // Battery doesn't exist in device (desktop computer)
                case BatteryState.Unknown:
                    chargingProperty.Detail = "NA";
                    FormeNotCharging();
                    break;
            }

            var source = Battery.PowerSource;

            switch (source)
            {
                case BatteryPowerSource.Battery:
                    sourceProperty.Detail = "Battery";
                    // Being powered by the battery
                    break;
                case BatteryPowerSource.AC:
                    sourceProperty.Detail = "AC";
                    // Being powered by A/C unit
                    break;
                case BatteryPowerSource.Usb:
                    sourceProperty.Detail = "USB";
                    // Being powered by USB cable
                    break;
                case BatteryPowerSource.Wireless:
                    sourceProperty.Detail = "Wireless";
                    // Powered via wireless charging
                    break;
                case BatteryPowerSource.Unknown:
                    sourceProperty.Detail = "Unknown";
                    // Unable to detect power source
                    break;
            }
        }

        void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            var level = (int)((Battery.ChargeLevel * 100)); // returns 0.0 to 1.0 or 1.0 when on AC or no battery.
            var state = Battery.State;
            var source = Battery.PowerSource;

            levelProperty.Detail = level.ToString() + "%";

            switch (state)
            {
                case BatteryState.Charging:
                    chargingProperty.Detail = "Charging";
                    // Currently charging
                    break;
                case BatteryState.Full:
                    chargingProperty.Detail = "Full";
                    // Battery is full
                    break;
                case BatteryState.Discharging:
                case BatteryState.NotCharging:
                    chargingProperty.Detail = "Discharging";
                    break;
                case BatteryState.NotPresent:
                // Battery doesn't exist in device (desktop computer)
                case BatteryState.Unknown:
                    chargingProperty.Detail = "NA";
                    break;
            }

            switch (source)
            {
                case BatteryPowerSource.Battery:
                    sourceProperty.Detail = "Battery";
                    // Being powered by the battery
                    break;
                case BatteryPowerSource.AC:
                    sourceProperty.Detail = "AC";
                    // Being powered by A/C unit
                    break;
                case BatteryPowerSource.Usb:
                    sourceProperty.Detail = "USB";
                    // Being powered by USB cable
                    break;
                case BatteryPowerSource.Wireless:
                    sourceProperty.Detail = "Wireless";
                    // Powered via wireless charging
                    break;
                case BatteryPowerSource.Unknown:
                    sourceProperty.Detail = "Unknown";
                    // Unable to detect power source
                    break;
            }

            if (level == 100) {
                Stop_Clicked(sender,e);
            }
        }

        void Start_Clicked(System.Object sender, System.EventArgs e)
        {

            using (var wb = new WebClient())
            {
                wb.DownloadString("http://10.10.10.1/RELAY=ON");
            }
            FormeCharging();
            
        }

        void Stop_Clicked(System.Object sender, System.EventArgs e)
        {
            using (var wb = new WebClient())
            {
                wb.DownloadString("http://10.10.10.1/RELAY=OFF");
            }
            FormeNotCharging();
        }

        void SwitchCellAlarm_Tapped(System.Object sender, System.EventArgs e)
        {
            alarmSwitch.On = true;
            fullSwitch.On = false;
            timeSwitch.On = false;

        }

        void SwitchCellTime_Tapped(System.Object sender, System.EventArgs e)
        {
            alarmSwitch.On = false;
            fullSwitch.On = false;
            timeSwitch.On = true;
        }

        void SwitchCellFull_Tapped(System.Object sender, System.EventArgs e)
        {
            alarmSwitch.On = false;
            fullSwitch.On = true;
            timeSwitch.On = false;
        }

        public void FormeCharging() {
            stop.IsEnabled = true;
            stop.TextColor = Color.Red;

            start.IsEnabled = false;
            start.TextColor = Color.Gray;
        }
        public void FormeNotCharging() {
            start.IsEnabled = true;
            start.TextColor = Color.LimeGreen;

            stop.IsEnabled = false;
            stop.TextColor = Color.Gray;
        }
    }
}
