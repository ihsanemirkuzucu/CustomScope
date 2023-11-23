using ScopeMiddlewareSample.Models.MyScope;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopeMiddlewareSample.Models.MyCustomScope
{
    public class CustomServiceProvider : IServiceProvider
    {
        private readonly MyCustomLifeTime _myCustomLifeTime;

        public CustomServiceProvider(MyCustomLifeTime myCustomLifeTime)
        {
            _myCustomLifeTime = myCustomLifeTime;
        }

        public object GetService(Type serviceType)
        {
            if (MyCustomLifeTime.instances.TryGetValue(_myCustomLifeTime._ip, out List<Storage> instances))
            {
                var current = instances.FirstOrDefault(x => x.Path == _myCustomLifeTime._httpContext.Request.Path);
                if (current != null)
                {
                    if (current.Count == 2)
                    {
                        current.Nesne = Activator.CreateInstance(serviceType);
                        current.Path = _myCustomLifeTime._httpContext.Request.Path;
                        current.Count = 0;

                        return current.Nesne;
                    }
                    current.Count++;
                    return current.Nesne;

                }
            }

            Storage storage = new Storage()
            {
                Nesne = Activator.CreateInstance(serviceType),
                Path = _myCustomLifeTime._httpContext.Request.Path,
                Count = 1
            };

            if (instances == null)
            {
                Log.Warning($"{_myCustomLifeTime._ip} li kullanıcı ilk kez {storage.Path}'i iyaret etti");
                instances = new List<Storage>();
                instances.Add(storage);
                MyCustomLifeTime.instances.Add(_myCustomLifeTime._ip, instances);
                return storage.Nesne;
            }

            instances.Add(storage);
            return storage.Nesne;

        }
    }
}
