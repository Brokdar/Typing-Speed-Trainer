using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Typing_Speed_Trainer
{
    internal class CenterBorderGapMaskConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var type = typeof(double);
            if (values == null
                || values.Length != 3
                || values[0] == null
                || values[1] == null
                || values[2] == null
                || !type.IsInstanceOfType(values[0])
                || !type.IsInstanceOfType(values[1])
                || !type.IsInstanceOfType(values[2]))
            {
                return DependencyProperty.UnsetValue;
            }

            var pixels = (double)values[0];
            var width = (double)values[1];
            var height = (double)values[2];
            if ((width == 0.0) || (height == 0.0))
            {
                return null;
            }
            var visual = new Grid
            {
                Width = width,
                Height = height
            };
            var colDefinition1 = new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Star) };
            var colDefinition2 = new ColumnDefinition { Width = new GridLength(pixels) };
            var colDefinition3 = new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Star) };

            visual.ColumnDefinitions.Add(colDefinition1);
            visual.ColumnDefinitions.Add(colDefinition2);
            visual.ColumnDefinitions.Add(colDefinition3);

            var rowDefinition1 = new RowDefinition { Height = new GridLength(height / 2.0) };
            var rowDefinition2 = new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) };

            visual.RowDefinitions.Add(rowDefinition1);
            visual.RowDefinitions.Add(rowDefinition2);

            var rectangle1 = new Rectangle { Fill = Brushes.Black };
            var rectangle2 = new Rectangle { Fill = Brushes.Black };
            var rectangle3 = new Rectangle { Fill = Brushes.Black };

            Grid.SetRowSpan(rectangle1, 2);
            Grid.SetRow(rectangle1, 0);
            Grid.SetColumn(rectangle1, 0);
            Grid.SetRow(rectangle2, 1);
            Grid.SetColumn(rectangle2, 1);
            Grid.SetRowSpan(rectangle3, 2);
            Grid.SetRow(rectangle3, 0);
            Grid.SetColumn(rectangle3, 2);

            visual.Children.Add(rectangle1);
            visual.Children.Add(rectangle2);
            visual.Children.Add(rectangle3);

            return new VisualBrush(visual);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new[] { Binding.DoNothing };
        }
    }
}
