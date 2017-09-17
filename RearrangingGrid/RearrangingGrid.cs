using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RearrangingGrid
{
    public class RearrangingGrid : Grid
    {
#region Dependency Properties
        public static DependencyProperty NarrowRowProperty = DependencyProperty.RegisterAttached("NarrowRow", typeof(int), typeof(RearrangingGrid), new PropertyMetadata(-1));
        public static DependencyProperty NarrowColumnProperty = DependencyProperty.RegisterAttached("NarrowColumn", typeof(int), typeof(RearrangingGrid), new PropertyMetadata(-1));
        public static DependencyProperty NarrowColumnSpanProperty = DependencyProperty.RegisterAttached("NarrowColumnSpan", typeof(int), typeof(RearrangingGrid), new PropertyMetadata(-1));
        public static DependencyProperty NarrowRowSpanProperty = DependencyProperty.RegisterAttached("NarrowRowSpan", typeof(int), typeof(RearrangingGrid), new PropertyMetadata(-1));
        public static readonly DependencyProperty NarrowThresholdProperty = DependencyProperty.Register("NarrowThreshold", typeof(double), typeof(RearrangingGrid), new PropertyMetadata(-1D));

        public static DependencyProperty ShortRowProperty = DependencyProperty.RegisterAttached("ShortRow", typeof(int), typeof(RearrangingGrid), new PropertyMetadata(-1));
        public static DependencyProperty ShortColumnProperty = DependencyProperty.RegisterAttached("ShortColumn", typeof(int), typeof(RearrangingGrid), new PropertyMetadata(-1));
        public static DependencyProperty ShortColumnSpanProperty = DependencyProperty.RegisterAttached("ShortColumnSpan", typeof(int), typeof(RearrangingGrid), new PropertyMetadata(-1));
        public static DependencyProperty ShortRowSpanProperty = DependencyProperty.RegisterAttached("ShortRowSpan", typeof(int), typeof(RearrangingGrid), new PropertyMetadata(-1));
        public static readonly DependencyProperty ShortThresholdProperty = DependencyProperty.Register("ShortThreshold", typeof(double), typeof(RearrangingGrid), new PropertyMetadata(-1D));

        //https://stackoverflow.com/questions/1122595/how-do-you-create-a-read-only-dependency-property
        private static readonly DependencyPropertyKey LayoutModePropertyKey
            = DependencyProperty.RegisterReadOnly("LayoutMode", typeof(LayoutMode), typeof(RearrangingGrid),
                new PropertyMetadata(LayoutMode.Regular));
        public static readonly DependencyProperty ReadOnlyPropProperty = LayoutModePropertyKey.DependencyProperty;

        public double NarrowThreshold
        {
            get { return (double)GetValue(NarrowThresholdProperty); }
            set { SetValue(NarrowThresholdProperty, value); }
        }

        public static int GetNarrowRow(DependencyObject target)
        {
            return (int)target.GetValue(NarrowRowProperty);
        }

        public static void SetNarrowRow(DependencyObject target, int value)
        {
            target.SetValue(NarrowRowProperty, value);
        }

        public static int GetNarrowColumn(DependencyObject target)
        {
            return (int)target.GetValue(NarrowColumnProperty);
        }

        public static void SetNarrowColumn(DependencyObject target, int value)
        {
            target.SetValue(NarrowColumnProperty, value);
        }

        public static int GetNarrowRowSpan(DependencyObject target)
        {
            return (int)target.GetValue(NarrowRowSpanProperty);
        }

        public static void SetNarrowRowSpan(DependencyObject target, int value)
        {
            target.SetValue(NarrowRowSpanProperty, value);
        }

        public static int GetNarrowColumnSpan(DependencyObject target)
        {
            return (int)target.GetValue(NarrowColumnSpanProperty);
        }

        public static void SetNarrowColumnSpan(DependencyObject target, int value)
        {
            target.SetValue(NarrowColumnSpanProperty, value);
        }

        public double ShortThreshold
        {
            get { return (double)GetValue(ShortThresholdProperty); }
            set { SetValue(ShortThresholdProperty, value); }
        }

        public static int GetShortRow(DependencyObject target)
        {
            return (int)target.GetValue(ShortRowProperty);
        }

        public static void SetShortRow(DependencyObject target, int value)
        {
            target.SetValue(ShortRowProperty, value);
        }

        public static int GetShortColumn(DependencyObject target)
        {
            return (int)target.GetValue(ShortColumnProperty);
        }

        public static void SetShortColumn(DependencyObject target, int value)
        {
            target.SetValue(ShortColumnProperty, value);
        }

        public static int GetShortRowSpan(DependencyObject target)
        {
            return (int)target.GetValue(ShortRowSpanProperty);
        }

        public static void SetShortRowSpan(DependencyObject target, int value)
        {
            target.SetValue(ShortRowSpanProperty, value);
        }

        public static int GetShortColumnSpan(DependencyObject target)
        {
            return (int)target.GetValue(ShortColumnSpanProperty);
        }

        public static void SetShortColumnSpan(DependencyObject target, int value)
        {
            target.SetValue(ShortColumnSpanProperty, value);
        }
        #endregion

        private LayoutMode _gridState = LayoutMode.Regular;
        public RearrangingGrid()
        {
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.HeightChanged && IsLoaded)
            {
                var shouldSwitchToShortValues = ActualHeight < ShortThreshold && _gridState == LayoutMode.Regular;
                var shouldSwitchToRegularValues = ActualHeight > ShortThreshold && _gridState == LayoutMode.Short;

                if (shouldSwitchToShortValues || shouldSwitchToRegularValues)
                {
                    _gridState = shouldSwitchToRegularValues ? LayoutMode.Regular : LayoutMode.Short;
                    SetValue(LayoutModePropertyKey, _gridState);

                    if (shouldSwitchToShortValues)
                        SwitchingToShort?.Invoke(this, null);//TODO implement event args
                    else
                        SwitchingToRegular?.Invoke(this, null);

                    var uiElements = Children.Cast<UIElement>().ToList();

                    //Row Changed
                    foreach (var element in uiElements.Where(el => GetShortRow(el) != -1))
                    {
                        var temp = GetRow(element);
                        SetRow(element, GetShortRow(element));
                        SetShortRow(element, temp);
                    }

                    //Column Changed
                    foreach (var element in uiElements.Where(el => GetShortColumn(el) != -1))
                    {
                        var temp = GetColumn(element);
                        SetColumn(element, GetShortColumn(element));
                        SetShortColumn(element, temp);
                    }

                    //ColumnSpan Changed
                    foreach (var element in uiElements.Where(el => GetShortColumnSpan(el) != -1))
                    {
                        var temp = GetColumnSpan(element);
                        SetColumnSpan(element, GetShortColumnSpan(element));
                        SetShortColumnSpan(element, temp);
                    }

                    //RowSpan Changed
                    foreach (var element in uiElements.Where(el => GetShortRowSpan(el) != -1))
                    {
                        var temp = GetRowSpan(element);
                        SetRowSpan(element, GetShortRowSpan(element));
                        SetShortRowSpan(element, temp);
                    }
                }
            }
            else if (e.WidthChanged && IsLoaded)
            {
                var shouldSwitchToNarrowValues = ActualWidth < NarrowThreshold && _gridState == LayoutMode.Regular;
                var shouldSwitchToRegularValues = ActualWidth > NarrowThreshold && _gridState == LayoutMode.Narrow;

                if (shouldSwitchToNarrowValues || shouldSwitchToRegularValues)
                {
                    _gridState = shouldSwitchToRegularValues ? LayoutMode.Regular : LayoutMode.Narrow;
                    SetValue(LayoutModePropertyKey,_gridState);

                    if (shouldSwitchToNarrowValues)
                        SwitchingToNarrow?.Invoke(this, null);//TODO implement event args
                    else
                        SwitchingToRegular?.Invoke(this, null);

                    var uiElements = Children.Cast<UIElement>().ToList();

                    //Row Changed
                    foreach (var element in uiElements.Where(el => GetNarrowRow(el) != -1))
                    {
                        var temp = GetRow(element);
                        SetRow(element, GetNarrowRow(element));
                        SetNarrowRow(element, temp);
                    } 
                    
                    //Column Changed
                    foreach (var element in uiElements.Where(el => GetNarrowColumn(el) != -1))
                    {
                        var temp = GetColumn(element);
                        SetColumn(element, GetNarrowColumn(element));
                        SetNarrowColumn(element, temp);
                    }

                    //ColumnSpan Changed
                    foreach (var element in uiElements.Where(el => GetNarrowColumnSpan(el) != -1))
                    {
                        var temp = GetColumnSpan(element);
                        SetColumnSpan(element, GetNarrowColumnSpan(element));
                        SetNarrowColumnSpan(element, temp);
                    }

                    //RowSpan Changed
                    foreach (var element in uiElements.Where(el => GetNarrowRowSpan(el) != -1))
                    {
                        var temp = GetRowSpan(element);
                        SetRowSpan(element, GetNarrowRowSpan(element));
                        SetNarrowRowSpan(element, temp);
                    }
                }
            }
        }

        public event EventHandler<EventArgs> SwitchingToNarrow;
        public event EventHandler<EventArgs> SwitchingToShort;
        public event EventHandler<EventArgs> SwitchingToRegular;
    }
}
