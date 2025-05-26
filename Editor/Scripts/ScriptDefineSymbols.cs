using Reflectis.SDK.Core.Editor;
using UnityEditor;

namespace Reflectis.CreatorKit.Worlds.Dialogs.Editor
{
    [InitializeOnLoad]
    public class ScriptDefineSymbols
    {
        public const string DIALOGS_SCRIPT_DEFINE_SYMBOL = "REFLECTIS_CREATOR_KIT_WORLDS_DIALOGS";
        static ScriptDefineSymbols()
        {
            ScriptDefineSymbolsUtilities.AddScriptingDefineSymbolToAllBuildTargetGroups(DIALOGS_SCRIPT_DEFINE_SYMBOL);
        }
    }
}