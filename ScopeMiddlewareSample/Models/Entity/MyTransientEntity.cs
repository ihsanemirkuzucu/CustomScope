using System;

namespace ScopeMiddlewareSample.Models.Entity
{
    public class MyTransientEntity
    {

        public MyTransientEntity()
        {
            ID = Guid.NewGuid();
        }
        public Guid ID { get; set; }

        public string GetId()
        {
            return $"Transient: {ID}";
        }
    }
}
