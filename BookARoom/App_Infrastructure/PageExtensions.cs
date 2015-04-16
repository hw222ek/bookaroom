using System.Web.UI;

namespace BookARoom
{
    public static class PageExtensions
    {
        /// <summary>
        /// Metod för att hämta data lagrad i session
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetTempData(this Page page, string key)
        {
            var value = page.Session[key];
            page.Session.Remove(key);
            return value;
        }

        /// <summary>
        /// Metod för att lagra data i session
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetTempData(this Page page, string key, object value)
        {
            page.Session[key] = value;
        }
    }
}