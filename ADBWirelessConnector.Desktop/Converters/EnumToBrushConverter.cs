using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ADBWirelessConnector.Desktop;

public class EnumToBrushConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value is MainWindow.ConnectionStatus status) {
            switch (status) {
                case MainWindow.ConnectionStatus.Disconnected:
                    return Brushes.Gray;
                case MainWindow.ConnectionStatus.Connecting:
                    return Brushes.GreenYellow;
                case MainWindow.ConnectionStatus.Connected:
                    return Brushes.Green;
                case MainWindow.ConnectionStatus.Disconnecting:
                    return Brushes.Yellow;
                case MainWindow.ConnectionStatus.None:
                    return Brushes.Red;
                default:
                    return DependencyProperty.UnsetValue;
            }
        }
        return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        return null;
    }
}
