using ScopeMiddlewareSample.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopeMiddlewareSample.Models
{
    public class LifeTimeEx
    {
        private readonly MyScopeEntity _myScopeEntity;
        private readonly MyTransientEntity _myTransientEntity;

        public LifeTimeEx(MyScopeEntity myScopeEntity, MyTransientEntity myTransientEntity)
        {
            _myScopeEntity = myScopeEntity;
            _myTransientEntity = myTransientEntity;
        }

        public string GetGuid()
        {
            return $"Scope: {_myScopeEntity.ID} --- Transient Scope: {_myTransientEntity.ID}";
        }
    }
}
