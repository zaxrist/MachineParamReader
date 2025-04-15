using CcontrolsZaxx.Shared;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CcontrolsZaxx.Buttons
{
    public partial class ButtonFlat : UserControl, INotifyPropertyChanged
    {
        public ButtonFlat()
        {
            this.DataContext = this;
            InitializeComponent();
            MouseHoverOpacity = 0;
            TextColor = ThemeColor.OutFocusTextColor;
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private double _MouseHoverOpacity = 0;

        public double MouseHoverOpacity
        {
            get { return _MouseHoverOpacity; }
            set { _MouseHoverOpacity = value; OnPropertyChanged(); }
        }
        private SolidColorBrush _TextColor;

        public SolidColorBrush TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; OnPropertyChanged(); }
        }
        private string _Textt;

        public string Textt
        {
            get { return _Textt; }
            set { _Textt = value; OnPropertyChanged(); }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseHoverOpacity = 0.03;
            TextColor = ThemeColor.InFocusTextColor;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
           MouseHoverOpacity = 0;
           TextColor = ThemeColor.OutFocusTextColor;
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MouseHoverOpacity = 0;
            TextColor = ThemeColor.MouseLeftPressedColor;
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MouseHoverOpacity = 0.03;
            TextColor = ThemeColor.InFocusTextColor;
        }
        private string _ToolTipHeaderText="";

        public string ToolTipHeaderText
        {
            get { if (ToolTipDescText == "") { TpDescTextVisibility = Visibility.Collapsed; }  if (_ToolTipHeaderText == "") { return Textt; } else { return _ToolTipHeaderText; } }
            set { _ToolTipHeaderText = value; OnPropertyChanged(); }
        }
        private string ToolTipDescText_ = "";

        public string ToolTipDescText
        {
            get { return ToolTipDescText_; }
            set { ToolTipDescText_ = value; OnPropertyChanged(); }
        }
        private Visibility ToolTipVisiblity_;

        public Visibility ToolTipVisiblity
        {
            get { return ToolTipVisiblity_; }
            set { ToolTipVisiblity_ = value; OnPropertyChanged(); }
        }

        public bool ShowToolTip { set{ if (value) { ToolTipVisiblity = Visibility.Visible; } else { ToolTipVisiblity = Visibility.Collapsed; OnPropertyChanged(); } } 
        }
        private Visibility TpDescTextVisibility_ = Visibility.Visible;

        public Visibility TpDescTextVisibility
        {
            get { return TpDescTextVisibility_; }
            set { TpDescTextVisibility_ = value; OnPropertyChanged(); }
        }
    }
}
