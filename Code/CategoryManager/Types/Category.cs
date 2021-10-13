using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabaseActionReusables.CategoryManager
{
    public class Category
    {

        private Category(int id, string name)
        {
            Id = id;
            Name = name;
        }


        public int Id { get; }

        public string Name { get; }


        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
            {
                return ((Category)(obj)).Id == Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


        public class Builder
        {

            public string Name { set; get; }

            public Category Build(int id)
            {
                return new Category(id, Name);
            }

        }

    }
}
