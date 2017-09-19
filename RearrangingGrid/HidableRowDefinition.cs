using System.Windows;
using System.Windows.Controls;

namespace RearrangingGrid
{
    /// <summary>
    /// Class to allow Visible property of RowDefinition to be bindable
    /// </summary> 
    public class HidableRowDefinition : RowDefinition
    {
        public static DependencyProperty VisibleProperty;

        public bool Visible { get { return (bool)GetValue(VisibleProperty); } set { SetValue(VisibleProperty, value); } }

        static HidableRowDefinition()
        {
            VisibleProperty = DependencyProperty.Register("Visible", typeof(bool), typeof(HidableRowDefinition), new PropertyMetadata(true, OnVisibleChanged));
            HeightProperty.OverrideMetadata(typeof(HidableRowDefinition), new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), null, CoerceHeight));
            MinHeightProperty.OverrideMetadata(typeof(HidableRowDefinition), new FrameworkPropertyMetadata((double)0, null, CoerceMinHeight));
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
            obj.CoerceValue(HeightProperty);
            obj.CoerceValue(MinHeightProperty);
        }

        static object CoerceHeight(DependencyObject obj, object nValue)
        {
            return ((HidableRowDefinition)obj).Visible ? nValue : new GridLength(0);
        }

        static object CoerceMinHeight(DependencyObject obj, object nValue)
        {
            return ((HidableRowDefinition)obj).Visible ? nValue : (double)0;
        }
    }
}
