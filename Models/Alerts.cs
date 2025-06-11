namespace Malshinon.models
{
    public class Alerts
    {
        public int _id { get; set; }
        public int _targetId { get; set; }
        public DateTime _createdAt { get; set; }
        public string _reason { get; set; }

        public Alerts(int TID, DateTime CAT, string reason, int  ID = 0)
        {
            _targetId = TID;
            _createdAt = CAT;
            _reason = reason;
            _id = ID;
        } 

        
    }
}
