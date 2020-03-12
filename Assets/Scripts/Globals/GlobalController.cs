using System;
using UnityEngine;

namespace SoulsLike
{
    public static class GlobalController
    {
        public static SavedGlobals SavedGlobals { get; private set; }

        static GlobalController()
        {
            SavedGlobals = new SavedGlobals();
        }

        /// <summary>
        /// Set Globals
        /// </summary>
        /// <param name="savedGlobals"></param>
        public static void SetGlobals(SavedGlobals savedGlobals) => SavedGlobals = savedGlobals;

        /// <summary>
        /// Get Global Setting
        /// </summary>
        /// <param name="globalID"></param>
        /// <returns></returns>
        public static GlobalSetting GetGlobalSetting(string globalID)
        {
            GlobalSetting globalSetting = Resources.Load<GlobalSetting>($"{GlobalConsts.GLOBAL_SETTINGS_FOLDER}/{globalID}");
            if (globalSetting == null)
            {
                Debug.LogError($"Error during SetGlobal: Global Setting {globalID} does not exist.");
                return null;
            }
            return globalSetting;
        }

        /// <summary>
        /// Set Global Setting
        /// </summary>
        /// <param name="globalID"></param>
        /// <param name="value"></param>
        public static void SetGlobal(string globalID, int value)
        {
            GlobalSetting globalSetting = GetGlobalSetting(globalID);
            if(globalSetting == null)
            {
                return;
            }
            string globID = globalSetting.globalName;
            try
            {
                if (!SavedGlobals.globals.ContainsKey(globID))
                {
                    SavedGlobals.globals.Add(globID, value);
                }
                else
                {
                    SavedGlobals.globals[globID] = value;
                }
            }
            catch (Exception exception)
            {
                Debug.LogError($"Error during SetGlobal: {exception.Message}");
            }
        }

        /// <summary>
        /// Get Global Setting
        /// </summary>
        /// <param name="globalID"></param>
        /// <returns></returns>
        public static int GetGlobal(string globalID)
        {
            if (SavedGlobals.globals.ContainsKey(globalID))
            {
                return SavedGlobals.globals[globalID];
            }
            Debug.LogWarning($"Global {globalID} does not exist.");
            return -1;
        }
    }
}
