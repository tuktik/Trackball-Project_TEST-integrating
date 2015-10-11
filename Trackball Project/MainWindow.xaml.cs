using MahApps.Metro.Controls;   // Metro를 사용하기 위한 Import
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

using MahApps.Metro;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using WinForms = System.Windows.Forms;  // Tray Icon 사용 시 설정
using System.Windows.Interop;
using System.Runtime.InteropServices;

using virtual_desktop;

namespace Trackball_Project
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private static MainWindow sMainWindow = null;
        static readonly object padlock = new object();

        InputDevice id;
        int NumKeyboard;
        IntPtr nHwnd;
        public int hwnd;
        public String setVID, setPID;
        MouseHook mouseHook = new MouseHook();

        // hWnd 가져오기
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern int FindWindowEx(int hwndParent, int hwndEnfant, int lpClasse, string lpTitre);

        private Hashtable mButtonClickList = new Hashtable();
        private Uri uriNone = new Uri(@"/Image/None.png", UriKind.Relative);
        private Uri uriMusic = new Uri(@"/Image/Music.png", UriKind.Relative);
        private Uri uriVideo = new Uri(@"/Image/Video.png", UriKind.Relative);
        private Uri uriPicture = new Uri(@"/Image/Picture.png", UriKind.Relative);
        private Uri uriDesktop = new Uri(@"/Image/Desktop.png", UriKind.Relative);
        private Uri uriCustom = new Uri(@"/Image/Custom.png", UriKind.Relative);

        // TrayIcon Toogle을 위해 임시로 변수 지정
        private bool TestToggleFlag = false;

        public Data LeftUp = null;
        public Data RightUp = null;
        public Data LeftDown = null;
        public Data RightDown = null;

        // TrayIcon 관련 Member 변수
        //private System.Windows.Threading.DispatcherTimer mTimer;
        public System.Windows.Forms.NotifyIcon mNotify;

        // Unit Class를 이용한 기능 구현을 시도했지만 뭔가 안되서 실패...
        /*
        public Unit leftTop = null;
        public Unit leftDown = null;
        public Unit rightTop;
        public Unit rightDown;
        */
        public MainWindow()
        {
            sMainWindow = this;                 // 싱클톤 패턴 구현을 위한 static 멤버 변수에 자기 자신 객체 할당
            InitializeComponent();              // WindowsForm을 실행 할 떄 필수적으로 호출 
            TrayIconSetting();                  // TrayIcon 관련 객체 생성 및 기본 세팅
            InitializeTrackballButtomImage();   // 기본 트랙볼 버튼 기능을 None으로 변경하고 해당 이미지로 변경
            InitializeDataMember();
            //this.DataContext = new AppearanceViewModel();

            // get the theme from the current application
            var theme = ThemeManager.DetectAppStyle(Application.Current);

            // now set the Green accent and dark theme
            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent("Blue"),
                                        ThemeManager.GetAppTheme("BaseLight"));
            //hwnd = FindWindowEx(0, 0, 0, "데굴데굴");
            RegisterMouseEvnet();
            
        }

        public void ColorChange(object sender, RoutedEventArgs e)
        {
            var color = sender;
            if (sender == Red)
            {
                ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent("Red"),
                                        ThemeManager.GetAppTheme("BaseLight"));
            }
            else if (sender == Blue)
            {
                ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent("Blue"),
                                        ThemeManager.GetAppTheme("BaseLight"));
            }
            else if (sender == Green)
            {
                ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent("Green"),
                                        ThemeManager.GetAppTheme("BaseLight"));
            }
            else if (sender == Purple)
            {
                ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent("Purple"),
                                        ThemeManager.GetAppTheme("BaseLight"));
            }
        }

        // 싱글톤을 위해 사용했지만 무슨 의도인지 아직 모름
        public static MainWindow Instance
        {
            get
            {
                lock (padlock)
                {
                    if (sMainWindow == null)
                    {
                        sMainWindow = new MainWindow();
                    }
                    return sMainWindow;
                }
            }
        }

        private void InitializeDataMember() {
            LeftUp = new Data(false, "None");
            RightUp = new Data(false, "None");
            LeftDown = new Data(false, "None");
            RightDown = new Data(false, "None");
        }
        public Data getLeftUp()
        {
            return LeftUp;
        }
        public Data getRightUp()
        {
            return RightUp;
        }
        public Data getLeftDown()
        {
            return LeftDown;
        }
        public Data getRightDown()
        {
            return RightDown;
        }
        public void printDatas()
        {
            Console.WriteLine("LeftUp - " + LeftUp.getFunctionName());
            Console.WriteLine("RightUp - " + RightUp.getFunctionName());
            Console.WriteLine("LeftDown - " + LeftDown.getFunctionName());
            Console.WriteLine("RightDown - " + RightDown.getFunctionName());
            Console.WriteLine("- - - - - - - - - - -");
        }

        private void InitializeTrackballButtomImage()
        {
            SetImageSource(LeftTopIcon, "None");
            SetImageSource(RightTopIcon, "None");
            SetImageSource(LeftDownIcon, "None");
            SetImageSource(RightDownIcon, "None");

            mButtonClickList["Music"] = "NotExist";
            mButtonClickList["Video"] = "NotExist";
            mButtonClickList["Picture"] = "NotExist";
            mButtonClickList["Desktop"] = "NotExist";
            mButtonClickList["Custom"] = "NotExist";
        }
        public void SetImageSource(Image direction, String func)
        {
            if (func.Equals("None"))
            {
                direction.Source = new BitmapImage(uriNone);
                mButtonClickList[func] = "NotExist";
            }
            else
            {
                if (func.Equals("Music"))
                    direction.Source = new BitmapImage(uriMusic);
                else if (func.Equals("Video"))
                    direction.Source = new BitmapImage(uriVideo);
                else if (func.Equals("Picture"))
                    direction.Source = new BitmapImage(uriPicture);
                else if (func.Equals("Desktop"))
                    direction.Source = new BitmapImage(uriDesktop);
                else if (func.Equals("Custom"))
                    direction.Source = new BitmapImage(uriCustom);
                mButtonClickList[func] = "Exist";
            }
        }
        public Image GetLeftTopIconImage()
        {
            return LeftTopIcon;
        }
        public Image GetRightTopIconImage()
        {
            return RightTopIcon;
        }
        public Image GetLeftDownIconImage()
        {
            return LeftDownIcon;
        }
        public Image GetRightDownIconImage()
        {
            return RightDownIcon;
        }

        public void SetButtonClicked(String direction, String str) {
            mButtonClickList[direction] = str;
        }
        public object GetButtonClickList(String direction)
        {
            return mButtonClickList[direction];
        }


        // TrayIcon 구현부
        private void TrayIconSetting()
        {
            try
            {
                // ContextMenu
                // 메뉴 패널이 등장하도록 설정하는 부분
                // (트레이 아이콘에서 마우스 우클릭 시 나타나는 메뉴 설정)
                System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();
                System.Windows.Forms.MenuItem item1 = new System.Windows.Forms.MenuItem();
                System.Windows.Forms.MenuItem item2 = new System.Windows.Forms.MenuItem();
                menu.MenuItems.Add(item1);
                menu.MenuItems.Add(item2);
                item1.Index = 0;
                item1.Text = "프로그램 종료";
                item1.Click += delegate(object click, EventArgs eClick) {
                    MessageBox.Show("프로그램 종료");
                    mNotify.Visible = false;
                    this.Close();
                };
                item2.Index = 0;
                item2.Text = "프로그램 보기";
                item2.Click += delegate(object click, EventArgs eClick)
                {
                    MessageBox.Show("프로그램 보기");
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };

                mNotify = new System.Windows.Forms.NotifyIcon();
                mNotify.Icon = Trackball_Project.Properties.Resources.ICON;
                mNotify.Visible = true;
                mNotify.DoubleClick += delegate(object senders, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
                mNotify.ContextMenu = menu;
                mNotify.Text = "데굴데굴 실행중";

                mNotify.BalloonTipTitle = "데굴데굴";
                mNotify.BalloonTipText = "프로그램이 실행 중입니다.";
                mNotify.ShowBalloonTip(1000);
                //mTimer = new System.Windows.Threading.DispatcherTimer();
                //mTimer.Interval = new TimeSpan(0, 0, 2);
                //mTimer.Tick += new EventHandler(timer_Tick);
                //mTimer.Start();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        // 윈도우 종료 이벤트 호출 시 트레이 아이콘 강제로 삭제
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mNotify.Visible = false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState.Minimized.Equals(WindowState))  // 윈도우가 최소화 되었을 때
            {
                this.Hide();
                mNotify.ShowBalloonTip(2000);
                MessageBox.Show("데굴데굴이 최소화 되었습니다.");
            }
            base.OnStateChanged(e);
        }
        /*
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            mNotify = new System.Windows.Forms.NotifyIcon();
            mNotify.BalloonTipText = "this app has been minimised";
            mNotify.BalloonTipTitle = "BallopnTipTitle";
            mNotify.Text = "Text";
            mNotify.Icon = Properties.Resources.ICON;
            mNotify.Click += m_notifyIcon_Click;

            //DispatcherTimer timer = new DispatcherTimer();
        }
        */
        // TrayIcon 관련 콜백함수 구현
        private int c = 0;  // 변수명 마음에 안듬.. 생각나는거 있음 바꿀 예정
        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = System.Windows.WindowState.Normal;
        }
        void timer_Tick(object sender, EventArgs e)
        {
            mNotify.BalloonTipTitle = "Title";
            mNotify.BalloonTipText = "BalloonTip Text";
            mNotify.ShowBalloonTip(1000);
            ++c;
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // when window is unvisible, change visible option tray icon
            if (IsVisible == false)
                mNotify.Visible = true;
        }
        public void ToggleTrayIcon(object sender, RoutedEventArgs e)
        {
            if (!TestToggleFlag)
            {
                mNotify.Icon = Trackball_Project.Properties.Resources.ICON;
                TestToggleFlag = true;
            }
            else
            {
                mNotify.Icon = Trackball_Project.Properties.Resources.Trackball_Off;
                TestToggleFlag = false;
            }

        }

        // Button Click Methods...
        private void Trackball_Button_Click(object sender, RoutedEventArgs e)
        {
            TrackballPage.IsOpen = true;
        }
        private void Custom_Button_Click(object sender, RoutedEventArgs e)
        {
            CustomPage.IsOpen = true;
        }
        private void Setting_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingPage.IsOpen = true;
        }

        private void Click_LeftTop(object sender, RoutedEventArgs e)
        {
            SelectWindow selectWindow = new SelectWindow(sMainWindow, "LeftTop");
            selectWindow.ShowDialog();
        }
        private void Click_RightTop(object sender, RoutedEventArgs e)
        {
            SelectWindow selectWindow = new SelectWindow(sMainWindow, "RightTop");
            selectWindow.ShowDialog();
        }
        private void Click_LeftDown(object sender, RoutedEventArgs e)
        {
            SelectWindow selectWindow = new SelectWindow(sMainWindow, "LeftDown");
            selectWindow.ShowDialog();
        }
        private void Click_RightDown(object sender, RoutedEventArgs e)
        {
            SelectWindow selectWindow = new SelectWindow(sMainWindow, "RightDown");
            selectWindow.ShowDialog();
        }

        private void Init_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("you click init button!");
        }
        private void Driver_Button_Click(object sender, RoutedEventArgs e)
        {
            mouseHook.Uninstall();
            DriverWindow driverWindow = new DriverWindow(sMainWindow);
            driverWindow.ShowDialog();
        }

        private void Window_Keyboard_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        // 현규 오빠 여기서부터 하시믄되여!
        private void Virtual_Desktop_Button_Click(object sender, RoutedEventArgs e)
        {
            if (setVID == null && setPID == null)
            {
                MessageBox.Show("선택한 마우스가 없습니다.");
                //return;
            }
            
            Virtual_Desktop test = new Virtual_Desktop();
            test.Show();


            //int hwnd = FindWindowEx(0, 0, 0, "데굴데굴");
            //Console.WriteLine(hwnd);

            //IntPtr myhandle = (IntPtr)hwnd;
            //id = new InputDevice(myhandle);
            //NumKeyboard = id.EnumerateDevices();
            //id.MouseEvent += new InputDevice.MouseEventHandler(m_MouseEvent);
            //id.KeyPressed += new InputDevice.DeviceEventHandler(m_KeyEvent);

            //Console.WriteLine("\nnum " + NumKeyboard);
            //mouseHook.Install();
            //MessageBox.Show("you click desktop button!!");
        }

        public void auto_start()
        {
            Auto_RID_Event.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, Auto_RID_Event));
        }



        #region hWnd 가져오기
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            var message = (WindowMessage)msg;
            this.nHwnd = hwnd;
            if (id != null)
            {
                id.ProcessMessage(msg, lParam);
            }
            return IntPtr.Zero;
        }
        #endregion hWnd 가져오기
        

        // mouseEvent Handler
        private void m_MouseEvent(object sender, InputDevice.MouseControlEventArgs e)
        {
            String logMsg = "Handler - " + e.Mouse.deviceHandle.ToString() + "\nType - " + e.Mouse.deviceType + "\nName - " + e.Mouse.deviceName + "\nDescription - " + e.Mouse.Name + "\n";
            String ID = e.Mouse.deviceName;
            String vID = ID.Substring(8, 8);
            String pID = ID.Substring(17, 8);
            //Console.WriteLine(logMsg);
            Console.WriteLine(vID);
            Console.WriteLine(pID);
            //if (vID.Equals("VID_047D") && pID.Equals("PID_2041")) { e.Mouse.setDevice = 1; mouseHook.checkDevice = true; }
            if (vID.Equals(setVID) && pID.Equals(setPID)) { e.Mouse.setDevice = 1; mouseHook.checkDevice = true; }
            else { e.Mouse.setDevice = 0; mouseHook.checkDevice = false; }
            if (e.Mouse.mButton == 2)
            {
                //keybd_event((byte)VK_BROWSER_BACK, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                //keybd_event((byte)VK_VOLUME_MUTE, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
            }

        }

        private void m_KeyEvent(object sender, InputDevice.KeyControlEventArgs e)
        {
            String logMsg = "Handler - " + e.Keyboard.deviceHandle.ToString() + "\nType - " + e.Keyboard.deviceType + "\nName - " + e.Keyboard.deviceName + "\nDescription - " + e.Keyboard.Name + "\n";
            //Console.WriteLine(logMsg);
        }
        // RAW EVENT 종료


        #region mouse hook event 등록
        public void RegisterMouseEvnet()
        {
            mouseHook.MouseMove += new MouseHook.MouseHookCallback(mouseHook_MouseMove);
            mouseHook.LeftButtonDown += new MouseHook.MouseHookCallback(mouseHook_LeftButtonDown);
            mouseHook.LeftButtonUp += new MouseHook.MouseHookCallback(mouseHook_LeftButtonUp);
            mouseHook.RightButtonDown += new MouseHook.MouseHookCallback(mouseHook_RightButtonDown);
            mouseHook.RightButtonUp += new MouseHook.MouseHookCallback(mouseHook_RightButtonUp);
            mouseHook.MiddleButtonDown += new MouseHook.MouseHookCallback(mouseHook_MiddleButtonDown);
            mouseHook.MiddleButtonUp += new MouseHook.MouseHookCallback(mouseHook_MiddleButtonUp);
            mouseHook.MouseWheel += new MouseHook.MouseHookCallback(mouseHook_MouseWheel);

            //mouseHook.Install();
        }

        void mouseHook_MouseWheel(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("MouseWheel Event");
        }

        void mouseHook_MiddleButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("MiddleButtonUp Event");
        }

        void mouseHook_MiddleButtonDown(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("MiddleButtonDown Event");
        }

        void mouseHook_RightButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("RightButtonUp Event");
        }

        void mouseHook_RightButtonDown(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("RightButtonDown Event");
        }

        void mouseHook_LeftButtonUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("LeftButtonUp Event");
        }

        void mouseHook_LeftButtonDown(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            Console.WriteLine("LeftButtonDown Event");
        }

        void mouseHook_MouseMove(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            //Console.WriteLine("X - " + mouseStruct.pt.x.ToString() + "   Y - " + mouseStruct.pt.y.ToString());
        }
        #endregion mouse hook event 등록 종료


    }
}
