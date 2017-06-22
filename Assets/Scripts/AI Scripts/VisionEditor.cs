#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(TankBehaviour))]
public class VisionEditor : Editor {

    public Transform player;

    void OnSceneGUI()
    {
        TankBehaviour cov = (TankBehaviour)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(cov.transform.position, Vector3.up, Vector3.forward, 360, cov.viewRadius);
        /*
        Vector3 viewAngleA = cov.DirecFromAngle(-cov.viewAngle /2, false);
        Vector3 viewAngleB = cov.DirecFromAngle(cov.viewAngle /2, false);

        Handles.DrawLine(cov.transform.position, cov.transform.position + viewAngleA * cov.viewRadius);
        Handles.DrawLine(cov.transform.position, cov.transform.position + viewAngleB * cov.viewRadius);
        */


        Handles.color = Color.red;

        foreach (Transform visibleTarget in cov.visibleTarget)
        {
            Handles.DrawLine(cov.transform.position, visibleTarget.position);
        }

        Handles.color = Color.green;
        Handles.DrawWireArc(cov.transform.position, Vector3.up, Vector3.forward, 360, cov.wanderRadius);


        Handles.color = Color.blue;
        Handles.DrawWireArc(cov.transform.position, Vector3.up, Vector3.forward, 360, cov.minimumRange);

    }
}
[CustomEditor (typeof(SniperBehavior))]
public class SniperVision : Editor {

    public Transform player;

    void OnSceneGUI()
    {
        SniperBehavior cov = (SniperBehavior)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(cov.transform.position, Vector3.up, Vector3.forward, 360, cov.viewRadius);

        Handles.color = Color.red;

        foreach (Transform visibleTarget in cov.visibleTarget)
        {
            Handles.DrawLine(cov.transform.position, visibleTarget.position);
        }

        Handles.color = Color.green;
        Handles.DrawWireArc(cov.transform.position, Vector3.up, Vector3.forward, 360, cov.wanderRadius);

        Handles.color = Color.blue;
        Handles.DrawWireArc(cov.transform.position, Vector3.up, Vector3.forward, 360, cov.minimumRange);

    }
}

#endif