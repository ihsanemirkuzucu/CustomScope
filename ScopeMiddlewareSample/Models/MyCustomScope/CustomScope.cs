using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ScopeMiddlewareSample.Models.MyCustomScope
{
    public class CustomScope : IServiceScope
    {
        private readonly MyCustomLifeTime _myCustomLifeTime;
        private bool disposedValue;
        private List<int> managedResource = new List<int>();
        private IntPtr unmanagedResource;


        public CustomScope(MyCustomLifeTime myCustomLifeTime)
        {
            _myCustomLifeTime = myCustomLifeTime;
            unmanagedResource = Marshal.AllocHGlobal(100);
        }

        public IServiceProvider ServiceProvider => new CustomServiceProvider(_myCustomLifeTime);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Managed kaynakları burada temizleyebiliriz
                    if (managedResource != null)
                    {
                        managedResource.Clear();
                        managedResource = null;
                    }
                }
                //finalizer calisti daha sonra false geldigi icin buraya sistemin temizleyemeyecegi kaynakları temizletiyoruz
                // Unmanaged kaynağı burada temizleyebiliriz
                if (unmanagedResource != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(unmanagedResource);
                    unmanagedResource = IntPtr.Zero;
                }

                disposedValue = true;
            }
        }
    }
}
