﻿#pragma checksum "..\..\..\..\Container\CutomTabsViewer.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "52AC2239B07A0F85C306A500A48E7DAC275B5ED3E67133A2AAB4DAB938EBB931"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CcontrolsZaxx.Container;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CcontrolsZaxx.Container {
    
    
    /// <summary>
    /// CutomTabsViewer
    /// </summary>
    public partial class CutomTabsViewer : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\Container\CutomTabsViewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Container\CutomTabsViewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.TranslateTransform LocalTranslateTransform;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\Container\CutomTabsViewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl TabControl1;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CcontrolsZaxx;component/container/cutomtabsviewer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Container\CutomTabsViewer.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.LocalTranslateTransform = ((System.Windows.Media.TranslateTransform)(target));
            return;
            case 3:
            this.TabControl1 = ((System.Windows.Controls.TabControl)(target));
            
            #line 21 "..\..\..\..\Container\CutomTabsViewer.xaml"
            this.TabControl1.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.TabControl1_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 22 "..\..\..\..\Container\CutomTabsViewer.xaml"
            this.TabControl1.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TabControl1_Selected);
            
            #line default
            #line hidden
            
            #line 22 "..\..\..\..\Container\CutomTabsViewer.xaml"
            this.TabControl1.MouseRightButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.TabControl1_MouseRightButtonUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

