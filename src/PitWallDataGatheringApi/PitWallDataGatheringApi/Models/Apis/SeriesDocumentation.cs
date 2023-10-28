using System.Runtime.Serialization;

namespace PitWallDataGatheringApi.Models.Apis
{
    [DataContract]
    public class SeriesDocumentation
    {
        [DataMember]
        public OneSerieDocumentation TyresWear { get; set; }

        [DataMember]
        public OneSerieDocumentation Laptimes { get; set; }

        [DataMember]
        public OneSerieDocumentation TyresTemperatures { get; internal set; }
        public OneSerieDocumentation AvgRoadWetness { get; internal set; }
        public OneSerieDocumentation AirTemperature { get; internal set; }
    }
}