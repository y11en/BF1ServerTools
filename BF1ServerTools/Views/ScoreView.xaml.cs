using BF1ServerTools.SDK;
using BF1ServerTools.SDK.Data;
using BF1ServerTools.RES;
using BF1ServerTools.Data;
using BF1ServerTools.Utils;
using BF1ServerTools.Extensions;

namespace BF1ServerTools.Views;

/// <summary>
/// ScoreView.xaml 的交互逻辑
/// </summary>
public partial class ScoreView : UserControl
{
    private List<PlayerData> PlayerList_Team01 = new();
    private List<PlayerData> PlayerList_Team02 = new();

    private List<PlayerData> PlayerList_Team1 = new();
    private List<PlayerData> PlayerList_Team2 = new();

    ///////////////////////////////////////////////////////

    /// <summary>
    /// 绑定UI队伍1动态数据集合，用于更新ListView
    /// </summary>
    public ObservableCollection<PlayerDataModel> ListView_PlayerList_Team1 { get; set; } = new();
    /// <summary>
    /// 绑定UI队伍2动态数据集合，用于更新ListView
    /// </summary>
    public ObservableCollection<PlayerDataModel> ListView_PlayerList_Team2 { get; set; } = new();

    /// <summary>
    /// 绑定UI队伍0动态数据集合，用于更新观战列表
    /// </summary>
    public ObservableCollection<SpectatorInfo> ListBox_PlayerList_Team01 { get; set; } = new();

    /// <summary>
    /// 绑定UI队伍0动态数据集合，用于更新载入中列表
    /// </summary>
    public ObservableCollection<SpectatorInfo> ListBox_PlayerList_Team02 { get; set; } = new();

    ///////////////////////////////////////////////////////

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
            //////////////////////////////// 数据初始化 ////////////////////////////////

            PlayerList_Team01.Clear();
            PlayerList_Team02.Clear();
            PlayerList_Team1.Clear();
            PlayerList_Team2.Clear();

            //////////////////////////////// 玩家数据 ////////////////////////////////

            foreach (var item in Player.GetPlayerList())
            {
                item.SquadId2 = ClientUtil.GetSquadChsName(item.SquadId);

                item.LifeKd = PlayerUtil.GetLifeKD(item.PersonaId);
                item.LifeKpm = PlayerUtil.GetLifeKPM(item.PersonaId);

                item.Admin = PlayerUtil.IsAdminVIPWhite(item.PersonaId, Globals.ServerAdmins_PID);
                item.Vip = PlayerUtil.IsAdminVIPWhite(item.PersonaId, Globals.ServerVIPs_PID);
                item.White = PlayerUtil.IsAdminVIPWhite(item.PersonaId, Globals.CustomWhites_PID);

                item.Kit = ClientUtil.GetPlayerKitImage(item.WeaponS0, item.WeaponS2, item.WeaponS5);
                item.Kit2 = ClientUtil.GetPlayerKitName(item.WeaponS0, item.WeaponS2, item.WeaponS5);

                switch (item.TeamId)
                {
                    case 0:
                        if (item.Spectator == 0x01)
                            PlayerList_Team01.Add(item);
                        else if (Globals.GameId != string.Empty)
                            PlayerList_Team02.Add(item);
                        break;
                    case 1:
                        PlayerList_Team1.Add(item);
                        break;
                    case 2:
                        PlayerList_Team2.Add(item);
                        break;
                }
            }

