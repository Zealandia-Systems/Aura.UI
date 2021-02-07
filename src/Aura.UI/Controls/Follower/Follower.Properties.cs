using Avalonia;
using Avalonia.Controls;
using Avalonia.Metadata;

namespace Aura.UI.Controls
{
    public partial class Follower
    {
        /// <summary>
        /// This is the control that's going to follow the pointer
        /// </summary>
        [Content]
        public Control FollowerControl
        {
            get => GetValue(FollowerControlProperty);
            set => SetValue(FollowerControlProperty, value);
        }

        public readonly static StyledProperty<Control> FollowerControlProperty =
            AvaloniaProperty.Register<Follower, Control>(nameof(FollowerControl));

        private Control _layercontrol = new Control();

        /// <summary>
        /// Gets or sets the control what will be inherited its "PointerOver" Event
        /// </summary>
        public Control LayerControl
        {
            get => _layercontrol;
            set => SetAndRaise(LayerControlProperty, ref _layercontrol, value);
        }

        /// <summary>
        /// Defines the <see cref="LayerControl"/> property.
        /// </summary>
        public readonly static DirectProperty<Follower, Control> LayerControlProperty =
            AvaloniaProperty.RegisterDirect<Follower, Control>(
                nameof(LayerControl),
                o => o.LayerControl,
                (o, v) => o.LayerControl = v);

        private Canvas _canvaslayer = new Canvas();

        /// <summary>
        /// Gets or sets the Canvas Layer
        /// </summary>
        public Canvas CanvasLayer
        {
            get => _canvaslayer;
            set => SetAndRaise(CanvasLayerProperty, ref _canvaslayer, value);
        }

        /// <summary>
        /// Defines the <see cref="CanvasLayer"/> property.
        /// </summary>
        public readonly static DirectProperty<Follower, Canvas> CanvasLayerProperty =
            AvaloniaProperty.RegisterDirect<Follower, Canvas>(
                nameof(CanvasLayer),
                o => o.CanvasLayer,
                (o, v) => o.CanvasLayer = v);
    }
}