using System;
using Xunit;

namespace RewritingNetCore.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //new BuildIsolationAssemlbyLoadContext().LoadEquivalent(typeof(RadFrameworkApplyRewritingTask));
            
            new RadFrameworkApplyRewritingTask
            {
                MSBuildProjectDirectory = "/home/anon/Documents/repos/RewritingNetCore/TestTarget",
                LibFolder = "/home/anon/Documents/repos/RewritingNetCore/RewritingNetCore.Tests/bin/Debug/netcoreapp2.1",
                IntermediateAssemblyName = "TestTarget.dll",
                IntermediateOutputPath = "/home/anon/Documents/repos/RewritingNetCore/TestTarget/obj/Debug/netcoreapp2.1",
                MSBuildProjectFile = "/home/anon/Documents/repos/RewritingNetCore/TestTarget/TestTarget.csproj",
                CoreRewritingDependencies = "[{ 'type' : 'RewritingApi.IAssemblyQueryProvider, RewritingApi', 'implementation' : 'RewritingApi.impl.AssemblyQueryProvider, RewritingApi' }]",
                CoreRewritingMiddleware = "[{ 'type' : 'RewritingApi.middleware.MetadataSerializationMiddleware, RewritingApi' }]"
            }.Execute();
        }
    }
}