using E_CommerceApp.Format;
using System.Collections.Generic;

namespace E_CommerceApp
{
    public interface IPagination
    {
        public List<Product> PageData { get; set; }

        /// <summary>
        /// Handles the pagination to the next page of data table on the Cart page
        /// </summary>
        /// <param name="range">Range of amount of rows to show</param>
        public void NextPage(int range);

        /// <summary>
        /// Handles the pagination to the previous page of data table on the Cart page
        /// </summary>
        /// <param name="range">Range of amount of rows to show</param>
        public void PrevPage(int range);
    }
}