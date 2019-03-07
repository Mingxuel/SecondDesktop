using System.Runtime.Serialization;

namespace SecondDesktopAppManagerDll
{
    [DataContract]
    public class AppItem
    {
        [DataMember]
        public string Icon { get; set; }
		[DataMember]
        public string Name { get; set; }
        [DataMember]
        public string AppUID { get; set; }
        [DataMember]
        public string Config { get; set; }
        [DataMember]
        public AppStatus Status { get; set; }
        [DataMember]
        public int Page { get; set; }
        [DataMember]
        public int RowID { get; set; }
        [DataMember]
        public int ColumnID { get; set; }
    }
}
