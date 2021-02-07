﻿using Aura.UI.Controls.Generators;
using Aura.UI.Controls.Primitives;
using Aura.UI.UIExtensions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using System.Collections;
using System.Linq;

namespace Aura.UI.Controls
{
    /// <summary>
    /// A powered-up TabControl
    /// </summary>
    public partial class AuraTabView : TabViewBase, IHeadered, IFootered
    {
        private Button AdderButton;
        internal double lastselectindex = 0;
        private Border b_;
        private Grid g_;

        protected void AdderButtonClicked(object sender, RoutedEventArgs e)
        {
            var e_ = new RoutedEventArgs(ClickOnAddingButtonEvent);
            RaiseEvent(e_);
            e_.Handled = true;
        }

        static AuraTabView()
        {
            SelectionModeProperty.OverrideDefaultValue<AuraTabView>(SelectionMode.Single);
        }

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new AuraTabItemContainerGenerator(this);
        }

        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
        {
            base.OnPropertyChanged(change);

            if (SelectedItem == null)
            {
                double d = ((double)ItemCount / 2);
                if (lastselectindex < d & ItemCount != 0)
                {
                    SelectedItem = (Items as IList).OfType<object>().FirstOrDefault();
                }
                else if (lastselectindex >= d & ItemCount != 0)
                {
                    SelectedItem = (Items as IList).OfType<object>().LastOrDefault();
                }
            }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            AdderButton = this.GetControl<Button>(e, "PART_AdderButton");

            AdderButton.Click += AdderButtonClicked;

            b_ = this.GetControl<Border>(e, "PART_InternalBorder");
            g_ = this.GetControl<Grid>(e, "PART_InternalGrid");

            PropertyChanged += AuraTabView_PropertyChanged;
        }

        private void AuraTabView_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            WidthRemainingSpace = g_.Bounds.Width;
            HeightRemainingSpace = g_.Bounds.Height;
        }
    }
}