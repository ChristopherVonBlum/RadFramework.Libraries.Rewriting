using System;
using RewritingApi.middleware;
using TestTarget;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            RuntimeMetadataSerializationStorage storage = new RuntimeMetadataSerializationStorage(typeof(ITestDto).Assembly);
            storage.GetIndex("");
        }
    }
}