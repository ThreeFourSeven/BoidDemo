using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [HideInInspector]
    public int id;
    [HideInInspector]
    public Vector2 position = new Vector2();
    public Vector2 velocity = new Vector2();

    private void FixedUpdate()
    {
        var angle = -Mathf.Atan2(velocity.x, velocity.y) * Mathf.Rad2Deg;
        position += velocity;
        transform.position = new Vector3(position.x, position.y, 0.0f);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
