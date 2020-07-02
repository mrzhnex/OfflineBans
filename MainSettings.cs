using EXILED;

namespace OfflineBans
{
    public class MainSettings : Plugin
    {
        public override string getName => "OfflineBans";
        private SetEvents SetEvents;
        public override void OnEnable()
        {
            SetEvents = new SetEvents();
            Events.RemoteAdminCommandEvent += SetEvents.OnRemoteAdminCommand;
        }

        public override void OnDisable()
        {
            Events.RemoteAdminCommandEvent -= SetEvents.OnRemoteAdminCommand;
        }

        public override void OnReload() { }
    }
}