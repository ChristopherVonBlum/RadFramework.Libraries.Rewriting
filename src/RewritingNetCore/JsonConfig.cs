using System;
using System.Collections.Generic;

namespace RewritingNetCore
{
    [Serializable]
    public class JsonConfig
    {
        public IEnumerable<MiddlewareDefinition> Middlewares { get; set; } = new List<MiddlewareDefinition>();
        public IEnumerable<RewritingDependencyDefinition> Dependencies { get; set; } = new List<RewritingDependencyDefinition>();
    }
}