using BF1ServerTools.SDK.Core;
using BF1ServerTools.SDK.Data;

namespace BF1ServerTools.SDK;

public static class Player
{
    /// <summary>
    /// 服务器最大玩家数量
    /// </summary>
    private const int MaxPlayer = 74;

    /// <summary>
    /// 获取自己信息
    /// </summary>
    /// <returns></returns>
    public static LocalData GetLocalPlayer()
    {
        var _baseAddress = Obfuscation.GetLocalPlayer();
        return new LocalData()
        {
            DisplayName = Memory.ReadString(_baseAddress + 0x40, 64),
            PersonaId = Memory.Read<long>(_baseAddress + 0x38),
            FullName = Memory.ReadString(_baseAddress + 0x2156, 64)
        };
    }

    /// <summary>
    /// 获取玩家列表缓存
    /// </summary>
    /// <returns></returns>
    public static List<CacheData> GetPlayerCache()
    {
        List<CacheData> _playerCache = new();

        for (int i = 0; i < MaxPlayer; i++)
        {
            var _baseAddress = Obfuscation.GetPlayerById(i);
            if (!Memory.IsValid(_baseAddress))
                continue;

            var _personaId = Memory.Read<long>(_baseAddress + 0x38);
            if (_personaId == 0)
                continue;

            var _name = Memory.ReadString(_baseAddress + 0x40, 64);
            var _teamId = Memory.Read<int>(_baseAddress + 0x1C34);
            var _spectator = Memory.Read<byte>(_baseAddress + 0x1C31);

            _playerCache.Add(new()
            {
                TeamId = _teamId,
                Name = _name,
                Spectator = _spectator,
                PersonaId = _personaId
            });
        }

        return _playerCache;
    }

    /// <summary>
    /// 获取玩家列表信息
    /// </summary>
    /// <returns></returns>
    public static List<PlayerData> GetPlayerList()
    {
        List<PlayerData> _playerList = new();
        var _weaponSlot = new string[8] { "", "", "", "", "", "", "", "" };

        //////////////////////////////// 玩家数据 ////////////////////////////////

        for (int i = 0; i < MaxPlayer; i++)
        {
            var _baseAddress = Obfuscation.GetPlayerById(i);
            if (!Memory.IsValid(_baseAddress))
                continue;

            var _personaId = Memory.Read<long>(_baseAddress + 0x38);
            if (_personaId == 0)
                continue;

            var _mark = Memory.Read<byte>(_baseAddress + 0x1D7C);
            var _teamId = Memory.Read<int>(_baseAddress + 0x1C34);
            var _spectator = Memory.Read<byte>(_baseAddress + 0x1C31);
            var _squadId = Memory.Read<int>(_baseAddress + 0x1E50);
            var _clan = Memory.ReadString(_baseAddress + 0x2151, 64);
            var _name = Memory.ReadString(_baseAddress + 0x40, 64);

            var _pClientVehicleEntity = Memory.Read<long>(_baseAddress + 0x1D38);
            if (Memory.IsValid(_pClientVehicleEntity))
            {
                var _pVehicleEntityData = Memory.Read<long>(_pClientVehicleEntity + 0x30);
                _weaponSlot[0] = Memory.ReadString(Memory.Read<long>(_pVehicleEntityData + 0x2F8), 64);

                for (int j = 1; j < 8; j++)
                    _weaponSlot[j] = "";
            }
            else
            {
                var _pClientSoldierEntity = Memory.Read<long>(_baseAddress + 0x1D48);
                var _pClientSoldierWeaponComponent = Memory.Read<long>(_pClientSoldierEntity + 0x698);
                var _m_handler = Memory.Read<long>(_pClientSoldierWeaponComponent + 0x8A8);

                for (int j = 0; j < 8; j++)
                {
                    var offset0 = Memory.Read<long>(_m_handler + j * 0x8);
                    offset0 = Memory.Read<long>(offset0 + 0x4A30);
                    offset0 = Memory.Read<long>(offset0 + 0x20);
                    offset0 = Memory.Read<long>(offset0 + 0x38);
                    offset0 = Memory.Read<long>(offset0 + 0x20);
                    _weaponSlot[j] = Memory.ReadString(offset0, 64);
                }
            }

            var index = _playerList.FindIndex(val => val.PersonaId == _personaId);
            if (index == -1)
            {
                _playerList.Add(new()
                {
                    Mark = _mark,
                    TeamId = _teamId,
                    Spectator = _spectator,
                    Clan = _clan,
                    Name = _name,
                    PersonaId = _personaId,
                    SquadId = _squadId,

                    Rank = 0,
                    Kill = 0,
                    Dead = 0,
                    Score = 0,

                    WeaponS0 = _weaponSlot[0],
                    WeaponS1 = _weaponSlot[1],
                    WeaponS2 = _weaponSlot[2],
                    WeaponS3 = _weaponSlot[3],
                    WeaponS4 = _weaponSlot[4],
                    WeaponS5 = _weaponSlot[5],
                    WeaponS6 = _weaponSlot[6],
                    WeaponS7 = _weaponSlot[7],
                });
            }
        }

        //////////////////////////////// 得分板数据 ////////////////////////////////

        var _pClientScoreBA = Memory.Read<long>(Memory.Bf1ProBaseAddress + 0x39EB8D8);
        _pClientScoreBA = Memory.Read<long>(_pClientScoreBA + 0x68);

        for (int i = 0; i < MaxPlayer; i++)
        {
            _pClientScoreBA = Memory.Read<long>(_pClientScoreBA);
            var _pClientScoreOffset = Memory.Read<long>(_pClientScoreBA + 0x10);
            if (!Memory.IsValid(_pClientScoreOffset))
                continue;

            var _mark = Memory.Read<byte>(_pClientScoreOffset + 0x300);
            var _rank = Memory.Read<int>(_pClientScoreOffset + 0x304);
            var _kill = Memory.Read<int>(_pClientScoreOffset + 0x308);
            var _dead = Memory.Read<int>(_pClientScoreOffset + 0x30C);
            var _score = Memory.Read<int>(_pClientScoreOffset + 0x314);

            var index = _playerList.FindIndex(val => val.Mark == _mark);
            if (index != -1)
            {
                _playerList[index].Rank = _rank;
                _playerList[index].Kill = _kill;
                _playerList[index].Dead = _dead;
                _playerList[index].Score = _score;
            }
        }

        return _playerList;
    }
}
