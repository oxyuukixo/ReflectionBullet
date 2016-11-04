using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    RaycastHit2D ray;

    Vector2 FireDir = new Vector2(1,0);

    public GameObject Bullet;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        float XAxis = Input.GetAxis("Horizontal");
        float YAxis = Input.GetAxis("Vertical");

        if(XAxis != 0f)
        {
            if (Mathf.Abs(XAxis) >= 1)
            {
                FireDir.x = Mathf.Abs(FireDir.x) * Mathf.Sign(XAxis);
            }
        }

        if (YAxis != 0f)
        {
            FireDir = Quaternion.Euler(0, 0, YAxis * Mathf.Sign(FireDir.x)) * FireDir;
        }

        if(Input.GetKeyDown("joystick button 0"))
        {
            GameObject BulletObj = Instantiate(Bullet);

            BulletObj.transform.position = transform.position;

            BulletObj.GetComponent<Rigidbody2D>().velocity = FireDir * 20;
        }

        ray = Physics2D.Raycast(transform.position, -Vector2.up);

        Debug.DrawRay(transform.position,FireDir * 5000, new Color(255,0,0));
    }
}
