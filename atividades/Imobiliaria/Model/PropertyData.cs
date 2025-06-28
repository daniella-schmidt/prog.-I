using Model;
using System.Collections.Generic;

namespace Repository
{
    public static class PropertyData
    {
        public static List<Property> Properties { get; set; } = new List<Property>();
        public static List<Category> Categories{ get; set; } = new List<Category>();
    }
}