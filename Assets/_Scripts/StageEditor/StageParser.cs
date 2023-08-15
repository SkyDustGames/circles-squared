using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using UnityEngine;
using static StageData;

public class StageParser : MonoBehaviour {
    
    public GameObject[] prefabs;
    static Dictionary<string, GameObject> objects;

    private void Awake() {
        if (objects != null) return;

        objects = new Dictionary<string, GameObject>();
        foreach (GameObject gameObject in prefabs) {
            objects.Add(gameObject.name, gameObject);
        }
    }

    public static StageData ParseDataFromFile(string path) {
        try {
            return ParseData(File.ReadAllText(path));
        } catch(IOException e) {
            Debug.LogError(e);
        }
        
        return null;
    }

    public static StageData ParseData(TextAsset textAsset) {
        return ParseData(textAsset.text, true);
    }

    public static StageData ParseData(string text, bool official = false) {
        try {
            XmlDocument document = new XmlDocument();
            document.LoadXml(text);

            StageData data = new StageData();

            XmlNode root = document.DocumentElement;
            foreach (XmlNode item in root) {
                StageObject stageObject = new StageObject();

                stageObject.assign = objects.GetValueOrDefault(item.Name);
                foreach (XmlAttribute attribute in item.Attributes) {
                    switch(attribute.Name) {
                        case "pos":
                        stageObject.position = GetVector(attribute.Value);
                        break;
                        case "rotation":
                        stageObject.rotation = float.Parse(attribute.Value);
                        break;
                        case "scale":
                        stageObject.size = GetVector(attribute.Value);
                        break;
                        default:
                        stageObject.properties.Add(attribute.Name, GetAttributeValue(attribute.Value));
                        break;
                    }
                }

                data.objects.Add(stageObject);
            }

            XmlAttribute index = root.Attributes["index"];
            if (index != null)
                data.index = int.Parse(index.Value);
            return data;
        } catch (XmlException e) {
            Debug.LogError(e);
        }

        return null;
    }

    private static object GetAttributeValue(string str) {
        if (str.Equals("true")) return true;
        if (str.Equals("false")) return false;

        if (str.Contains(",")) return GetVector(str);

        try {
            return float.Parse(str, CultureInfo.InvariantCulture);
        } catch {}

        return str;
    }

    private static Vector3 GetVector(string str) {
        string[] s = str.Split(",");
        return new Vector3(float.Parse(s[0], CultureInfo.InvariantCulture), float.Parse(s[1], CultureInfo.InvariantCulture));
    }
}