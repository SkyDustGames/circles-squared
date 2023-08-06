using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class Saves {

    public static Save loaded;
    public static string path = Application.persistentDataPath + Path.DirectorySeparatorChar + "c2save.png";

    [RuntimeInitializeOnLoadMethod]
    public static void Load() {
        Application.quitting += Save;

        if (!File.Exists(path)) {
            loaded = new Save();
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        loaded = formatter.Deserialize(stream) as Save;
        stream.Close();
    }

    #if UNITY_EDITOR
    [InitializeOnLoadMethod]
    private static void RunOnEditorExit() {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state) {
        if (state == PlayModeStateChange.ExitingPlayMode)
            Save();
    }
    #endif

    public static void Save() {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, loaded);
        stream.Close();
    }
}