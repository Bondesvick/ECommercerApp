namespace E_CommerceApp
{
    public static class GlobalConfig
    {
        private static readonly IDataOperations Initiate = new DataOperations();
        private static readonly IPagination Pagination = new Pagination();

        /// <summary>
        /// Injects an instance of DataOperations
        /// </summary>
        /// <returns>The instance</returns>
        public static IDataOperations Inject()
        {
            //Initiate.LoadData();
            return Initiate;
        }

        /// <summary>
        /// Injects an instance of Pagination
        /// </summary>
        /// <returns>The instance</returns>
        public static IPagination InjectPaginator()
        {
            return Pagination;
        }
    }
}