using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;

namespace CSSharpHideAdminEvents
{
    public class CSSharpHideAdminEvents : BasePlugin
    {
        public override string ModuleName => "CS# Hide Admin Events";
        public override string ModuleVersion => "1.0.0";
        public override string ModuleAuthor => "HKS 27D";
        public override string ModuleDescription => "";

        public override void Load(bool hotReload)
        {
            RegisterEventHandler((EventPlayerTeam @event, GameEventInfo info) =>
            {
                CCSPlayerController JoinTeamPlayer = @event.Userid!;

                if (JoinTeamPlayer != null && JoinTeamPlayer.IsValid && JoinTeamPlayer.PlayerPawn.IsValid && !JoinTeamPlayer.IsBot)
                {
                    if (AdminManager.PlayerHasPermissions(JoinTeamPlayer, "@css/generic"))
                        info.DontBroadcast = true;
                }

                return HookResult.Continue;
            }, HookMode.Pre);

            RegisterEventHandler((EventPlayerDisconnect @event, GameEventInfo info) =>
            {
                CCSPlayerController DisconnectedPlayer = @event.Userid!;

                if (DisconnectedPlayer != null && DisconnectedPlayer.IsValid && DisconnectedPlayer.PlayerPawn.IsValid && !DisconnectedPlayer.IsBot)
                {
                    if (AdminManager.PlayerHasPermissions(DisconnectedPlayer, "@css/generic"))
                        info.DontBroadcast = true;
                }

                return HookResult.Continue;
            }, HookMode.Pre);
        }
    }
};
