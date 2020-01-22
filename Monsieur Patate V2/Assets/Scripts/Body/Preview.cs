using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour {

    public static bool activated = true;
    public float radius = 0.1f;
    private void OnDrawGizmos() {
        if (activated) {
            Gizmos.DrawCube(transform.position, Vector3.one * radius);
        }
    }
}
