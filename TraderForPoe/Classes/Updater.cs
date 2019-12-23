﻿using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;

namespace TraderForPoe.Classes
{
    internal static class Updater
    {
        /// <summary>
        /// Check if a newer version is available
        /// </summary>
        /// <returns>Returns true if update is available</returns>
        public static bool UpdateIsAvailable()
        {
            var webClient = new WebClient();

            var updateString = webClient.DownloadString("https://raw.githubusercontent.com/labo89/TraderForPoe/master/update").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var thisVersion = Convert.ToDouble(Assembly.GetEntryAssembly()?.GetName().Version.ToString().Substring(0, 3), System.Globalization.CultureInfo.InvariantCulture);

            var onlineVersion = Convert.ToDouble(updateString[0], System.Globalization.CultureInfo.InvariantCulture);

            if (onlineVersion > thisVersion)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void CheckForUpdate()
        {
            if (UpdateIsAvailable())
            {
                if (MessageBox.Show("A new version is available. Do you want to Update? Application will be restarted.", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    StartUpdate();
                }
            }
            else
            {
                MessageBox.Show("No updates available", "No Updates", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            }
        }

        public static void StartUpdate()
        {
            var webClient = new WebClient();

            var updateString = webClient.DownloadString("https://raw.githubusercontent.com/labo89/TraderForPoe/master/update").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var downloadLink = updateString[1];

            var newExePath = Path.GetTempPath() + "TraderForPoe.exe";

            using (var wc = new WebClient())
            {
                wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                wc.DownloadFileAsync(new Uri(downloadLink), newExePath);
            }
        }

        private static void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            File.Delete(Path.GetTempPath() + "TraderForPoe.bak");

            var sourceFileName = Assembly.GetEntryAssembly()?.Location;
            if (sourceFileName != null)
            {
                File.Move(sourceFileName, Path.GetTempPath() + "TraderForPoe.bak");

                File.Move(Path.GetTempPath() + "TraderForPoe.exe",
                    AppDomain.CurrentDomain.BaseDirectory + "TraderForPoe.exe");

                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            }

            Application.Current.Shutdown();
        }
    }
}