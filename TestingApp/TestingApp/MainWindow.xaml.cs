﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using OmgUtils.Logging;

namespace TestingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var log = new Logger();
            log.Init(".\\log.txt", logTextBlock);

            log.LogInfo("Hello");
            log.LogError("Bye");
            log.LogWarning("Omagad");
            object number = "Hello!";
            var lel = number.GetType();
            Type.GetTypeCode(lel);
            log.LogInfo(lel.ToString());

            var cfg = new OmgUtils.CFGFileParser(log);
            cfg.Parse(".\\test.cfg");
            cfg.SetValue("g_myAnus", "Not cool");
        }
    }
}
