using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject player;

    public float start_x;
    public float start_y;

    public float end_x;
    public float end_y;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -1);

        if (transform.position.x < start_x)
        {
            transform.position = new Vector3(start_x, player.transform.position.y, -1);
        }

        if (transform.position.x >= end_x)
        {
            transform.position = new Vector3(end_x, player.transform.position.y, -1);
        }

        if (transform.position.y < start_y)
        {
            transform.position = new Vector3(transform.position.x, start_y, -1);
        }

        if (transform.position.y >= end_y)
        {
            transform.position = new Vector3(transform.position.x, end_y, -1);
        }
    }
}
