using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBoid : MonoBehaviour
{
    public GameObject targetObject;
    [HideInInspector]
    public int id;
    [HideInInspector]
    public Vector2 position = new Vector2();
    [HideInInspector]
    public Vector2 velocity = new Vector2();

    float tickCtr = 0;
    float timeTillTick = 10;

    Vector2 ComputeVelocityFromRay(RaycastHit2D hit)
    {
        Vector2 attraction = new Vector2();
        Vector2 avoidance = new Vector2();
        Vector2 follow = new Vector2();
        Vector2 target = new Vector2();
        Vector2 flockCenter = new Vector2();
        if (hit.collider != null)
        {
            Vector2 boidPosition = hit.collider.gameObject.GetComponent<OBoid>().position;
            Vector2 boidVelocity = hit.collider.gameObject.GetComponent<OBoid>().velocity;
            attraction = (boidPosition - position) / 100.0f;
            if ((boidPosition - position).magnitude < OBoidGlobals.seperation* OBoidGlobals.seperation)
                avoidance = -(boidPosition - position) / 8.0f;
            follow = boidVelocity;
        }
        Vector2 target2D = targetObject.transform.position;
        target = (target2D - position) / 100.0f;
        flockCenter = (DemoPlayer.flockCenter - position) / 100.0f;
        return OBoidGlobals.attractionCoef * attraction + OBoidGlobals.avoidanceCoef * avoidance + OBoidGlobals.followCoef * follow + OBoidGlobals.targetCoef * target + OBoidGlobals.targetCenterCoef * flockCenter;
    }

    Vector2 ClampVelocity(Vector2 velocity)
    {
        if (velocity.magnitude > OBoidGlobals.maxSpeed)
            return (velocity / velocity.magnitude) * OBoidGlobals.maxSpeed;
        return velocity;
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tickCtr++;
        if (tickCtr > timeTillTick)
        {
            tickCtr = 0;
            var forwardRay = GetDirection();
            /*var leftRay = Quaternion.AngleAxis(90, Vector3.forward) * forwardRay;
            var rightRay = Quaternion.AngleAxis(-90, Vector3.forward) * forwardRay;*/

            var forwardVelocityInfluence = ComputeVelocityFromRay(Physics2D.Raycast(transform.position, forwardRay, OBoidGlobals.viewRange));
            /*var leftVelocityInfluence = ComputeVelocityFromRay(Physics2D.Raycast(transform.position, leftRay, OBoidGlobals.viewRange));
            var rightVelocityInfluence = ComputeVelocityFromRay(Physics2D.Raycast(transform.position, rightRay, OBoidGlobals.viewRange));*/

            velocity = ClampVelocity(velocity + forwardVelocityInfluence /*+ leftVelocityInfluence + rightVelocityInfluence*/);
        }
        transform.localScale = new Vector3(OBoidGlobals.size, OBoidGlobals.size, OBoidGlobals.size);
        var angle = -Mathf.Atan2(velocity.x, velocity.y) * Mathf.Rad2Deg;
        position += velocity;
        transform.position = new Vector3(position.x, position.y, 0.0f);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public Vector2 GetDirection()
    {
        return velocity / velocity.magnitude;
    }

}
