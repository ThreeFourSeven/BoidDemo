using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidFlock : MonoBehaviour
{

    public GameObject boidPrefab;
    public GameObject targetObject;
    public int size = 10;
    public float boidSize = 0.25f;
    public float maxBoidSpeed = 1.0f;
    public float boidSeperation = 1.0f;
    public float naturalBoidSeperation = .2f;
    public bool naturalBoidDistancing = true;
    public float boidAttractionCoef = 1.0f;
    public float boidAvoidanceCoef = 1.0f;
    public float boidFollowCoef = 1.0f;
    public float boidTragetCoef = 1.0f;
    public float unequalSpeedCoef = 1.0f;

    Vector2 center = new Vector2();

    List<GameObject> boids = new List<GameObject>();

    void AdaptListToSize()
    {
        while (boids.Count > size)
        {
            Destroy(boids[0]);
            boids.RemoveAt(0);
        }
        while (boids.Count < size)
        {
            addBoid(center);
        }
    }

    //Boid rules

    Vector2 AttractionRule(int id, Vector2 position)
    {
        Vector2 v = new Vector2();
        foreach (GameObject go in boids)
        {
            if (id != go.GetComponent<Boid>().id)
            {
                v += go.GetComponent<Boid>().position;
            }
        }
        v /= (size - 1);
        return (v - position) / 100.0f;
    }

    Vector2 AvoidanceRule(int id, Vector2 position)
    {
        Vector2 v = new Vector2();
        float mod = naturalBoidSeperation * Mathf.Abs(Mathf.Sin(2 * Mathf.PI * Time.time * .5f));
        foreach (GameObject go in boids)
        {
            if (id != go.GetComponent<Boid>().id)
            {
                Vector2 boidPosition = go.GetComponent<Boid>().position;
                if ((boidPosition - position).magnitude < boidSeperation + mod)
                    v -= boidPosition - position;
            }
        }
        return v / 8.0f;
    }

    Vector2 FollowRule(int id, Vector2 velocity)
    {
        Vector2 v = new Vector2();
        foreach (GameObject go in boids)
        {
            if (id != go.GetComponent<Boid>().id)
            {
                v += go.GetComponent<Boid>().velocity;
            }
        }
        v /= (size - 1);
        return v;
    }

    Vector2 TargetRule(Vector2 position)
    {
        Vector2 tPosition = targetObject.transform.position;
        return (tPosition - position) / 100.0f;
    }

    Vector2 UnequalSpeedRule(int id, Vector2 position)
    {
        Vector2 v = new Vector2();
        foreach (GameObject go in boids)
        {
            if (id != go.GetComponent<Boid>().id)
            {
                v += go.GetComponent<Boid>().position;
            }
        }
        v /= (size - 1);
        float mod = naturalBoidSeperation * Mathf.Abs(Mathf.Sin(2 * Mathf.PI * Time.time * .5f));
        return mod * (position - v) / 100.0f;
    }

    Vector2 ClampVelocity(Vector2 velocity)
    {
        if (velocity.magnitude > maxBoidSpeed)
            return (velocity / velocity.magnitude) * maxBoidSpeed;
        return velocity;
    }

    void Update()
    {
        if (size < 0)
            size = 0;
        AdaptListToSize();
        Vector2 pSum = new Vector2();
        for (int i = 0; i < boids.Count; i++)
        {
            GameObject go = boids[i];
            int id = go.GetComponent<Boid>().id;
            Vector2 boidPosition = go.GetComponent<Boid>().position;
            Vector2 boidVelocity = go.GetComponent<Boid>().velocity;
            pSum += boidPosition;
            go.GetComponent<Boid>().velocity = ClampVelocity(boidVelocity + AttractionRule(id, boidPosition) * boidAttractionCoef + AvoidanceRule(id, boidPosition) * boidAvoidanceCoef + FollowRule(id, boidVelocity) * boidFollowCoef + TargetRule(boidPosition) * boidTragetCoef + UnequalSpeedRule(id, boidPosition) * unequalSpeedCoef);
        }
        if (size > 0)
            center = pSum / boids.Count;
    }

    public void addBoid(Vector2 position) {
        GameObject go = Instantiate(boidPrefab, new Vector3(0,0,0), Quaternion.identity);
        go.transform.parent = transform;
        go.GetComponent<Boid>().id = boids.Count;
        go.GetComponent<Boid>().position = position;
        go.transform.localScale = new Vector3(boidSize, boidSize, 1.0f);
        boids.Add(go);
    }
}
