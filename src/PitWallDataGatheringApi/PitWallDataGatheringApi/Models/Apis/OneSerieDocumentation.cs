using System.Runtime.Serialization;

namespace PitWallDataGatheringApi.Models.Apis
{
    [DataContract]
    public class OneSerieDocumentation
    {
        public OneSerieDocumentation()
        {
        }
        
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public string[] Labels { get; set; }

        [DataMember]
        public string Description { get; internal set; }
    }
}