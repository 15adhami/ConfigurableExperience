using HarmonyLib;
using RimWorld;
using System;
using XmlExtensions;

namespace ConfigurableExperience
{

	[HarmonyPatch(typeof(SkillRecord))]
	[HarmonyPatch("LearnRateFactor")]
	static class Patch_LearningSaturation
	{
		static void Postfix(SkillRecord __instance, ref float __result, SkillDef ___def, bool direct = false)
		{
			// Global multiplier
			__result *= float.Parse(SettingsManager.GetSetting("imranfish.configurableexperience", "globalMultiplier"));

			// Stat multiplier
			__result *= float.Parse(SettingsManager.GetSetting("imranfish.configurableexperience",  ___def.defName + "StatMultiplier"));

			switch (__instance.passion)
			{
				case Passion.None:
					__result *= float.Parse(SettingsManager.GetSetting("imranfish.configurableexperience", "multiplierNoPassion")) / 0.35f / 100f;
					break;
				case Passion.Minor:
					__result *= float.Parse(SettingsManager.GetSetting("imranfish.configurableexperience", "multiplierMinorPassion")) / 100f;
					break;
				case Passion.Major:
					__result *= float.Parse(SettingsManager.GetSetting("imranfish.configurableexperience", "multiplierMajorPassion")) / 1.5f / 100f;
					break;
				default:
					throw new NotImplementedException("Passion level " + __instance.passion);
			}

			if (!direct)
			{
				if (__instance.LearningSaturatedToday)
				{
					__result /= 0.2f;
				}
				if (__instance.xpSinceMidnight > float.Parse(SettingsManager.GetSetting("imranfish.configurableexperience", "dailyThreshold")))
				{
					__result *= float.Parse(SettingsManager.GetSetting("imranfish.configurableexperience", "saturatedMultiplier"));
				}
			}
		}
	}
}
