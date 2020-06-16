namespace RVTR.Booking.DataContext{

    public class SearchFilter{
        private int offset;
        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        private int limit;
        public int Limit
        {
            get { return limit; }
            set { limit = value; }
        }
           
        private int myVar;
        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }
        

    }

}