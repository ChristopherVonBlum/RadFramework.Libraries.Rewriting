using System;
using RewritingContracts;

namespace TestTarget
{
    [AuthorizationServiceConsumer]
    public interface ITestDto
    {
        string Value1 { get; }
        int Value2 { get; }
    }
}