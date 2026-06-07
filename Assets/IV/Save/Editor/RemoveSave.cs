using UnityEditor;

namespace IV.Save.Editor
{
    public class RemoveSave
    {
        [MenuItem("IV/Clear Saves", priority = 90)]
        public static void Init()
        {
            SaveManager.ClearSaves();
        }
    }
}