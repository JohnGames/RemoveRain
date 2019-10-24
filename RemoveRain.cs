using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityModManagerNet;
using System.Reflection;
using static UnityModManagerNet.UnityModManager;
using System.IO;
using System.Collections;
using UnityEngine.Video;
using System.Reflection.Emit;

namespace RemoveRain
{
	static class Main
	{

		// Send a response to the mod manager about the launch status, success or not.
		static bool Load(ModEntry modEntry)
		{
			modEntry.OnToggle = OnToggle;
			modEntry.OnUpdate = OnUpdate;
			if (modEntry.GameVersion != gameVersion)
			{
				UnityModManager.Logger.Log($"{modEntry.Info.DisplayName} expects {modEntry.GameVersion} but found {gameVersion}.");
			}
			return true; // If false the mod will show an error.
		}

		static bool OnToggle(UnityModManager.ModEntry modEntry, bool value /* active or inactive */)
		{
			if (NotNullCheck)
			{
				if (value)
				{
					if (RainManager.Instance.m_IsEnabled)
					{
						if (RainManager.Instance.GetIsRaining())
						{
							RainManager.Instance.ToggleRain();
						}
						RainManager.Instance.SetEnabled(false);
					}
				}
				else
				{
					if (!RainManager.Instance.m_IsEnabled)
					{
						RainManager.Instance.SetEnabled(true);
						RainManager.Instance.StartRainingNow();
					}
				}
			}


			modEntry.Enabled = value;
			return true;
		}

		static void OnUpdate(UnityModManager.ModEntry modEntry, float dt)
		{
			if (NotNullCheck)
			{
				if (RainManager.Instance.m_IsEnabled)
				{
					if (RainManager.Instance.GetIsRaining())
					{
						RainManager.Instance.ToggleRain();
					}
					RainManager.Instance.SetEnabled(false);
				}
			}
		}

		static bool NotNullCheck
		{
			get
			{
				return RainManager.Instance != null
					&& GameStateManager.Instance != null;
			}
		}
	}


}

