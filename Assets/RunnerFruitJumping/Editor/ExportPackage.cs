using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

public class ExportPackage : Editor {

    [MenuItem("ExportProject/Export")]
    public static void Export()
    {
        string[] projectContent = AssetDatabase.GetAllAssetPaths();
        AssetDatabase.ExportPackage(projectContent, "UltimateTemplate.unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.IncludeLibraryAssets);
        Debug.Log("Project Exported");
    }
}
