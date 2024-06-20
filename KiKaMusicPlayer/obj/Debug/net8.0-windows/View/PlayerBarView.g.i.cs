﻿#pragma checksum "..\..\..\..\View\PlayerBarView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DC511A48C6B19C626CD5C008126B31F49D24BDB9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using KiKaMusicPlayer.Utilities;
using KiKaMusicPlayer.View;
using KiKaMusicPlayer.ViewModel;
using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Core;
using Microsoft.Xaml.Behaviors.Input;
using Microsoft.Xaml.Behaviors.Layout;
using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace KiKaMusicPlayer.View {
    
    
    /// <summary>
    /// PlayerBarView
    /// </summary>
    public partial class PlayerBarView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\View\PlayerBarView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image album_cover;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\View\PlayerBarView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock title_text;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\View\PlayerBarView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock artists_text;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\View\PlayerBarView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider player_bar_slider;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\View\PlayerBarView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button play_button;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\View\PlayerBarView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button play_mode_button;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\View\PlayerBarView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock current_time_text;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\..\View\PlayerBarView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock duration_text;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\View\PlayerBarView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button volume_button;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\..\View\PlayerBarView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup volume_pop_up;
        
        #line default
        #line hidden
        
        
        #line 141 "..\..\..\..\View\PlayerBarView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider volume_slider;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/KiKaMusicPlayer;V1.0.0.0;component/view/playerbarview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\PlayerBarView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.album_cover = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.title_text = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.artists_text = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.player_bar_slider = ((System.Windows.Controls.Slider)(target));
            return;
            case 5:
            this.play_button = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.play_mode_button = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.current_time_text = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.duration_text = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.volume_button = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.volume_pop_up = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 11:
            this.volume_slider = ((System.Windows.Controls.Slider)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

