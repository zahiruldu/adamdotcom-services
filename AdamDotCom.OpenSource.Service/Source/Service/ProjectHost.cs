using System.Runtime.Serialization;

namespace AdamDotCom.OpenSource.Service
{
    [DataContract]
    public enum ProjectHost
    {
        [EnumMember] Unknown = 0,
        [EnumMember] GoogleCode = 1,
        [EnumMember] GitHub = 2
    }
}