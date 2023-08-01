using System.Collections.Generic;
using UnityEngine;

public class StageData {

    public class StageObject {

        public GameObject assign;
        public Vector3 position, size;
        public float rotation;
        public Dictionary<string, object> properties = new Dictionary<string, object>();

        public override string ToString() {
            return "{{" + assign + "," + position + "," + size + "," + rotation + "}," + properties + "}";
        }
    }

    public List<StageObject> objects = new List<StageObject>();
    public int index = -1;

    public override string ToString() {
        string str = "";
        foreach (StageObject stageObject in objects) {
            str += stageObject.ToString() + ",";
        }

        return "[" + str.Substring(0, str.Length - 1) + "]";
    }
}