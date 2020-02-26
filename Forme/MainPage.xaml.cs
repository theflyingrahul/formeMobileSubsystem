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
        Boolean alarmBool, timeBool, fullBool;
        public MainPage()
        {
            InitializeComponent();

            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;

            alarmBool = true;
            timeBool = false;
            fullBool = false;

            var level = (int)((Battery.ChargeLevel*100)); // returns 0.0 to 1.0 or 1.0 when on AC or no battery.
            
            var state = Battery.State;

            levelProperty.Detail = level.ToString()+"%";

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
        }

        void Start_Clicked(System.Object sender, System.EventArgs e)
        {

            using (var wb = new WebClient())
            {
                wb.DownloadString("http://10.10.10.1/RELAY=ON");
            }
            //using (var client = new HttpClient())
            //{
            //    // send a GET request  
            //    var uri = new Uri("http://10.10.10.1/RELAY=ON");
            //    await client.GetAsync(uri);
            //}
        }
        void Stop_Clicked(System.Object sender, System.EventArgs e)
        {
            using (var wb = new WebClient())
            {
                wb.DownloadString("http://10.10.10.1/RELAY=OFF");
            }
            //using (var client = new HttpClient())
            //{
            //    // send a GET request  
            //    var uri = "http://10.10.10.1/RELAY=OFF";
            //    await client.GetStringAsync(uri);
            //}
        }

        void SwitchCellAlarm_Tapped(System.Object sender, System.EventArgs e)
        {
            alarmBool = !alarmBool;
            timeBool = false;
            fullBool = false;

            OnPropertyChanged("alarmBool");
            OnPropertyChanged("timeBool");
            OnPropertyChanged("fullBool");

        }
        void SwitchCellTime_Tapped(System.Object sender, System.EventArgs e)
        {
            timeBool = !timeBool;
            fullBool = false;
            alarmBool = false;

            OnPropertyChanged("alarmBool");
            OnPropertyChanged("timeBool");
            OnPropertyChanged("fullBool");
        }
        void SwitchCellFull_Tapped(System.Object sender, System.EventArgs e)
        {
            fullBool = !fullBool;
            timeBool = false;
            alarmBool = false;

            OnPropertyChanged("alarmBool");
            OnPropertyChanged("timeBool");
            OnPropertyChanged("fullBool");
        }
        public new event PropertyChangedEventHandler PropertyChanged;
        protected override void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
