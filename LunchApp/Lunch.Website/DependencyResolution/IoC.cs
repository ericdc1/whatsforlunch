using System.Reflection;
using Lunch.Core;
using StructureMap.Configuration.DSL;

namespace Lunch.Website.DependencyResolution
{

	public class StructureMapRegistry : Registry
	{

		public StructureMapRegistry()
		{
			Scan(s =>
			{
				s.WithDefaultConventions();
				s.AssembliesFromApplicationBaseDirectory(AssemblyFilter);
			});
		}

		private bool AssemblyFilter(Assembly assembly)
		{
			object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
			if (customAttributes.Length == 0) {
				return false;
			}
			if (((AssemblyProductAttribute)customAttributes[0]).Product.StartsWith("Lunch")) {
				return true;
			}
			return false;
		}

	}

}
