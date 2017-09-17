using System.Windows;
using System.Windows.Controls;

namespace RearrangingGrid
{
    /// <summary>
    /// Class to allow Visible property of ColumnDefinition to be bindable
    /// </summary>
    public class HidableColumnDefinition : ColumnDefinition
    {
        public static DependencyProperty VisibleProperty;

        public bool Visible { get { return (bool)GetValue(VisibleProperty); } set { SetValue(VisibleProperty, value); } }

        public static readonly DependencyProperty HideWidthProperty =
            DependencyProperty.Register(nameof(HideWidth), typeof(double), typeof(HidableColumnDefinition), new PropertyMetadata(-1D));

        public double HideWidth
        {
            get { return (double)GetValue(HideWidthProperty); }
            set { SetValue(HideWidthProperty, value); }
        }
        
        // Constructors
        static HidableColumnDefinition()
        {
            VisibleProperty = DependencyProperty.Register("Visible", typeof(bool), typeof(HidableColumnDefinition), new PropertyMetadata(true, OnVisibleChanged));
            WidthProperty.OverrideMetadata(typeof(HidableColumnDefinition), new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), null, CoerceWidth));
            MinWidthProperty.OverrideMetadata(typeof(HidableColumnDefinition), new FrameworkPropertyMetadata((double)0, null, CoerceMinWidth));
        }

        public static void SetVisible(DependencyObject obj, bool nVisible)
        {
            obj.SetValue(VisibleProperty, nVisible);
        }
        public static bool GetVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(VisibleProperty);
        }

        static void OnVisibleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            obj.CoerceValue(WidthProperty);
            obj.CoerceValue(MinWidthProperty);
        }
        static object CoerceWidth(DependencyObject obj, object nValue)
        {
            return ((HidableColumnDefinition)obj).Visible ? nValue : new GridLength(0);
        }
        static object CoerceMinWidth(DependencyObject obj, object nValue)
        {
            return ((HidableColumnDefinition)obj).Visible ? nValue : (double)0;
        }
    }
}


