using System.Windows;
using System.Windows.Media;

namespace Chess.Model
{
    public static class WPFControls
    {
        public static T GetChildOfType<T>(this DependencyObject depObj, int number) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = number; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child, number);
                if (result != null) return result;
            }
            return null;
        }
    }
}
