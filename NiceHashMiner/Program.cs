﻿using Newtonsoft.Json;
using NiceHashMiner.Configs;
using NiceHashMiner.Forms;
using NiceHashMiner.PInvoke;
using NiceHashMiner.Utils;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NiceHashMiner.Enums;
using SharpRaven;
using SharpRaven.Data;

namespace NiceHashMiner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        static void ExceptionLogger(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            var ravenClient = new RavenClient("https://8470ed41b73f4cd180852685a415551a@sentry.io/1225545");
            ravenClient.Capture(new SentryEvent(e));
        }

        static void ThreadExceptionLogger(object sender, ThreadExceptionEventArgs args)
        {
            Exception e = args.Exception;
            var ravenClient = new RavenClient("https://8470ed41b73f4cd180852685a415551a@sentry.io/1225545");
            ravenClient.Capture(new SentryEvent(e));
        }

        [STAThread]
        static void Main(string[] argv)
        {
            // log unhandled exceptions
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionLogger);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadExceptionLogger);

            // Set working directory to exe
            var pathSet = false;
            var path = Path.GetDirectoryName(Application.ExecutablePath);
            if (path != null)
            {
                Environment.CurrentDirectory = path;
                pathSet = true;
            }

            // Add common folder to path for launched processes
            var pathVar = Environment.GetEnvironmentVariable("PATH");
            pathVar += ";" + Path.Combine(Environment.CurrentDirectory, "common");
            Environment.SetEnvironmentVariable("PATH", pathVar);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            //Console.OutputEncoding = System.Text.Encoding.Unicode;
            // #0 set this first so data parsing will work correctly
            Globals.JsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Culture = CultureInfo.InvariantCulture
            };

            // #1 first initialize config
            ConfigManager.InitializeConfig();

            // #2 check if multiple instances are allowed
            var startProgram = true;
            if (ConfigManager.GeneralConfig.AllowMultipleInstances == false)
            {
                try
                {
                    var current = Process.GetCurrentProcess();
                    foreach (var process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {
                            startProgram = false;
                        }
                    }
                }
                catch { }
            }

            if (startProgram)
            {
                if (ConfigManager.GeneralConfig.LogToFile)
                {
                    Logger.ConfigureWithFile();
                }

                if (ConfigManager.GeneralConfig.DebugConsole)
                {
                    PInvokeHelpers.AllocConsole();
                }

                // init active display currency after config load
                ExchangeRateApi.ActiveDisplayCurrency = ConfigManager.GeneralConfig.DisplayCurrency;

                // #2 then parse args
                var commandLineArgs = new CommandLineParser(argv);

                Helpers.ConsolePrint("NICEHASH", "Starting up NiceHashMiner v" + Application.ProductVersion);

                if (!pathSet)
                {
                    Helpers.ConsolePrint("NICEHASH", "Path not set to executable");
                }

                /* Preset language to RU and icence aggreement */
                ConfigManager.InitializeConfig();
                ConfigManager.GeneralConfig.agreedWithTOS = Globals.CurrentTosVer;
                ConfigManager.GeneralConfig.SetDefaults();
                ConfigManager.GeneralConfigFileCommit();
                ConfigManager.HideTrayIcon = commandLineArgs.hideTrayIcon;
                ConfigManager.HideEmail = commandLineArgs.hideEmail;
                ConfigManager.TestDriverUpdateForm = commandLineArgs.testDriverUpdateForm;


                var tosChecked = ConfigManager.GeneralConfig.agreedWithTOS == Globals.CurrentTosVer;
                if (!tosChecked || !ConfigManager.GeneralConfigIsFileExist() && !commandLineArgs.IsLang)
                {
                    Helpers.ConsolePrint("NICEHASH",
                        "No config file found. Running NiceHash Miner Legacy for the first time. Choosing a default language.");
                    Application.Run(new Form_ChooseLanguage());
                }

                
                // Init languages
                International.Initialize(ConfigManager.GeneralConfig.Language);

                if (commandLineArgs.IsLang)
                {
                    Helpers.ConsolePrint("NICEHASH", "Language is overwritten by command line parameter (-lang).");
                    International.Initialize(commandLineArgs.LangValue);
                    ConfigManager.GeneralConfig.Language = commandLineArgs.LangValue;
                }

                if (ConfigManager.GeneralConfig.Account == null || ConfigManager.GeneralConfig.WorkerName == "")
                {
                    Application.Run(new Form_Authorization());
                }
                else
                {
                    WebAPI.UpdateMachineInfo(ConfigManager.GeneralConfig.WorkerName);
                }

                // check WMI
                if (ConfigManager.GeneralConfig.Account != null)
                {
                    if (Helpers.IsWmiEnabled())
                    {
                        if (ConfigManager.GeneralConfig.agreedWithTOS == Globals.CurrentTosVer)
                        {
                            while (true) {
                                Application.Run(new Form_Main());
                                if (ConfigManager.GeneralConfig.Account == null)
                                {
                                    Application.Run(new Form_Authorization());
                                }
                                else {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(International.GetText("Program_WMI_Error_Text"),
                            International.GetText("Program_WMI_Error_Title"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Вы должны сначала авторизоваться в системе!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
