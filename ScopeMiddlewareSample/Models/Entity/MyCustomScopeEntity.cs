using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopeMiddlewareSample.Models.Entity
{
    public class MyCustomScopeEntity
    {
        

        public MyCustomScopeEntity()
        {
            ID = Guid.NewGuid();
        }
        public Guid ID { get; set; }



        public string GetId()
        {
            return $"Custom Scope: {ID}";
        }
    }
}
