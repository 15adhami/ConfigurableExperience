using HarmonyLib;
using Verse;

namespace ConfigurableExperience
{
	public class ConfigurableExperience : Mod
	{
		public ConfigurableExperience(ModContentPack content) : base(content)
		{
			var harmony = new Harmony("com.github.15adhami.configurableexperience");
			harmony.PatchAll();
		}
	}
}
