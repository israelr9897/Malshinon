namespace Malshinon.models
{
    public class Report
    {
        public int _id { get; set; }
        public int _malshinId { get; set; }
        public int _targetId { get; set; }
        public DateTime _stemptime { get; set; }
        public string _reportText { get; set; }
    

        public Report(int MAl, int TAR,  string RT, DateTime ST , int ID)
        {
            _malshinId = MAl;
            _targetId = TAR;
            _stemptime = ST;
            _reportText = RT;
            _id = ID;
        } 

        
    }
}