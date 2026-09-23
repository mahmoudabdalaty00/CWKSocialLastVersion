namespace Data.Specifications.Params.Base
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TID"></typeparam>
    public abstract class SpecParamsBase<TID>
        where TID : struct
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public TID? Id { get; set; }
        public string Sort { get; set; }

        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.Trim().ToLower();
        }
    }
}
