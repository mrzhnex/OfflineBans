using EXILED;
using EXILED.Extensions;
using System;
using System.Linq;

namespace OfflineBans
{
    internal class SetEvents
    {
        private string GetUsageAtBan()
        {
            return "Usage: atban <SteamID64> <Time> <Reason>";
        }

        private string GetUsageUnBan()
        {
            return "Usage: unban <SteamID64>";
        }
        internal void OnRemoteAdminCommand(ref RACommandEvent ev)
        {
            string[] args = ev.Command.Split(' ');

            if (args.Length > 0 && args[0].ToLower() == "atban")
            {
                if (args.Length < 3 || args.Length > 4)
                {
                    ev.Sender.RAMessage("Out of args." + GetUsageAtBan());
                    return;
                }
                if (!int.TryParse(args[2], out int time))
                {
                    ev.Sender.RAMessage("Wrong args." + GetUsageAtBan());
                    return;
                }
                if (time <= 0)
                {
                    ev.Sender.RAMessage("Wrong args." + GetUsageAtBan());
                    return;
                }
                string steamid = args[1];
                string reason = string.Empty;
                if (args.Length == 4)
                    reason = args[3];

                if (Player.GetHubs().Where(x => x.GetUserId().Replace("@steam", string.Empty) == steamid).FirstOrDefault() != default)
                {
                    Player.GetHubs().Where(x => x.GetUserId().Replace("@steam", string.Empty) == steamid).FirstOrDefault().BanPlayer(time, reason, ev.Sender.Nickname);
                    ev.Sender.RAMessage("Success ban " + steamid);
                    return;
                }
                else
                {
                    BanDetails banDetails = new BanDetails()
                    {
                        Expires = DateTime.UtcNow.AddMinutes(time).Ticks,
                        Id = steamid,
                        IssuanceTime = TimeBehaviour.CurrentTimestamp(),
                        Issuer = ev.Sender.Nickname,
                        Reason = reason,
                        OriginalName = string.Empty
                    };
                    BanHandler.IssueBan(banDetails, BanHandler.BanType.UserId);
                    ev.Sender.RAMessage("Success offline ban " + steamid);
                    return;
                }
            }
        }
    }
}