using MahApps.Metro.Controls;   // Metro Import
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Trackball_Project
{
    /// <summary>
    /// SelectWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectWindow : MetroWindow
    {
        private static MainWindow mMainWindow = null;

        private Uri uriClickable = new Uri(@"/Image/Emtpy.png", UriKind.Relative);
        private Uri uriNotClickable = new Uri(@"/Image/Not Clickable.png", UriKind.Relative);

        private String mDirection = null;
        private Image mClickedImg = null;
        private Data mData = null;

        public SelectWindow()
        {
            InitializeComponent();
        }
        public SelectWindow(MainWindow mainWindow, String direction)
        {
            InitializeComponent();
            mMainWindow = mainWindow;
            mDirection = direction;

            // 설정하고자 하는 방향에 대한 정보를 확인함
            if (mDirection.Equals("LeftTop"))
            {
                mClickedImg = mMainWindow.GetLeftTopIconImage();
                mData = mMainWindow.LeftUp;
            }
            else if (mDirection.Equals("RightTop"))
            {
                mClickedImg = mMainWindow.GetRightTopIconImage();
                mData = mMainWindow.RightUp;
            }
            else if (mDirection.Equals("LeftDown"))
            {
                mClickedImg = mMainWindow.GetLeftDownIconImage();
                mData = mMainWindow.LeftDown;
            }
            else if (mDirection.Equals("RightDown"))
            {
                mClickedImg = mMainWindow.GetRightDownIconImage();
                mData = mMainWindow.RightDown;
            }
            SetButtonImageSource();
        }

        private void SetButtonImageSource() {
            /*
            if (!(mMainWindow.GetButtonClickList("Music").Equals("NotExist")))
            {

                //MusicButtonFrame.Source = new BitmapImage(uriNotClickable);
                //MusicButtonObject.IsEnabled = false;
            }
            else
                MusicButtonFrame.Source = new BitmapImage(uriClickable);

            if (!(mMainWindow.GetButtonClickList("Video").Equals("NotExist")))
            {
                //VideoButtonFrame.Source = new BitmapImage(uriNotClickable);
                //VideoButtonObject.IsEnabled = false;
            }
            else
                VideoButtonFrame.Source = new BitmapImage(uriClickable);

            if (!(mMainWindow.GetButtonClickList("Picture").Equals("NotExist")))
            {
                //PictureButtonFrame.Source = new BitmapImage(uriNotClickable);
                //PictureButtonObject.IsEnabled = false;
            }
            else
                PictureButtonFrame.Source = new BitmapImage(uriClickable);

            if (!(mMainWindow.GetButtonClickList("Desktop").Equals("NotExist")))
            {
                DesktopButtonFrame.Source = new BitmapImage(uriNotClickable);
                DesktopButtonObject.IsEnabled = false;
            }
            else
                DesktopButtonFrame.Source = new BitmapImage(uriClickable);

            if (!(mMainWindow.GetButtonClickList("Custom").Equals("NotExist")))
            {
                CustomButtonFrame.Source = new BitmapImage(uriNotClickable);
                CustomButtonObject.IsEnabled = false;
            }
            else
                CustomButtonFrame.Source = new BitmapImage(uriClickable);
            */
        }

        // Click Event Callback 함수 구현부
        private void Click_Music(object sender, RoutedEventArgs e)
        {
            String preData = mData.getFunctionName();
            if (!mDirection.Equals("LeftUp") && mMainWindow.getLeftUp().getFunctionName().Equals("Music"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetLeftTopIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getLeftUp().setIsClicked(false);
                mMainWindow.getLeftUp().setFunctionName(preData);
            }
            else if (!mDirection.Equals("RightUp") && mMainWindow.getRightUp().getFunctionName().Equals("Music"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetRightTopIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getRightUp().setIsClicked(false);
                mMainWindow.getRightUp().setFunctionName(preData);
            }
            else if (!mDirection.Equals("LeftDown") && mMainWindow.getLeftDown().getFunctionName().Equals("Music"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetLeftDownIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getLeftDown().setIsClicked(false);
                mMainWindow.getLeftDown().setFunctionName(preData);
            }
            else if (!mDirection.Equals("RightDown") && mMainWindow.getRightDown().getFunctionName().Equals("Music"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetRightDownIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getRightDown().setIsClicked(false);
                mMainWindow.getRightDown().setFunctionName(preData);
            }
            //mMainWindow.SetButtonClicked(preData, "NotExist");
            //mMainWindow.SetImageSource(mClickedImg, "None");

            mMainWindow.SetImageSource(mClickedImg, "Music");
            mData.setIsClicked(true);
            mData.setFunctionName("Music");

            //mMainWindow.printDatas();
            this.Close();
        }
        private void Click_Video(object sender, RoutedEventArgs e)
        {
            String preData = mData.getFunctionName();
            if (!mDirection.Equals("LeftUp") && mMainWindow.getLeftUp().getFunctionName().Equals("Video"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetLeftTopIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getLeftUp().setIsClicked(false);
                mMainWindow.getLeftUp().setFunctionName(preData);
            }
            else if (!mDirection.Equals("RightUp") && mMainWindow.getRightUp().getFunctionName().Equals("Video"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetRightTopIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getRightUp().setIsClicked(false);
                mMainWindow.getRightUp().setFunctionName(preData);
            }
            else if (!mDirection.Equals("LeftDown") && mMainWindow.getLeftDown().getFunctionName().Equals("Video"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetLeftDownIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getLeftDown().setIsClicked(false);
                mMainWindow.getLeftDown().setFunctionName(preData);
            }
            else if (!mDirection.Equals("RightDown") && mMainWindow.getRightDown().getFunctionName().Equals("Video"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetRightDownIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getRightDown().setIsClicked(false);
                mMainWindow.getRightDown().setFunctionName(preData);
            }
            mMainWindow.SetImageSource(mClickedImg, "Video");
            mData.setIsClicked(true);
            mData.setFunctionName("Video");

            //mMainWindow.printDatas();
            this.Close();
        }
        private void Click_Picture(object sender, RoutedEventArgs e)
        {
            String preData = mData.getFunctionName();
            if (!mDirection.Equals("LeftUp") && mMainWindow.getLeftUp().getFunctionName().Equals("Picture"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetLeftTopIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getLeftUp().setIsClicked(false);
                mMainWindow.getLeftUp().setFunctionName(preData);
            }
            else if (!mDirection.Equals("RightUp") && mMainWindow.getRightUp().getFunctionName().Equals("Picture"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetRightTopIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getRightUp().setIsClicked(false);
                mMainWindow.getRightUp().setFunctionName(preData);
            }
            else if (!mDirection.Equals("LeftDown") && mMainWindow.getLeftDown().getFunctionName().Equals("Picture"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetLeftDownIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getLeftDown().setIsClicked(false);
                mMainWindow.getLeftDown().setFunctionName(preData);
            }
            else if (!mDirection.Equals("RightDown") && mMainWindow.getRightDown().getFunctionName().Equals("Picture"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetRightDownIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getRightDown().setIsClicked(false);
                mMainWindow.getRightDown().setFunctionName(preData);
            }
            mMainWindow.SetImageSource(mClickedImg, "Picture");
            mData.setIsClicked(true);
            mData.setFunctionName("Picture");

            //mMainWindow.printDatas();
            this.Close();
        }
        private void Click_Desktop(object sender, RoutedEventArgs e)
        {
            String preData = mData.getFunctionName();
            if (!mDirection.Equals("LeftUp") && mMainWindow.getLeftUp().getFunctionName().Equals("Desktop"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetLeftTopIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getLeftUp().setIsClicked(false);
                mMainWindow.getLeftUp().setFunctionName(preData);
            }
            else if (!mDirection.Equals("RightUp") && mMainWindow.getRightUp().getFunctionName().Equals("Desktop"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetRightTopIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getRightUp().setIsClicked(false);
                mMainWindow.getRightUp().setFunctionName(preData);
            }
            else if (!mDirection.Equals("LeftDown") && mMainWindow.getLeftDown().getFunctionName().Equals("Desktop"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetLeftDownIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getLeftDown().setIsClicked(false);
                mMainWindow.getLeftDown().setFunctionName(preData);
            }
            else if (!mDirection.Equals("RightDown") && mMainWindow.getRightDown().getFunctionName().Equals("Desktop"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetRightDownIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getRightDown().setIsClicked(false);
                mMainWindow.getRightDown().setFunctionName(preData);
            }
            mMainWindow.SetImageSource(mClickedImg, "Desktop");
            mData.setIsClicked(true);
            mData.setFunctionName("Desktop");

            //mMainWindow.printDatas();
            this.Close();
        }
        private void Click_Custom(object sender, RoutedEventArgs e)
        {
            String preData = mData.getFunctionName();
            if (!mDirection.Equals("LeftUp") && mMainWindow.getLeftUp().getFunctionName().Equals("Custom"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetLeftTopIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getLeftUp().setIsClicked(false);
                mMainWindow.getLeftUp().setFunctionName(preData);
            }
            else if (!mDirection.Equals("RightUp") && mMainWindow.getRightUp().getFunctionName().Equals("Custom"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetRightTopIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getRightUp().setIsClicked(false);
                mMainWindow.getRightUp().setFunctionName(preData);
            }
            else if (!mDirection.Equals("LeftDown") && mMainWindow.getLeftDown().getFunctionName().Equals("Custom"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetLeftDownIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getLeftDown().setIsClicked(false);
                mMainWindow.getLeftDown().setFunctionName(preData);
            }
            else if (!mDirection.Equals("RightDown") && mMainWindow.getRightDown().getFunctionName().Equals("Custom"))
            {
                mMainWindow.SetImageSource(mMainWindow.GetRightDownIconImage(), preData);
                if (preData.Equals("None"))
                    mMainWindow.getRightDown().setIsClicked(false);
                mMainWindow.getRightDown().setFunctionName(preData);
            }
            mMainWindow.SetImageSource(mClickedImg, "Custom");
            mData.setIsClicked(true);
            mData.setFunctionName("Custom");

            //mMainWindow.printDatas();
            this.Close();
        }
        private void Click_None(object sender, RoutedEventArgs e)
        {
            mMainWindow.SetImageSource(mClickedImg, "None");
            mData.setIsClicked(false);
            mData.setFunctionName("None");

            //mMainWindow.printDatas();
            this.Close();
        }
    }
}
