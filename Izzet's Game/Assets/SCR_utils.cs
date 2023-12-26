using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SCR_utils {
    public class customAttributes {
        public class ReadOnlyAttribute : PropertyAttribute { }
    }
    public class functions {
        public static bool checkIfPrefab(GameObject a, GameObject b) {
            if (a.GetPrefabDefinition() == b.GetPrefabDefinition())
                return true;
            return false;
        }
    }
}
