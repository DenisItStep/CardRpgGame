using System.Runtime.Serialization;

namespace CardGameServer
{
    [DataContract]
    public class LastHitInfo
    {
        [DataMember]
        public int slot { get; set; }

        [DataMember]
        public int dmg { get; set; }

        [DataMember]
        public bool IsCritical { get; set; }

        [DataMember]
        public bool isMissed { get; set; }

        public LastHitInfo()
        {
            slot = -1;
            dmg = -1;
            IsCritical = false;
            isMissed = false;
        }
    }
}