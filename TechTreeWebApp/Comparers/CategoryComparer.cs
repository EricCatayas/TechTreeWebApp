using System.Collections;
using System.Diagnostics.CodeAnalysis;
using TechTreeWebApp.Entities;

namespace TechTreeWebApp.Comparers
{
    public class CategoryComparer : IEqualityComparer<Category>
    {
        public bool Equals(Category? x, Category? y)
        {
            if(y==null) return false;
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] Category obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
