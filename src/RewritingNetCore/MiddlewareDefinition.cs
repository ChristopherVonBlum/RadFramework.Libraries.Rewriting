using System;
using System.Collections.Generic;
using System.Reflection;

namespace RewritingNetCore
{
    [Serializable]
    public class MiddlewareDefinition
    {
        [NonSerialized]
        public Assembly RuntimeAssembly;
        public string Type { get; set; }
        public string Assembly { get; set; }
    }
    
    
}