﻿using FurinaImpact.Common.Constants;
using FurinaImpact.Common.Security;
using FurinaImpact.Gameserver.Controllers.Attributes;
using FurinaImpact.Gameserver.Controllers.Result;
using FurinaImpact.Gameserver.Game;
using FurinaImpact.Gameserver.Game.Avatar;
using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Controllers;

[NetController]
internal class AccountController : ControllerBase
{
    [NetCommand(CmdType.GetPlayerTokenReq)]
    public ValueTask<IResult> OnGetPlayerTokenReq()
    {
        return ValueTask.FromResult(Response(CmdType.GetPlayerTokenRsp, new GetPlayerTokenRsp
        {
            ServerRandKey = Convert.ToBase64String(MhySecurity.EncryptWithRSA(new byte[8])),
            Sign = string.Empty, // bypassed
            Uid = 1938,
            CountryCode = "RU",
            PlatformType = 3
        }));
    }

    [NetCommand(CmdType.PingReq)]
    public ValueTask<IResult> OnPingReq()
    {
        return ValueTask.FromResult(Response(CmdType.PingRsp, new PingRsp
        {
            ServerTime = (uint)DateTimeOffset.Now.ToUnixTimeSeconds()
        }));
    }

    [NetCommand(CmdType.PlayerLoginReq)]
    public ValueTask<IResult> OnPlayerLoginReq(Player player)
    {
        player.InitDefaultPlayer();

        AddNotify(CmdType.PlayerDataNotify, new PlayerDataNotify
        {
            NickName = player.Name,
            PropMap =
            {
                {PlayerProp.PROP_PLAYER_LEVEL, new() { Type = PlayerProp.PROP_PLAYER_LEVEL, Ival = 5 } },
                {PlayerProp.PROP_IS_FLYABLE, new() { Type = PlayerProp.PROP_IS_FLYABLE, Ival = 1 } },
                {PlayerProp.PROP_MAX_STAMINA, new() { Type = PlayerProp.PROP_MAX_STAMINA, Ival = 10000 } },
                {PlayerProp.PROP_CUR_PERSIST_STAMINA, new() { Type = PlayerProp.PROP_CUR_PERSIST_STAMINA, Ival = 10000 } },
                {PlayerProp.PROP_IS_TRANSFERABLE, new() { Type = PlayerProp.PROP_IS_TRANSFERABLE, Ival = 1 } },
                {PlayerProp.PROP_IS_SPRING_AUTO_USE, new() { Type = PlayerProp.PROP_IS_SPRING_AUTO_USE, Ival = 1 } },
                {PlayerProp.PROP_SPRING_AUTO_USE_PERCENT, new() { Type = PlayerProp.PROP_SPRING_AUTO_USE_PERCENT, Ival = 50 } }
            }
        });

        AvatarDataNotify avatarDataNotify = new()
        {
            CurAvatarTeamId = 1,
            AvatarTeamMap = { {1, new() } }
        };

        foreach (GameAvatar gameAvatar in player.Avatars)
        {
            avatarDataNotify.AvatarList.Add(gameAvatar.AsAvatarInfo());
        }

        if (player.TryGetAvatar(10000089, out GameAvatar? currentAvatar))
        {
            avatarDataNotify.AvatarTeamMap[1].AvatarGuidList.Add(currentAvatar.Guid);
            avatarDataNotify.ChooseAvatarGuid = 228;
        }

        AddNotify(CmdType.AvatarDataNotify, avatarDataNotify);

        AddNotify(CmdType.OpenStateUpdateNotify, new OpenStateUpdateNotify
        {
            OpenStateMap =
            {
                {1, 1},
                {2, 1},
                {3, 1},
                {4, 1},
                {5, 1},
                {6, 1},
                {7, 0},
                {8, 1},
                {10, 1},
                {11, 1},
                {12, 1},
                {13, 1},
                {14, 1},
                {15, 1},
                {27, 1},
                {28, 1},
                {29, 1},
                {30, 1},
                {31, 1},
                {32, 1},
                {33, 1},
                {37, 1},
                {38, 1},
                {45, 1},
                {47, 1},
                {53, 1},
                {54, 1},
                {55, 1},
                {59, 1},
                {62, 1},
                {65, 1},
                {900, 1},
                {901, 1},
                {902, 1},
                {903, 1},
                {1001, 1},
                {1002, 1},
                {1003, 1},
                {1004, 1},
                {1005, 1},
                {1007, 1},
                {1008, 1},
                {1009, 1},
                {1010, 1},
                {1100, 1},
                {1103, 1},
                {1300, 1},
                {1401, 1},
                {1403, 1},
                {1700, 1},
                {2100, 1},
                {2101, 1},
                {2103, 1},
                {2400, 1},
                {3701, 1},
                {3702, 1},
                {4100, 1 } 
            }
        });

        AddNotify(CmdType.PlayerEnterSceneNotify, new PlayerEnterSceneNotify
        {
            SceneTagIdList = { },
            SceneBeginTime = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds(),
            SceneId = 3,
            SceneTransaction = "3-1337-1699517719-13830",
            Pos = new()
            {
                X = 2191.16357421875f,
                Y = 214.65115356445312f,
                Z = -1120.633056640625f
            },
            TargetUid = 1938,
            UnkUid1020 = 1938,
            EnterSceneToken = SceneController.EnterSceneToken,
            PrevPos = new(),
            Unk13 = 1,
            Unk3 = 1,
            Unk449 = 1,
            Unk834 = 1
        });

        return ValueTask.FromResult(Response(CmdType.PlayerLoginRsp, new PlayerLoginRsp
        {
            CountryCode = "RU",
            GameBiz = "hk4e_global",
            ResVersionConfig = new()
        }));
    }
}
