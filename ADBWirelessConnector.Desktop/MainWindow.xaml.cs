using ADBWirelessConnector.Core;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace ADBWirelessConnector.Desktop;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged {
    public enum ConnectionStatus {
        None,
        Connecting, 
        Connected,
        Disconnecting,
        Disconnected
    }

    private ADB adb;
    private Dictionary<SettingName, object> data;
    private ConnectionStatus status = ConnectionStatus.Disconnected;
    public ConnectionStatus Status {
        get => status;
        set {
            if (status != value) {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public MainWindow() {
        InitializeComponent();
        DataContext = this;

        data = Settings.LoadData();

        if (data != null) {
            adb = new ADB(data[SettingName.ADB].ToString());
            path.Text = adb.Path;
            address.Text = data[SettingName.Address].ToString();
            port.Text = data[SettingName.Port].ToString();

            if (bool.Parse($"{data[SettingName.Connection]}")) {
                SetActiveState();
            } else
                SetInactiveState();
        } else {
            data = [];
            SetInactiveState();
        }
    }

    private void OnConnect(object sender, RoutedEventArgs e) => Connect();

    private void Connect() {
        Status = ConnectionStatus.Connecting;

        adb.Path = path.Text;

        if (adb.GetVersion() == string.Empty) {
            ThrowError("Your ADB doesn't found. Please check path to your ADB.");
            return;
        }

        if (adb.GetDevices().Length > 1) {
            adb.DisconnectAll();
            Connect();
        }

        if (!adb.Connect(address.Text, port.Text)) {
            ThrowError("Couldn't connect to your device.");
            return;
        } else {
            SetActiveState();
            UpdateData(true);
        }
    }

    private void OnDisconnect(object sender, RoutedEventArgs e) => Disconnect();

    private void Disconnect() {
        Status = ConnectionStatus.Disconnecting;

        adb.Path = path.Text;

        if (adb.GetVersion() == string.Empty) {
            ThrowError("Your ADB doesn't found. Please check path to your ADB.");
            return;
        }

        if (adb.Disconnect(address.Text, port.Text)) {
            SetInactiveState();
            UpdateData(false);
        }
    }

    private void ThrowError(string message) {
        MessageBox.Show(message);
        Status = ConnectionStatus.None;
    }

    private void SetActiveState() {
        Status = ConnectionStatus.Connected;
        path.IsEnabled = false;
        browseButton.IsEnabled = false;
        address.IsEnabled = false;
        port.IsEnabled = false;
        connect.IsEnabled = false;
        disconnect.IsEnabled = true;
    }

    private void SetInactiveState() {
        Status = ConnectionStatus.Disconnected;
        path.IsEnabled = true;
        browseButton.IsEnabled = true;
        address.IsEnabled = true;
        port.IsEnabled = true;
        connect.IsEnabled = true;
        disconnect.IsEnabled = false;
    }

    private void OnBrowse(object sender, RoutedEventArgs e) {
        OpenFileDialog fileFialog = new() {
            DefaultExt = ".exe",
            Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*"
        };

        bool? result = fileFialog.ShowDialog();

        if (result == true) {
            path.Text = fileFialog.FileName;
        }
    }

    private void UpdateData(bool connected) {
        data[SettingName.ADB] = path.Text;
        data[SettingName.Address] = address.Text;
        data[SettingName.Port] = port.Text;
        data[SettingName.Connection] = connected;
        Settings.SaveData(data);
    }

    protected virtual void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}