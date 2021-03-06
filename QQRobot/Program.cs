﻿using DumbQQ.Client;
using QQRobot.Service;
using QQRobot.Util;
using System;
using System.Runtime.Loader;

namespace QQRobot
{
    public class Program
    {
        private static QQBotLogger logger;
        private static QQService service;

        public static void Main(string[] args)
        {
            logger = new QQBotLogger(SharedInfo.AppName);
            service = new QQService();

            // 程序退出时关闭客户端
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            Console.CancelKeyPress += CurrentDomain_ProcessExit;
            // 启动服务
            service.StartQQBot();
            // 防止程序终止
            logger.Info("程序正在运行，输入exit退出");
            while (true)
            {
                var line = Console.ReadLine();
                if ("exit" == line)
                {
                    Environment.Exit(1);
                }
            }
        }
        
        private static void CurrentDomain_ProcessExit(Object sender, EventArgs e)
        {
            logger.Debug("Exiting");
            service.CloseQQClient();
        }
    }
}
