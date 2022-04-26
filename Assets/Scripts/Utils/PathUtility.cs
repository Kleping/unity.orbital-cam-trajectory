#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Windows;

namespace Utils
{
    public static class PathUtility
    {
        #if UNITY_EDITOR
        public static string GetSelectedRoot()
        {
            var obj = Selection.activeObject;
            var path = obj == null ? "Assets" : AssetDatabase.GetAssetPath(obj.GetInstanceID());
            const char directorySeparatorChar = '/';

            if (path.Length <= 0) return string.Empty;
            if (Directory.Exists(path)) return path;

            var newPath = string.Empty;
            var dirLevels = path.Split(directorySeparatorChar);
            static bool CheckCondition(int i, int i1, int len)
            {
                return i < len - i1;
            }
                
            for (var i = 0; CheckCondition(i, 1, dirLevels.Length); i++)
            {
                newPath += dirLevels[i];
                if (CheckCondition(i + 1, 1, dirLevels.Length))
                {
                    newPath += directorySeparatorChar;
                }
            }

            return newPath;
        }
        #endif
    }
}
