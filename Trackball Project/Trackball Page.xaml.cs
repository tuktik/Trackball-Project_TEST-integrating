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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Trackball_Project
{
    /// <summary>
    /// Trackball_Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Trackball_Page : Page
    {
        public Trackball_Page()
        {
            InitializeComponent();
        }

        private void Click_LeftTop(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You Click Left Top Button!");
        }
        private void Click_RightTop(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You Click Right Top Button!");
        }
        private void Click_LeftDown(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You Click Left Down Button!");
        }
        private void Click_RightDown(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You Click Right Down Button!");
        }
    }
}
