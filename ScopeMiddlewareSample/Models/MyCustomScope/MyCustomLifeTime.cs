using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ScopeMiddlewareSample.Models.MyScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopeMiddlewareSample.Models.MyCustomScope
{
    public class MyCustomLifeTime : IServiceScopeFactory
    {
        //key=IP   value=List<Storage>
        public readonly static Dictionary<string, List<Storage>> instances = new Dictionary<string, List<Storage>>();

        public IServiceScope CreateScope()
        {
            return new CustomScope(this);
        }

        public readonly HttpContext _httpContext;
        public readonly string _ip;

        public MyCustomLifeTime(HttpContext httpContext)
        {
            _httpContext = httpContext;
            _ip = GetIpAddress();
        }

        protected string GetIpAddress()
        {                                                //Kaynak IP adresi bu keywordde tutulur
            if (_httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                return _httpContext.Request.Headers["X-Forwarded-For"];
            return _httpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();// IP adresini al, IPv6 ise IPv4'e dönüştürüp al
        }

    }
}
