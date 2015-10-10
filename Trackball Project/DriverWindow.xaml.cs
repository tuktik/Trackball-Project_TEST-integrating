using MahApps.Metro.Controls;   // Metro Import
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Management;
using System.Windows.Interop;

namespace Trackball_Project
{
    /// <summary>
    /// DriverWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DriverWindow : MetroWindow
    {
        private static MainWindow mMainWindow = null;


        private List<String> MouseList = new List<string>();
        private List<USBDeviceInfo> usbDevice;

        InputDevice id;
        int NumKeyboard;
        private String setVID, setPID;

        public DriverWindow()
        {
            InitializeComponent();
        }
        public DriverWindow(MainWindow mainWindow)
        {
            mMainWindow = mainWindow;
            InitializeComponent();

            // 설치된 마우스 정보를 가져온다.
            GetDeviceList dList = new GetDeviceList();
            usbDevice = dList.getList();
            foreach (var usbList in usbDevice)
            {
                Console.WriteLine("\nDevice VID : {0}\nDevice PID : {1}\nDevice NAME : {2}\n", usbList.VID, usbList.PID, usbList.NAME);
                MouseList.Add(usbList.NAME);
            }
            this.DeviceList.ItemsSource = MouseList;

            //RegisterMouseEvnet();

        }



        // hWnd 가져오기
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern int FindWindowEx(int hwndParent, int hwndEnfant, int lpClasse, string lpTitre);

        private void Mouse_Selected(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int idx = 0;
            if (DeviceList.SelectedItem != null)
                idx = DeviceList.SelectedIndex;
            setVID = usbDevice[idx].VID;
            setPID = usbDevice[idx].PID;
        }

        private void Device_Drive_Select_Btn(object sender, RoutedEventArgs e)
        {
            if (setVID == null && setPID == null)
            {
                MessageBox.Show("선택한 마우스가 없습니다.");
                return;
            }
            mMainWindow.setVID = setVID;
            mMainWindow.setPID = setPID;
            mMainWindow.auto_start();
            int hwnd = FindWindowEx(0, 0, 0, "데굴데굴");
            Console.WriteLine(hwnd);
            this.Close();
        }







        #region 설치된 디바이스 목록 가져오기
        static List<GetDeviceID> GetUSBDevices()
        {
            List<GetDeviceID> devices = new List<GetDeviceID>();

            ManagementObjectCollection collection;
            //using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity"))
            //using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))
            //using (var searcher = new ManagementObjectSearcher(@"Select * FROM Win32_DiskDrive WHERE InterfaceType='USB'"))
            using (var searcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_PnPEntity"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                devices.Add(new GetDeviceID(
                (string)device.GetPropertyValue("DeviceID"),
                (string)device.GetPropertyValue("PNPDeviceID"),
                (string)device.GetPropertyValue("Description")
                ));
            }

            collection.Dispose();
            return devices;
        }
        #endregion 설치된 디바이스 목록 가져오기

        #region 마우스 리스트 가져오기
        public class GetDeviceList
        {
            [DllImport("GetDeviceEvent.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr ListDevices(string vid, string pid);

            public string t;

            List<USBDeviceInfo> dInfo;

            public List<USBDeviceInfo> getList() { return this.dInfo; }

            public GetDeviceList()
            {
                dInfo = new List<USBDeviceInfo>();

                var usbDevices = GetUSBDevices();

                foreach (var usbDevice in usbDevices)
                {
                    //Console.WriteLine("\nDevice ID: {0}\nPNP Device ID: {1}\nDescription: {2}\n", usbDevice.DeviceID, usbDevice.PnpDeviceID, usbDevice.Description);
                    string PNPDeviceID = usbDevice.PnpDeviceID;
                    //string DeviceID = PNPDeviceID.Substring(0, 3);usbDevice.Description
                    string DeviceID = usbDevice.Description;
                    if (DeviceID.Equals("HID 규격 마우스"))
                    {
                        string DeviceVID = PNPDeviceID.Substring(4, 8);
                        string DevicePID = PNPDeviceID.Substring(13, 8);
                        //Console.WriteLine("\nDevice ID: {0}\nPNP Device ID: {1}\nDescription: {2}\n", DeviceID, DeviceVID, DevicePID);

                        string DeviceName = Marshal.PtrToStringAnsi(ListDevices(DeviceVID, DevicePID));
                        if (!DeviceName.Equals("FAIL"))
                        {
                            bool dCheck = true;
                            foreach (var cList in dInfo)
                            {
                                if (cList.NAME.Equals(DeviceName))
                                {
                                    dCheck = false;
                                    break;
                                }
                            }
                            if (dCheck) dInfo.Add(new USBDeviceInfo(DeviceVID, DevicePID, DeviceName));
                        }
                        else
                            Console.WriteLine("\nReceive Fail\n");
                    }
                }
            }
        }
        #endregion 마우스 리스트 가져오기
    }


    class GetDeviceID
    {
        public GetDeviceID(string deviceID, string pnpDeviceID, string description)
        {
            this.DeviceID = deviceID;
            this.PnpDeviceID = pnpDeviceID;
            this.Description = description;
        }
        public string DeviceID { get; private set; }
        public string PnpDeviceID { get; private set; }
        public string Description { get; private set; }
    }

    public class USBDeviceInfo
    {
        public USBDeviceInfo(string VID, string PID, string NAME)
        {
            this.VID = VID;
            this.PID = PID;
            this.NAME = NAME;
        }
        public string VID { get; private set; }
        public string PID { get; private set; }
        public string NAME { get; private set; }
    }
}
