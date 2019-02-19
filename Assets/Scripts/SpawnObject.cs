using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject gameObject;
    public float startTime = 10f;
    public float rate = 4f;
    public int xMin;
    public int xMax;
    public int yMin;
    public int yMax;
    public int zMin;
    public int zMax;

    private Vector3 location;

    // Use this for initialization
    void Start () {
        InvokeRepeating("CreateObject", startTime, rate);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void CreateObject()
    {
        location.x = Random.Range(xMin, xMax);
        location.y = Random.Range(yMin, yMax);
        location.z = Random.Range(zMin, zMax);
        Instantiate(gameObject, location, Quaternion.identity);
    }

}
