using System.IO;
using System.Runtime.CompilerServices;
using Mono.Cecil;

namespace RewritingApi.ser
{
    public interface ISerializationFacade
    {
        void EmbedNamedResource(string name, TypeReference deserializationProvider, byte[] rawData);
    }
}