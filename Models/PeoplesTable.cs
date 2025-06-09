namespace Malshinon.models
{
    class PeoplesTable
    {
        public int _id { get; set; }
        public string _firstName { get; set; }
        public string _lastName { get; set; }
        public string _codeName { get; set; }
        public string _type { get; set; }
        public int _num_reports { get; set; }
        public int _num_mentions { get; set; }

        public PeoplesTable(string FN, string LN, string CD, string type, int NR, int NM, int  ID = 0)
        {
            _firstName = FN;
            _lastName = LN;
            _codeName = CD;
            _type = type;
            _num_reports = NR;
            _num_mentions = NM;
            _id = ID;
        } 

        
    }
}