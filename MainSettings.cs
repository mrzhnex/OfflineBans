using EXILED;

namespace OfflineBans
{
    public class MainSettings : Plugin
    {
        public override string getName => nameof(OfflineBans);
        public SetEvents SetEvents { get; set; }
        public override void OnEnable()
        {
            SetEvents = new SetEvents();
            Events.RemoteAdminCommandEvent += SetEvents.OnRemoteAdminCommand;
            Log.Info(getName + " on");
        }

        public override void OnDisable()
        {
            Events.RemoteAdminCommandEvent -= SetEvents.OnRemoteAdminCommand;
            Log.Info(getName + " off");
        }

        public override void OnReload() { }
    }
}