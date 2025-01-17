﻿using BF1ServerTools.SDK;
using BF1ServerTools.SDK.Core;
using BF1ServerTools.Utils;
using BF1ServerTools.Views;
using BF1ServerTools.Models;
using BF1ServerTools.Helper;

using CommunityToolkit.Mvvm.Input;

namespace BF1ServerTools;

/// <summary>
/// MainWindow.xaml 的交互逻辑
/// </summary>
public partial class MainWindow
{
    /// <summary>
    /// 主窗口关闭委托
    /// </summary>
    public delegate void WindowClosingDelegate();
    /// <summary>
    /// 主窗口关闭事件
    /// </summary>
    public static event WindowClosingDelegate WindowClosingEvent;

    /// <summary>
    /// 向外暴露主窗口实例
    /// </summary>
    public static Window MainWindowInstance { get; private set; }

    ///////////////////////////////////////////////////////

    /// <summary>
    /// 数据模型绑定
    /// </summary>
    public MainModel MainModel { get; set; } = new();

    ///////////////////////////////////////////////////////

    private AuthView AuthView { get; set; } = new();
    private ScoreView ScoreView { get; set; } = new();
    private DetailView DetailView { get; set; } = new();
    private RuleView RuleView { get; set; } = new();
    private MonitView MonitView { get; set; } = new();
    private LogView LogView { get; set; } = new();
    private ChatView ChatView { get; set; } = new();
    private RobotView RobotView { get; set; } = new();
    private MoreView MoreView { get; set; } = new();

    ///////////////////////////////////////////////////////

    /// <summary>
    /// 声明一个变量，用于存储软件开始运行的时间
    /// </summary>
    private DateTime Origin_DateTime;

    /// <summary>
    /// 主程序运行标志
    /// </summary>
    public static bool IsAppRunning = true;

    ///////////////////////////////////////////////////////

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Main_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;
        MainWindowInstance = this;

        Navigate("AuthView");

        ////////////////////////////////////////////

        // 客户端程序版本号
        MainModel.VersionInfo = CoreUtil.ClientVersionInfo;
        MainModel.AppRunTime = "loading...";

        MainModel.DisplayName1 = "loading...";
        MainModel.PersonaId1 = 0;

        MainModel.DisplayName2 = "loading...";
        MainModel.PersonaId2 = 0;

        // 获取当前时间，存储到对于变量中
        Origin_DateTime = DateTime.Now;

        ////////////////////////////////////////////

        new Thread(MainWinUpdateThread)
        {
            Name = "MainWinUpdateThread",
            IsBackground = true
        }.Start();
    }

    private void Window_Main_Closing(object sender, CancelEventArgs e)
    {
        // 终止线程内循环
        IsAppRunning = false;

        WindowClosingEvent();
        LoggerHelper.Info("调用主窗口关闭事件成功");

        ProcessUtil.CloseThirdProcess();
        LoggerHelper.Info("关闭第三方进程成功");

        Chat.FreeMemory();
        LoggerHelper.Info("释放中文聊天指针内存成功");

        Memory.UnInitialize();
        LoggerHelper.Info("释放内存模块进程句柄成功");

        SQLiteHelper.UnInitialize();
        LoggerHelper.Info("关闭数据库链接成功");

        Application.Current.Shutdown();
        LoggerHelper.Info("程序关闭\n\n");
    }

    /// <summary>
    /// 主窗口UI更新线程
    /// </summary>
    private void MainWinUpdateThread()
    {
        while (IsAppRunning)
        {
            // 获取软件运行时间
            MainModel.AppRunTime = MiscUtil.ExecDateDiff(Origin_DateTime, DateTime.Now);

            // 是否使用模式1
            MainModel.IsUseMode1 = Globals.IsUseMode1;

            // 模式1玩家信息
            var localData = Player.GetLocalPlayer();
            Globals.DisplayName1 = localData.DisplayName;
            Globals.PersonaId1 = localData.PersonaId;
            MainModel.DisplayName1 = Globals.DisplayName1;
            MainModel.PersonaId1 = Globals.PersonaId1;
            // 模式2玩家信息
            MainModel.DisplayName2 = Globals.DisplayName2;
            MainModel.PersonaId2 = Globals.PersonaId2;

            if (!ProcessUtil.IsBf1Run())
            {
                this.Dispatcher.Invoke(this.Close);
                return;
            }

            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// View页面导航
    /// </summary>
    /// <param name="viewName"></param>
    [RelayCommand]
    private void Navigate(string viewName)
    {
        switch (viewName)
        {
            case "AuthView":
                if (ContentControl_Main.Content != AuthView)
                    ContentControl_Main.Content = AuthView;
                break;
            case "ScoreView":
                if (ContentControl_Main.Content != ScoreView)
                    ContentControl_Main.Content = ScoreView;
                break;
            case "DetailView":
                if (ContentControl_Main.Content != DetailView)
                    ContentControl_Main.Content = DetailView;
                break;
            case "RuleView":
                if (ContentControl_Main.Content != RuleView)
                    ContentControl_Main.Content = RuleView;
                break;
            case "MonitView":
                if (ContentControl_Main.Content != MonitView)
                    ContentControl_Main.Content = MonitView;
                break;
            case "LogView":
                if (ContentControl_Main.Content != LogView)
                    ContentControl_Main.Content = LogView;
                break;
            case "ChatView":
                if (ContentControl_Main.Content != ChatView)
                    ContentControl_Main.Content = ChatView;
                break;
            case "RobotView":
                if (ContentControl_Main.Content != RobotView)
                    ContentControl_Main.Content = RobotView;
                break;
            case "MoreView":
                if (ContentControl_Main.Content != MoreView)
                    ContentControl_Main.Content = MoreView;
                break;
        }
    }
}
