﻿#pragma checksum "..\..\SubwayNavigation.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BAF77832B8B2F3C8CF767B146CF54FD6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using SubwayNavigation;
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


namespace SubwayNavigation {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\SubwayNavigation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbTitle;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\SubwayNavigation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbFrom;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\SubwayNavigation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbTo;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\SubwayNavigation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvasSubway;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\SubwayNavigation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btShowPath;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\SubwayNavigation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbFromStation;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\SubwayNavigation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbToStation;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\SubwayNavigation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvWay;
        
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
            System.Uri resourceLocater = new System.Uri("/SubwayNavigation;component/subwaynavigation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SubwayNavigation.xaml"
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
            this.lbTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.lbFrom = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.lbTo = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.canvasSubway = ((System.Windows.Controls.Canvas)(target));
            
            #line 46 "..\..\SubwayNavigation.xaml"
            this.canvasSubway.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.canvasSubway_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btShowPath = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\SubwayNavigation.xaml"
            this.btShowPath.Click += new System.Windows.RoutedEventHandler(this.btShowPath_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cbFromStation = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.cbToStation = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.lvWay = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

