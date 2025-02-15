﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Interactivity;
using System;
using System.Diagnostics;

namespace Aura.UI.Controls.Navigation
{
    [PseudoClasses(":opened", ":closed", ":selected")]
    public partial class NavigationViewItemBase : TreeViewItem, IHeadered
    {
        private object _content = "Content";

        static NavigationViewItemBase()
        {
            IsExpandedProperty.Changed.AddClassHandler<NavigationViewItemBase>(
                (x, e) =>
                {
                    if (x.IsExpanded == true)
                    {
                        var o_e = new RoutedEventArgs(OpenedEvent);
                        x.RaiseEvent(o_e);
                    }
                    else if (x.IsExpanded == false)
                    {
                        var c_e = new RoutedEventArgs(ClosedEvent);
                        x.RaiseEvent(c_e);
                    }
                });
            OpenedEvent.AddClassHandler<NavigationViewItemBase>((x, e) => x.OnOpened(x, e));
            ClosedEvent.AddClassHandler<NavigationViewItemBase>((x, e) => x.OnClosed(x, e));
            IsSelectedProperty.Changed.AddClassHandler<NavigationViewItemBase>
                ((x, e) =>
                {
                    if (x.IsSelected)
                    {
                        x.OnSelected(x, e);
                    }
                    else if (!x.IsSelected)
                    {
                        x.OnDeselected(x, e);
                    }
                });
            IsOpenProperty.Changed.Subscribe(e => OnIsOpenChanged(e));
        }

        protected virtual void OnDeselected(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            // Debug.WriteLine("I'm deselected");
        }

        protected virtual void OnSelected(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            // Debug.WriteLine("I'm selected");
        }

        protected virtual void OnOpened(object sender, RoutedEventArgs e)
        {
            PseudoClasses.Remove(":closed");
            PseudoClasses.Add(":opened");
        }

        private static void OnIsOpenChanged(AvaloniaPropertyChangedEventArgs<bool> e)
        {
            var sender = e.Sender as NavigationViewItem;
            if (sender != null && e.NewValue.HasValue)
            {
                if (sender.IsSelected && sender.Parent is NavigationViewItem nw && nw.Parent is NavigationView nwp)
                {
                    nwp.SelectSingleItem(nw);
                    nw.IsExpanded = false;
                    // Debug.WriteLine($"yeah & {sender.Header}");
                }
            }
        }

        protected virtual void OnClosed(object sender, RoutedEventArgs e)
        {
            PseudoClasses.Remove(":opened");
            PseudoClasses.Add(":closed");
        }

        public event EventHandler<RoutedEventArgs> Opened
        {
            add => AddHandler(OpenedEvent, value);
            remove => RemoveHandler(OpenedEvent, value);
        }

        public static readonly RoutedEvent<RoutedEventArgs> OpenedEvent =
            RoutedEvent.Register<NavigationViewItemBase, RoutedEventArgs>(nameof(Opened), RoutingStrategies.Bubble);

        public event EventHandler<RoutedEventArgs> Closed
        {
            add => AddHandler(ClosedEvent, value);
            remove => RemoveHandler(ClosedEvent, value);
        }

        public static readonly RoutedEvent<RoutedEventArgs> ClosedEvent =
            RoutedEvent.Register<NavigationViewItemBase, RoutedEventArgs>(nameof(Closed), RoutingStrategies.Bubble);
    }
}