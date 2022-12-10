using BF1ServerTools.SDK;

namespace BF1ServerTools.Views;

/// <summary>
/// ScoreView.xaml 的交互逻辑
/// </summary>
public partial class ScoreView : UserControl
{
    public ScoreView()
    {
        InitializeComponent();
        this.DataContext = this;
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;

        new Thread(UpdatePlayerListThread)
        {
            Name = "UpdatePlayerListThread",
            IsBackground = true
        }.Start();
    }

    private void MainWindow_WindowClosingEvent()
    {

    }

    /// <summary>
    /// 更新服务器玩家列表线程
    /// </summary>
    private void UpdatePlayerListThread()
    {
        while (MainWindow.IsAppRunning)
        {
            foreach (var item in Player.GetPlayerList())
            {

            }

            /////////////////////////////////////////////////////////////////////////

            Thread.Sleep(1000);
        }
    }
}
