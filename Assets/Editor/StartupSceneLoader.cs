#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class StartupSceneLoader
{
    // IMPORTANT: Adjust this path if your scene is elsewhere
    private const string MainMenuPath = "Assets/MainMenu.unity";

    static StartupSceneLoader()
    {
        EditorApplication.delayCall += OnEditorLoaded;
    }

    private static void OnEditorLoaded()
    {
        // Load scene asset
        SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(MainMenuPath);

        if (scene == null)
        {
            Debug.LogWarning($"Could not find MainMenu at: {MainMenuPath}");
            return;
        }

        // Force Play Mode to always start in MainMenu
        EditorSceneManager.playModeStartScene = scene;

        // If not already dirty, open MainMenu automatically
        if (!EditorApplication.isPlayingOrWillChangePlaymode)
        {
            if (!EditorSceneManager.GetActiveScene().isDirty)
            {
                EditorSceneManager.OpenScene(MainMenuPath);
            }
        }

        Debug.Log("StartupSceneLoader applied successfully.");
    }
}
#endif
