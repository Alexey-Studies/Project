using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Net.WebRequestMethods;

namespace WinFormsAppProject
{
    static class Settings
    {
        private const string REGISTRY_KEY = "HKEY_CURRENT_USER\\Software\\MyApp";

        // Сохранение в регистр windows.
        public static void Save(string key, long value)
        {
            using (RegistryKey fer = Registry.CurrentUser.CreateSubKey("Software\\MyApp"))
            {
                fer.SetValue(key, value);
                fer.Close();
            }
        }

        // Взятие из регистра значение.
        public static string GetSemestrBeginning()
        {
            RegistryKey fer = Registry.CurrentUser.CreateSubKey("Software\\MyApp");
            string tInteger = Registry.GetValue(REGISTRY_KEY, "SemestrBeginning", 0).ToString();

            return tInteger;
        }

        // Конвертация в timestamp.
        public static uint ConvertToTimestamp(string value) => Convert.ToUInt32(
                ((DateTimeOffset)DateTime.Parse(value))
                    .ToUnixTimeSeconds()
            );

        // Конвертация из регистра windows в строчку.
        public static string FromStamp(string args)
        {
            uint DataUint = Convert.ToUInt32(args);
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            if (args == "0")
            {
                dateTime = DateTime.Now;
                return dateTime.ToString();
            }
            dateTime = dateTime.AddSeconds(DataUint).ToLocalTime();

            return dateTime.ToString();
        }

        public static void Init(ref DateTimePicker SemestrBeginning)
        {
            string TempFromRegister = GetSemestrBeginning();
            string StartData = FromStamp(TempFromRegister);
            SemestrBeginning.Text = StartData;
        }
    }
}
