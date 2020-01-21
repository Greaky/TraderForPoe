using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TraderForPoe.WPF.Classes
{
    public class SqueezeTabPanel : Panel
    {
        private double _rowHeight;
        private double _scaleFactor;

        // Ensure tabbing works correctly
        static SqueezeTabPanel()
        {
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(SqueezeTabPanel), new FrameworkPropertyMetadata(KeyboardNavigationMode.Once));
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(typeof(SqueezeTabPanel), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
        }

        // This Panel lays its children out horizontally.
        // If all children cannot fit in the allocated space,
        // the available space is divided proportionally between them.
        protected override Size MeasureOverride(Size availableSize)
        {
            // See how much room the children want
            var width = 0.0;
            _rowHeight = 0.0;
            foreach (UIElement element in Children)
            {
                element.Measure(availableSize);
                var size = GetDesiredSizeLessMargin(element);
                _rowHeight = Math.Max(_rowHeight, size.Height);
                width += size.Width;
            }

            // If not enough room, scale the
            // children to the available width
            if (width > availableSize.Width)
            {
                _scaleFactor = availableSize.Width / width;
                width = 0.0;
                foreach (UIElement element in Children)
                {
                    element.Measure(new Size(element.DesiredSize.Width * _scaleFactor, availableSize.Height));
                    width += element.DesiredSize.Width;
                }
            }
            else
                _scaleFactor = 1.0;

            return new Size(width, _rowHeight);
        }

        // Perform arranging of children based on the final size
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var point = new Point();
            foreach (UIElement element in Children)
            {
                var size1 = element.DesiredSize;
                var size2 = GetDesiredSizeLessMargin(element);
                var margin = (Thickness)element.GetValue(MarginProperty);
                var width = size2.Width;
                if (element.DesiredSize.Width != size2.Width)
                    width = arrangeSize.Width - point.X; // Last-tab-selected "fix"
                element.Arrange(new Rect(
                    point,
                    new Size(Math.Min(width, size2.Width), _rowHeight)));
                var leftRightMargin = Math.Max(0.0, -(margin.Left + margin.Right));
                point.X += size1.Width + (leftRightMargin * _scaleFactor);
            }

            return arrangeSize;
        }

        // Return element's size
        // after subtracting margin
        private Size GetDesiredSizeLessMargin(UIElement element)
        {
            var margin = (Thickness)element.GetValue(MarginProperty);
            var size = new Size
            {
                Height = Math.Max(0.0, element.DesiredSize.Height - (margin.Top + margin.Bottom)),
                Width = Math.Max(0.0, element.DesiredSize.Width - (margin.Left + margin.Right))
            };
            return size;
        }
    }
}
