using System.Diagnostics;

namespace ADBWirelessConnector.Core;
public class ADB {
    public string Path { get; set; }

    public ADB(string path) {
        Path = path;
    }

    public string GetVersion() {
        string version = string.Empty;

        if (Request("version", out string[] output))
            version = output[1];

        return version;
    }

    public string[] GetDevices() {
        List<string> devices = [];

        if (Request("devices", out string[] output) && output.Length > 1) {
            for (int i = 1; i < output.Length; i++) {
                devices.Add(output[i].Trim());
            }
        }

        return [.. devices];
    }

    public bool ContainsDevice(string fullAddress) => GetDevices().Any(d => d.Contains(fullAddress));

    public bool OpenPort(string port) => Request($"tcpip", out _);

    public bool Connect(string address, string port) {
        string fullAddress = $"{address}:{port}";
        _ = Request($"connect {fullAddress}", out _);

        return ContainsDevice(fullAddress);
    }

    public bool Disconnect(string address, string port) => Disconnect($"{address}:{port}");

    private bool Disconnect(string fullAddress) {
        if (!ContainsDevice(fullAddress)) return true;

        _ = Request($"disconnect {fullAddress}", out _);
        return Disconnect(fullAddress);
    }

    public void DisconnectAll() {
        foreach (string device in GetDevices()) {
            string address = device.Split()[0].Trim();
            Disconnect(address);
        }
    }

    public bool Request(string args, out string[] output) {
        using Process process = new();

        process.StartInfo.FileName = Path;
        process.StartInfo.Arguments = args;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardOutput = true;

        try {
            process.Start();
        } catch (Exception ex) {
            Debug.WriteLine($"Exception: {ex.Message}");
            output = [];
            return false;
        }

        output = process.StandardOutput.ReadToEnd().Trim().Split("\n");
        process.WaitForExit();

        return true;
    }
}
