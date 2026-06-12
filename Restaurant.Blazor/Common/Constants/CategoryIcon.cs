namespace Restaurant.Blazor.Common.Constants
{
    public static class CategoryIcon
    {
        public const string MainCourse = "bi bi-fork-knife";
        public const string Desserts = "bi bi-cup-hot";
        public const string Appetizer = "bi bi-egg-fried";

        public static string GetIcon(this string name)
        {
            switch (name)
            {
                case "Main Course":
                    return MainCourse;
                case "Desserts":
                    return Desserts;
                case "Appetizer":
                    return Appetizer;
                default:
                    return name;
            }
        }
    }
}
