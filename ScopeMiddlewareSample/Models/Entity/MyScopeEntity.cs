using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopeMiddlewareSample.Models.Entity
{
    public class MyScopeEntity
    {
        private readonly MyTransientEntity _myTransientEntity;

        public MyScopeEntity(MyTransientEntity myTransientEntity)
        {
            ID = Guid.NewGuid();
            _myTransientEntity = myTransientEntity;
        }
        public Guid ID { get; set; }

        public string GetTransientId()
        {
            return $"Scope: {ID} --- Transient Scope: {_myTransientEntity.ID}";
        }

        public string GetId()
        {
            return $"Scope: {ID}";
        }

    }
}
