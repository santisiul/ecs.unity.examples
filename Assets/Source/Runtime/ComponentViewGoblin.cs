//  Project : Actors-Examples
// Contacts : Pixeye - info@pixeye.games
//     Date : 2/20/2019 

namespace Pixeye
{
	[System.Serializable]
	public class ComponentViewGoblin : IComponent
	{
		 
	}

	public static class ExtensionComponentViewGoblin
	{
		public static ComponentViewGoblin ComponentViewGoblin(this int entity) { return Storage<ComponentViewGoblin>.Instance.components[entity]; }

		public static bool TryGetComponentViewGoblin(this int entity, out ComponentViewGoblin component)
		{
			component = Storage<ComponentViewGoblin>.Instance.TryGet(entity);
			return component != null;
		}

		public static bool HasComponentViewGoblin(this int entity) { return Storage<ComponentViewGoblin>.Instance.HasComponent(entity); }
	}
}