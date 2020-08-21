using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayer : MonoBehaviour
{
    public GameObject boidPrefab;
    public GameObject targetObject;
    public static Vector2 flockCenter = new Vector2();
    public static int count = 0;
    List<GameObject> boids = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 500; i++)
        {
            GameObject go = Instantiate(boidPrefab, new Vector3(), Quaternion.identity);
            go.GetComponent<OBoid>().GetComponent<OBoid>().targetObject = targetObject;
            go.GetComponent<OBoid>().GetComponent<OBoid>().position = new Vector2(Random.Range(-10,10), Random.Range(-10, 10));
            boids.Add(go);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject go in boids)
            flockCenter += go.GetComponent<OBoid>().position;
        count = boids.Count;
        flockCenter /= (count - 1);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            GameObject go = Instantiate(boidPrefab, p, Quaternion.identity);
            go.GetComponent<OBoid>().targetObject = targetObject;
            go.GetComponent<OBoid>().position = p;
            boids.Add(go);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            targetObject.transform.position = p;
        }
    }
}