            // 显示队伍1中文武器名称
            for (int i = 0; i < PlayerList_Team1.Count; i++)
            {
                PlayerList_Team1[i].WeaponS0 = ClientUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS0);
                PlayerList_Team1[i].WeaponS1 = ClientUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS1);
                PlayerList_Team1[i].WeaponS2 = ClientUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS2);
                PlayerList_Team1[i].WeaponS3 = ClientUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS3);
                PlayerList_Team1[i].WeaponS4 = ClientUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS4);
                PlayerList_Team1[i].WeaponS5 = ClientUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS5);
                PlayerList_Team1[i].WeaponS6 = ClientUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS6);
                PlayerList_Team1[i].WeaponS7 = ClientUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS7);
            }
            // 显示队伍2中文武器名称
            for (int i = 0; i < PlayerList_Team2.Count; i++)
            {
                PlayerList_Team2[i].WeaponS0 = ClientUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS0);
                PlayerList_Team2[i].WeaponS1 = ClientUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS1);
                PlayerList_Team2[i].WeaponS2 = ClientUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS2);
                PlayerList_Team2[i].WeaponS3 = ClientUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS3);
                PlayerList_Team2[i].WeaponS4 = ClientUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS4);
                PlayerList_Team2[i].WeaponS5 = ClientUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS5);
                PlayerList_Team2[i].WeaponS6 = ClientUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS6);
                PlayerList_Team2[i].WeaponS7 = ClientUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS7);
            }

            ////////////////////////////////////////////////////////////////////////////////

            this.Dispatcher.BeginInvoke(() =>
            {
                UpdateListViewTeam1();
            });

            this.Dispatcher.BeginInvoke(() =>
            {
                UpdateListViewTeam2();
            });

            this.Dispatcher.BeginInvoke(() =>
            {
                UpdateListBoxTeam01();
            });

            this.Dispatcher.BeginInvoke(() =>
            {
                UpdateListBoxTeam02();
            });

            /////////////////////////////////////////////////////////////////////////

            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// 动态更新 ListView 队伍1
    /// </summary>
    private void UpdateListViewTeam1()
    {
        if (PlayerList_Team1.Count == 0 && ListView_PlayerList_Team1.Count != 0)
        {
            ListView_PlayerList_Team1.Clear();
        }

        if (PlayerList_Team1.Count != 0)
        {
            // 更新ListView中现有的玩家数据，并把ListView中已经不在服务器的玩家清除
            for (int i = 0; i < ListView_PlayerList_Team1.Count; i++)
            {
                int index = PlayerList_Team1.FindIndex(val => val.PersonaId == ListView_PlayerList_Team1[i].PersonaId);
                if (index != -1)
                {
                    ListView_PlayerList_Team1[i].Rank = PlayerList_Team1[index].Rank;
                    ListView_PlayerList_Team1[i].Clan = PlayerList_Team1[index].Clan;
                    ListView_PlayerList_Team1[i].Admin = PlayerList_Team1[index].Admin;
                    ListView_PlayerList_Team1[i].Vip = PlayerList_Team1[index].Vip;
                    ListView_PlayerList_Team1[i].White = PlayerList_Team1[index].White;
                    ListView_PlayerList_Team1[i].SquadId = PlayerList_Team1[index].SquadId;
                    ListView_PlayerList_Team1[i].SquadId2 = PlayerList_Team1[index].SquadId2;
                    ListView_PlayerList_Team1[i].Kill = PlayerList_Team1[index].Kill;
                    ListView_PlayerList_Team1[i].Dead = PlayerList_Team1[index].Dead;
                    ListView_PlayerList_Team1[i].Kd = PlayerList_Team1[index].Kd;
                    ListView_PlayerList_Team1[i].Kpm = PlayerList_Team1[index].Kpm;
                    ListView_PlayerList_Team1[i].LifeKd = PlayerList_Team1[index].LifeKd;
                    ListView_PlayerList_Team1[i].LifeKpm = PlayerList_Team1[index].LifeKpm;
                    ListView_PlayerList_Team1[i].Score = PlayerList_Team1[index].Score;
                    ListView_PlayerList_Team1[i].Kit = PlayerList_Team1[index].Kit;
                    ListView_PlayerList_Team1[i].Kit2 = PlayerList_Team1[index].Kit2;
                    ListView_PlayerList_Team1[i].WeaponS0 = PlayerList_Team1[index].WeaponS0;
                    ListView_PlayerList_Team1[i].WeaponS1 = PlayerList_Team1[index].WeaponS1;
                    ListView_PlayerList_Team1[i].WeaponS2 = PlayerList_Team1[index].WeaponS2;
                    ListView_PlayerList_Team1[i].WeaponS3 = PlayerList_Team1[index].WeaponS3;
                    ListView_PlayerList_Team1[i].WeaponS4 = PlayerList_Team1[index].WeaponS4;
                    ListView_PlayerList_Team1[i].WeaponS5 = PlayerList_Team1[index].WeaponS5;
                    ListView_PlayerList_Team1[i].WeaponS6 = PlayerList_Team1[index].WeaponS6;
                    ListView_PlayerList_Team1[i].WeaponS7 = PlayerList_Team1[index].WeaponS7;
                }
                else
                {
                    ListView_PlayerList_Team1.RemoveAt(i);
                }
            }

            // 增加ListView没有的玩家数据
            for (int i = 0; i < PlayerList_Team1.Count; i++)
            {
                int index = ListView_PlayerList_Team1.ToList().FindIndex(val => val.PersonaId == PlayerList_Team1[i].PersonaId);
                if (index == -1)
                {
                    ListView_PlayerList_Team1.Add(new()
                    {
                        Rank = PlayerList_Team1[i].Rank,
                        Clan = PlayerList_Team1[i].Clan,
                        Name = PlayerList_Team1[i].Name,
                        PersonaId = PlayerList_Team1[i].PersonaId,
                        Admin = PlayerList_Team1[i].Admin,
                        Vip = PlayerList_Team1[i].Vip,
                        White = PlayerList_Team1[i].White,
                        SquadId = PlayerList_Team1[i].SquadId,
                        SquadId2 = PlayerList_Team1[i].SquadId2,
                        Kill = PlayerList_Team1[i].Kill,
                        Dead = PlayerList_Team1[i].Dead,
                        Kd = PlayerList_Team1[i].Kd,
                        Kpm = PlayerList_Team1[i].Kpm,
                        LifeKd = PlayerList_Team1[i].LifeKd,
                        LifeKpm = PlayerList_Team1[i].LifeKpm,
                        Score = PlayerList_Team1[i].Score,
                        Kit = PlayerList_Team1[i].Kit,
                        Kit2 = PlayerList_Team1[i].Kit2,
                        WeaponS0 = PlayerList_Team1[i].WeaponS0,
                        WeaponS1 = PlayerList_Team1[i].WeaponS1,
                        WeaponS2 = PlayerList_Team1[i].WeaponS2,
                        WeaponS3 = PlayerList_Team1[i].WeaponS3,
                        WeaponS4 = PlayerList_Team1[i].WeaponS4,
                        WeaponS5 = PlayerList_Team1[i].WeaponS5,
                        WeaponS6 = PlayerList_Team1[i].WeaponS6,
                        WeaponS7 = PlayerList_Team1[i].WeaponS7
                    });

                }
            }

            // 排序
            ListView_PlayerList_Team1.Sort();

            // 修正序号
            for (int i = 0; i < ListView_PlayerList_Team1.Count; i++)
            {
                ListView_PlayerList_Team1[i].Index = i + 1;
            }
        }
    }

    /// <summary>
    /// 动态更新 ListView 队伍2
    /// </summary>
    private void UpdateListViewTeam2()
    {
        if (PlayerList_Team2.Count == 0 && ListView_PlayerList_Team2.Count != 0)
        {
            ListView_PlayerList_Team2.Clear();
        }

        if (PlayerList_Team2.Count != 0)
        {
            // 更新ListView中现有的玩家数据，并把ListView中已经不在服务器的玩家清除
            for (int i = 0; i < ListView_PlayerList_Team2.Count; i++)
            {
                int index = PlayerList_Team2.FindIndex(val => val.PersonaId == ListView_PlayerList_Team2[i].PersonaId);
                if (index != -1)
                {
                    ListView_PlayerList_Team2[i].Rank = PlayerList_Team2[index].Rank;
                    ListView_PlayerList_Team2[i].Clan = PlayerList_Team2[index].Clan;
                    ListView_PlayerList_Team2[i].Admin = PlayerList_Team2[index].Admin;
                    ListView_PlayerList_Team2[i].Vip = PlayerList_Team2[index].Vip;
                    ListView_PlayerList_Team2[i].White = PlayerList_Team2[index].White;
                    ListView_PlayerList_Team2[i].SquadId = PlayerList_Team2[index].SquadId;
                    ListView_PlayerList_Team2[i].SquadId2 = PlayerList_Team2[index].SquadId2;
                    ListView_PlayerList_Team2[i].Kill = PlayerList_Team2[index].Kill;
                    ListView_PlayerList_Team2[i].Dead = PlayerList_Team2[index].Dead;
                    ListView_PlayerList_Team2[i].Kd = PlayerList_Team2[index].Kd;
                    ListView_PlayerList_Team2[i].Kpm = PlayerList_Team2[index].Kpm;
                    ListView_PlayerList_Team2[i].LifeKd = PlayerList_Team2[index].LifeKd;
                    ListView_PlayerList_Team2[i].LifeKpm = PlayerList_Team2[index].LifeKpm;
                    ListView_PlayerList_Team2[i].Score = PlayerList_Team2[index].Score;
                    ListView_PlayerList_Team2[i].Kit = PlayerList_Team2[index].Kit;
                    ListView_PlayerList_Team2[i].Kit2 = PlayerList_Team2[index].Kit2;
                    ListView_PlayerList_Team2[i].WeaponS0 = PlayerList_Team2[index].WeaponS0;
                    ListView_PlayerList_Team2[i].WeaponS1 = PlayerList_Team2[index].WeaponS1;
                    ListView_PlayerList_Team2[i].WeaponS2 = PlayerList_Team2[index].WeaponS2;
                    ListView_PlayerList_Team2[i].WeaponS3 = PlayerList_Team2[index].WeaponS3;
                    ListView_PlayerList_Team2[i].WeaponS4 = PlayerList_Team2[index].WeaponS4;
                    ListView_PlayerList_Team2[i].WeaponS5 = PlayerList_Team2[index].WeaponS5;
                    ListView_PlayerList_Team2[i].WeaponS6 = PlayerList_Team2[index].WeaponS6;
                    ListView_PlayerList_Team2[i].WeaponS7 = PlayerList_Team2[index].WeaponS7;
                }
                else
                {
                    ListView_PlayerList_Team2.RemoveAt(i);
                }
            }

            // 增加ListView没有的玩家数据
            for (int i = 0; i < PlayerList_Team2.Count; i++)
            {
                int index = ListView_PlayerList_Team2.ToList().FindIndex(val => val.PersonaId == PlayerList_Team2[i].PersonaId);
                if (index == -1)
                {
                    ListView_PlayerList_Team2.Add(new()
                    {
                        Rank = PlayerList_Team2[i].Rank,
                        Clan = PlayerList_Team2[i].Clan,
                        Name = PlayerList_Team2[i].Name,
                        PersonaId = PlayerList_Team2[i].PersonaId,
                        Admin = PlayerList_Team2[i].Admin,
                        Vip = PlayerList_Team2[i].Vip,
                        White = PlayerList_Team2[i].White,
                        SquadId = PlayerList_Team2[i].SquadId,
                        SquadId2 = PlayerList_Team2[i].SquadId2,
                        Kill = PlayerList_Team2[i].Kill,
                        Dead = PlayerList_Team2[i].Dead,
                        Kd = PlayerList_Team2[i].Kd,
                        Kpm = PlayerList_Team2[i].Kpm,
                        LifeKd = PlayerList_Team2[i].LifeKd,
                        LifeKpm = PlayerList_Team2[i].LifeKpm,
                        Score = PlayerList_Team2[i].Score,
                        Kit = PlayerList_Team2[i].Kit,
                        Kit2 = PlayerList_Team2[i].Kit2,
                        WeaponS0 = PlayerList_Team2[i].WeaponS0,
                        WeaponS1 = PlayerList_Team2[i].WeaponS1,
                        WeaponS2 = PlayerList_Team2[i].WeaponS2,
                        WeaponS3 = PlayerList_Team2[i].WeaponS3,
                        WeaponS4 = PlayerList_Team2[i].WeaponS4,
                        WeaponS5 = PlayerList_Team2[i].WeaponS5,
                        WeaponS6 = PlayerList_Team2[i].WeaponS6,
                        WeaponS7 = PlayerList_Team2[i].WeaponS7
                    });
                }
            }

            // 排序
            ListView_PlayerList_Team2.Sort();

            // 修正序号
            for (int i = 0; i < ListView_PlayerList_Team2.Count; i++)
            {
                ListView_PlayerList_Team2[i].Index = i + 1;
            }
        }
    }

    /// <summary>
    /// 动态更新 ListBox 队伍01
    /// </summary>
    private void UpdateListBoxTeam01()
    {
        if (PlayerList_Team01.Count == 0 && ListBox_PlayerList_Team01.Count != 0)
        {
            ListBox_PlayerList_Team01.Clear();
        }

        if (PlayerList_Team01.Count != 0)
        {
            for (int i = 0; i < ListBox_PlayerList_Team01.Count; i++)
            {
                int index = PlayerList_Team01.FindIndex(val => val.PersonaId == ListBox_PlayerList_Team01[i].PersonaId);
                if (index == -1)
                    ListBox_PlayerList_Team01.RemoveAt(i);
            }

            for (int i = 0; i < PlayerList_Team01.Count; i++)
            {
                int index = ListBox_PlayerList_Team01.ToList().FindIndex(val => val.PersonaId == PlayerList_Team01[i].PersonaId);
                if (index == -1)
                {
                    ListBox_PlayerList_Team01.Add(new()
                    {
                        Name = PlayerList_Team01[i].Name,
                        PersonaId = PlayerList_Team01[i].PersonaId
                    });
                }
            }
        }
    }

    /// <summary>
    /// 动态更新 ListBox 队伍02
    /// </summary>
    private void UpdateListBoxTeam02()
    {
        if (PlayerList_Team02.Count == 0 && ListBox_PlayerList_Team02.Count != 0)
        {
            ListBox_PlayerList_Team02.Clear();
        }

        if (PlayerList_Team02.Count != 0)
        {
            for (int i = 0; i < ListBox_PlayerList_Team02.Count; i++)
            {
                int index = PlayerList_Team02.FindIndex(val => val.PersonaId == ListBox_PlayerList_Team02[i].PersonaId);
                if (index == -1)
                    ListBox_PlayerList_Team02.RemoveAt(i);
            }

            for (int i = 0; i < PlayerList_Team02.Count; i++)
            {
                int index = ListBox_PlayerList_Team02.ToList().FindIndex(val => val.PersonaId == PlayerList_Team02[i].PersonaId);
                if (index == -1)
                {
                    ListBox_PlayerList_Team02.Add(new()
                    {
                        Name = PlayerList_Team02[i].Name,
                        PersonaId = PlayerList_Team02[i].PersonaId
                    });
                }
            }
        }
    }
}
