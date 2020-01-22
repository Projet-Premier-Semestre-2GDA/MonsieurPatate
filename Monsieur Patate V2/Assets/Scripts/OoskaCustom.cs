using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public static class OoskaCustom {
    static Dictionary<string, bool> AxisDownDict = new Dictionary<string, bool>();
    public static bool GetAxisDown(string axisName) {
        float absoluteAxisValue = Mathf.Abs(Input.GetAxisRaw(axisName));
        bool isAxisDown = absoluteAxisValue > 0.1;
        
        //Ajouter l'axe au dictionaire
        if (!AxisDownDict.ContainsKey(axisName)) {
            AxisDownDict.Add(axisName, false);
        }

        if (isAxisDown && AxisDownDict[axisName]) {
            return false;
        }
        
        AxisDownDict[axisName] = isAxisDown;
        return isAxisDown;
    }

    public static bool GetAxisUp(string axisName) {
        float absoluteAxisValue = Mathf.Abs(Input.GetAxisRaw(axisName));
        bool isAxisUp = absoluteAxisValue < 0.1;
        
        //Ajouter l'axe au dictionaire
        if (!AxisDownDict.ContainsKey(axisName)) {
            AxisDownDict.Add(axisName, true);
        }

        if (isAxisUp && !AxisDownDict[axisName]) {
            return false;
        }

        AxisDownDict[axisName] = !isAxisUp;
        return isAxisUp;
    }
}