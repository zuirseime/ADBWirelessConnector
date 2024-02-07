using Microsoft.Win32;

namespace ADBWirelessConnector.Core;

public enum SettingName {
    ADB,
    Address,
    Port,
    Connection
}

public static class Settings {
    private static string ProjectName = "WirlessConnector";
    private static string RegistryPath = @"SOFTWARE\zuirseime\";

    public static Dictionary<SettingName, object> LoadData() {
        if (RegistrySubKeyExists()) {
            Dictionary<SettingName, object> data = [];

            SettingName[] values = (SettingName[])Enum.GetValues(typeof(SettingName));
            Array.ForEach(values, value => data[value] = ReadRegistryValue(value.ToString()));

            return data;
        }

        return null;
    }

    public static void SaveData(Dictionary<SettingName, object> data) {
        if (!RegistrySubKeyExists())
            CreateSubKey();

        foreach (var pair in data) {
            WriteRegistryValue(pair.Key, data[pair.Key]);
        }
    }

    private static bool RegistrySubKeyExists() {
        try {
            using RegistryKey key = OpenSubKey();
            return key != null;
        } catch {
            return false;
        }
    }

    private static void CreateSubKey() => _ = Registry.CurrentUser.CreateSubKey(RegistryPath + ProjectName);
    private static RegistryKey OpenSubKey(bool writable = false) => Registry.CurrentUser.OpenSubKey(RegistryPath + ProjectName, writable);

    private static object ReadRegistryValue(string param) {
        using RegistryKey key = OpenSubKey();

        object? value = key.GetValue(param);

        return value != null ? value : string.Empty;
    }

    private static void WriteRegistryValue(SettingName param, object value) {
        using RegistryKey key = OpenSubKey(true);
        key.SetValue(param.ToString(), value);
    }
}