using System.Diagnostics.CodeAnalysis;
using TechTreeWebApp.Areas.Admin.Models;
using TechTreeWebApp.Entities;

namespace TechTreeWebApp.Comparers
{
    public class CompareUser : IEqualityComparer<UserModel>
    {
        public bool Equals(UserModel? x, UserModel? y)
        {
           if(y == null) return false;
           return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] UserModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
