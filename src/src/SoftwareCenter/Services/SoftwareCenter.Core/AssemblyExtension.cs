using System.Reflection;

namespace SoftwareCenter.Core;

public static class AssemblyExtension
{
    extension<T>(T _) where T : Assembly
    {
        public static bool IsBuildingOpenApiDocs()
        {
            return Assembly.GetEntryAssembly()?.GetName().Name == "GetDocument.Insider";
        }
        
        public static bool NotBuildingOpenApiDocs()
        {
            return Assembly.GetEntryAssembly()?.GetName().Name != "GetDocument.Insider";
        }
  
    }
}