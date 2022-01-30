namespace SmartSchool.WebAPI.Helpers
{
    public class PaginationParams
    {
        public const int MaxSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize 
        { 
            get { return _pageSize; } 
            set { _pageSize = (value > MaxSize) ? MaxSize : value; }
        }
    }
}