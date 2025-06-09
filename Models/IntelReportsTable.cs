namespace Malshinon.models
{
    class IntelReportsTable
    {
        public int _id { get; set; }
        public int _malshinId { get; set; }
        public int _targetId { get; set; }
        public string _stemptime { get; set; }
        public string _reportText { get; set; }
    

        public IntelReportsTable(int MAl, int TAR, string ST, string RT, int ID = 0)
        {
            _malshinId = MAl;
            _targetId = TAR;
            _stemptime = ST;
            _reportText = RT;
            _id = ID;
        } 

        
    }
}