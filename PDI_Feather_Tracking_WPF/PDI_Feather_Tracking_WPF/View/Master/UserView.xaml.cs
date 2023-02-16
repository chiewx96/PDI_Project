﻿using PDI_Feather_Tracking_WPF.ViewModel;
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

namespace PDI_Feather_Tracking_WPF.View
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {

        UserViewModel _userViewModel;

        public UserView(UserViewModel userViewModel)
        {
            _userViewModel = userViewModel;
            DataContext = _userViewModel;
            InitializeComponent();
        }
    }
}
