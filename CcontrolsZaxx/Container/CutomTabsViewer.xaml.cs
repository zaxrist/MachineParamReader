using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CcontrolsZaxx.Container
{
    public partial class CutomTabsViewer : UserControl
    {
        private ObservableCollection<TabItem> _TabItemss = new ObservableCollection<TabItem>();
        public ObservableCollection<TabItem> TabItemss
        {
            get { return _TabItemss; }
            set { _TabItemss = value; OnPropertyChanged(); }
        }
        private int _SelIndex;

        public int SelIndex
        {
            get { return _SelIndex; }
            set { _SelIndex = value; OnPropertyChanged(); }
        }

        public Point StartMousePosition { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CutomTabsViewer()
        {
            RenderTransform = new TranslateTransform();
            this.DataContext = this;
            InitializeComponent();
            TabControl1.MouseLeftButtonDown += TabControl1_MouseLeftButtonDown;
            TabControl1.SelectionChanged += TabControl1_SelectionChanged;
        }

        private void TabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsDragging(true);
        }
        int draggedOldIndex = 0;
        private void IsDragging(bool drag)
        {
            if (drag)
            {
                Dragging = drag;
                draggedOldIndex = TabControl1.SelectedIndex;
                Cursor = Cursors.SizeAll;
            }
            else
            {
                Dragging = drag;
                Cursor = Cursors.Arrow;
            }
        }
        private void TabControl1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsDragging(true);
        }
        public void AddTab(string TabName, Control Content)
        {
            if (SelIndex == 0)
            {
                TabItemss.Insert(0, NewTab(TabName, Content));
            }
            else
            {

                TabItemss.Insert(SelIndex + 1, NewTab(TabName, Content));
                SelIndex = SelIndex + 1;
            }
            IsDragging(false);
        }

        public TabItem NewTab(string xTbName, Control Content)
        {
            string TabnewName = GetTabNewNames(xTbName);
            TabItem TBItem = new TabItem();
            TBItem.MouseEnter += TBItem_MouseEnter;
            TBItem.MouseLeftButtonUp += TBItem_MouseLeftButtonUp;

            TBItem.Name = TabnewName;
            StackPanel pn = new StackPanel();
            pn.Name = TabnewName;

            System.Windows.Controls.Label lb = new System.Windows.Controls.Label();
            lb.Content = TabnewName;
            pn.Children.Add(lb);
            pn.MouseEnter += Pn_MouseEnter; pn.MouseLeave += Pn_MouseLeave;

            Button Bt = new Button();
            Bt.Content = "X"; Bt.Background = Brushes.Transparent; Bt.BorderBrush = Brushes.Transparent;
            Bt.Click += Bt_Click; Bt.Name = TabnewName; Bt.Visibility = Visibility.Hidden;
            pn.Orientation = Orientation.Horizontal;
            pn.Children.Add(Bt);

            TBItem.Header = pn;
            TBItem.Content = Content;
            return TBItem;
        }
        private void TBItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsDragging(false);
        }

        private bool Dragging = false;
        private void TBItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                IsDragging(false);
                TabItem tt = sender as TabItem;
                int SelIndex = TabItemss.IndexOf(tt);
                if (SelIndex > TabItemss.Count) return;
                if (SelIndex == draggedOldIndex) { return; }
                TabItemss.Move(draggedOldIndex, TabItemss.IndexOf(tt));
            }
        }

        private void Pn_MouseLeave(object sender, MouseEventArgs e)
        {

            StackPanel ss = sender as StackPanel;
            foreach (Control item in ss.Children)
            {
                if (item.Name == ss.Name)
                {
                    if (ActiveTab == ss.Name) { return; }
                    item.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Pn_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel ss = sender as StackPanel;
            foreach (Control item in ss.Children)
            {
                if (item.Name == ss.Name)
                {
                    item.Visibility = Visibility.Visible;
                }
            }
        }
        private string GetTabNewNames(string IntName)
        {
            int ObjCount = 0;
            bool IsTrue = true;
            while (IsTrue)
            {
                string NewName = IntName + "_" + ObjCount;
                bool Foundd = false;
                foreach (var item in TabItemss)
                {
                    if (NewName == item.Name)
                    {
                        ObjCount++;
                        Foundd = true;
                    }
                }
                if (!Foundd) { IsTrue = false; }
            }

            return IntName + "_" + ObjCount;

        }

        private void RemoveTab(string TabName)
        {
            for (int i = 0; i < TabItemss.Count; i++)
            {
                if (TabItemss[i].Name == TabName)
                {
                    TabItemss.Remove(TabItemss[i]);
                }
            }
        }

        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            Button tt = sender as Button;
            RemoveTab(tt.Name);
        }
        public string ActiveTab = "";
        private void TabControl1_Selected(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                TabItem ss = item as TabItem;
                //ActiveTab = ss.Name;
                HideAllx(ss.Name);
            }
        }
        private void HideAllx(string NewtabName)
        {
            if (ActiveTab != "")
            {
                foreach (var item in TabItemss)
                {
                    if (item.Name == ActiveTab)
                    {
                        StackPanel pp = item.Header as StackPanel;
                        foreach (Control tt in pp.Children)
                        {
                            if (ActiveTab == tt.Name)
                            {
                                tt.Visibility = Visibility.Hidden;
                            }
                        }
                    }
                }
                ActiveTab = "";
            }
            ActiveTab = NewtabName;
        }

        private void TabControl1_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Debug.Print(SelIndex.ToString());
        }

        private void TabControl1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsDragging(false);
        }
    }
}