#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// AI editor.
/// 
/// red is for sight range.
/// 
/// green is for minimum firing range.
/// 
/// blue is for maximum firing range.
/// 
/// 
/// 
/// </summary>

[CustomEditor (typeof(TankBehaviour))]
public class AIEditor : Editor 
{

    public Transform player;

    void OnSceneGUI()
    {
        TankBehaviour tb = (TankBehaviour)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(tb.transform.position, Vector3.up, Vector3.forward, 360, tb.viewRadius);

        Handles.color = Color.red;

        foreach (Transform visibleTarget in tb.visibleTarget)
        {
            Handles.DrawLine(tb.transform.position, visibleTarget.position);
        }

        Handles.color = Color.green;
        Handles.DrawWireArc(tb.transform.position, Vector3.up, Vector3.forward, 360, tb.minimumRange);


        Handles.color = Color.blue;
        Handles.DrawWireArc(tb.transform.position, Vector3.up, Vector3.forward, 360, tb.maximumRange);

        Handles.color = Color.black;
        Handles.DrawWireArc(tb.transform.position, Vector3.up, Vector3.forward, 360, tb.wanderRadius);
    }
}
[CustomEditor (typeof(SniperBehavior))]
public class SniperVision : Editor 
{
    void OnSceneGUI()
    {
        SniperBehavior sb = (SniperBehavior)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(sb.transform.position, Vector3.up, Vector3.forward, 360, sb.viewRadius);

        Handles.color = Color.red;

        foreach (Transform visibleTarget in sb.visibleTarget)
        {
            Handles.DrawLine(sb.transform.position, visibleTarget.position);
        }

        Handles.color = Color.green;
        Handles.DrawWireArc(sb.transform.position, Vector3.up, Vector3.forward, 360, sb.minimumRange);

        Handles.color = Color.blue;
        Handles.DrawWireArc(sb.transform.position, Vector3.up, Vector3.forward, 360, sb.maximumRange);

        Handles.color = Color.black;
        Handles.DrawWireArc(sb.transform.position, Vector3.up, Vector3.forward, 360, sb.wanderRadius);

    }
}

[CustomEditor (typeof(ArtilleryBehavior))]
public class Artillery : Editor
{
    void OnSceneGUI()
    {
        ArtilleryBehavior ab = (ArtilleryBehavior)target;


        Handles.color = Color.green;
        Handles.DrawWireArc(ab.transform.position, Vector3.up, Vector3.forward, 360, ab.minimumRange);

        Handles.color = Color.blue;
        Handles.DrawWireArc(ab.transform.position, Vector3.up, Vector3.forward, 360, ab.maximumRange);


        //I I commenting this first. Don't know if artillery even need the wandering.
        /*
        Handles.color = Color.black;
        Handles.DrawWireArc(ab.transform.position, Vector3.up, Vector3.forward, 360, ab.wanderRadius);
        */

    }
}


#endif