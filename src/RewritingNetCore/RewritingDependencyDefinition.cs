using System;

namespace RewritingNetCore
{
    [Serializable]
    public class RewritingDependencyDefinition
    {
        public string Type { get; set; }
        public string Implementation { get; set; }
        public string Assembly { get; set; }
    }
}