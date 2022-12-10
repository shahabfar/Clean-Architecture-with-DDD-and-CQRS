namespace Product.Application.Paginations
{
    public abstract class Pagination
    {
        protected Pagination()
        {
            PageSize = int.MaxValue;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
