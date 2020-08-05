using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBoidGlobals : MonoBehaviour
{
    public static float size = 0.15f;
    public static float maxSpeed = 0.05f;
    public static float seperation = 10.0f;
    public static float attractionCoef = 1.6f;
    public static float avoidanceCoef = 4.9f;
    public static float followCoef = 1.4f;
    public static float targetCoef = 0.35f;
    public static float targetCenterCoef = 0.1f;
    public static float viewRange = 0.5f;

    // Update is called once per frame
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0,0,256,384));
        GUILayout.Label("Boid Count " + DemoPlayer.count);
        GUILayout.Label("Boid Size " + size);
        size = GUILayout.HorizontalSlider(size, .1f, .5f);
        GUILayout.Label("Boid Max Speed " + maxSpeed);
        maxSpeed = GUILayout.HorizontalSlider(maxSpeed, .01f, 1.0f);
        GUILayout.Label("Boid Separation " + seperation);
        seperation = GUILayout.HorizontalSlider(seperation, 0.1f, 10.0f);
        GUILayout.Label("Boid Attraction Coefficient " + attractionCoef);
        attractionCoef = GUILayout.HorizontalSlider(attractionCoef, 0.0f, 10.0f);
        GUILayout.Label("Boid Avoidance Coefficient " + avoidanceCoef);
        avoidanceCoef = GUILayout.HorizontalSlider(avoidanceCoef, 0.0f, 10.0f);
        GUILayout.Label("Boid Follow Coefficient " + followCoef);
        followCoef = GUILayout.HorizontalSlider(followCoef, 0.0f, 10.0f);
        GUILayout.Label("Boid Target Coefficient " + targetCoef);
        targetCoef = GUILayout.HorizontalSlider(targetCoef, 0.0f, 10.0f);
        GUILayout.Label("Boid Target Center Coefficient " + targetCenterCoef);
        targetCenterCoef = GUILayout.HorizontalSlider(targetCenterCoef, 0.0f, 10.0f);
        GUILayout.Label("Boid View Range " + viewRange);
        viewRange = GUILayout.HorizontalSlider(viewRange, 1.0f, 5.0f);
        GUILayout.EndArea();
    }
}
