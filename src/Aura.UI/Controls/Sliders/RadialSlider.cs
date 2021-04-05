﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Aura.UI.Controls
{
    public class RadialSlider : RangeBase
    {
        static RadialSlider()
        {
            MaximumProperty.Changed.Subscribe(CalibrateAngles);
            MinimumProperty.Changed.Subscribe(CalibrateAngles);
            ValueProperty.Changed.Subscribe(CalibrateAngles);

            MaximumProperty.OverrideMetadata<RadialSlider>(new DirectPropertyMetadata<double>(100));
            MinimumProperty.OverrideMetadata<RadialSlider>(new DirectPropertyMetadata<double>(0));
            ValueProperty.OverrideMetadata<RadialSlider>(new DirectPropertyMetadata<double>(25));

            AffectsRender<RadialSlider>(XAngleProperty, YAngleProperty);
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            var p = e.GetCurrentPoint(null);
            var yAngle = Helpers.Maths.DegreesBetweenPointAndCenter(p.Position, Bounds.Center);
            Debug.WriteLine(p.Position.ToString() + " :current position");
            Debug.WriteLine(Bounds.Center.ToString() + " :center");
            Debug.WriteLine(yAngle.ToString() + " :degrees");
            Value = Helpers.Maths.ValueFromMinMaxAngle(yAngle, Minimum, Maximum, Value);
            Debug.WriteLine(Value.ToString() + " :value");
            Debug.WriteLine("===================================================");
        }

        private static void CalibrateAngles(AvaloniaPropertyChangedEventArgs<double> e)
        {
            var pr = e.Sender as RadialSlider;

            if (pr != null)
            {
                pr.XAngle = -90;
                pr.YAngle = Helpers.Maths.Calibrate(pr.Value, pr.Minimum, pr.Maximum, pr.Value) * 180;
            }
        }

        public int StrokeWidth
        {
            get => GetValue(StrokeWidthProperty);
            set => SetValue(StrokeWidthProperty, value);
        }

        public static readonly StyledProperty<int> StrokeWidthProperty =
            AvaloniaProperty.Register<ProgressRing, int>(nameof(StrokeWidth), 20);

        public Color ForegroundColor
        {
            get => GetValue(ForegroundColorProperty);
            set => SetValue(ForegroundColorProperty, value);
        }

        public readonly static StyledProperty<Color> ForegroundColorProperty =
            AvaloniaProperty.Register<ProgressRing, Color>(nameof(ForegroundColor));

        public Color BackgroundColor
        {
            get => GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public readonly static StyledProperty<Color> BackgroundColorProperty =
            AvaloniaProperty.Register<ProgressRing, Color>(nameof(BackgroundColor));

        private double x_angle;

        public double XAngle
        {
            get => x_angle;
            private set => SetAndRaise(XAngleProperty, ref x_angle, value);
        }

        private readonly static DirectProperty<ProgressRing, double> XAngleProperty =
            AvaloniaProperty.RegisterDirect<ProgressRing, double>(nameof(XAngle), o => o.XAngle);

        private double y_angle;

        public double YAngle
        {
            get => y_angle;
            private set => SetAndRaise(YAngleProperty, ref y_angle, value);
        }

        private readonly static DirectProperty<ProgressRing, double> YAngleProperty =
            AvaloniaProperty.RegisterDirect<ProgressRing, double>(nameof(YAngle), o => o.YAngle);
    }
}
