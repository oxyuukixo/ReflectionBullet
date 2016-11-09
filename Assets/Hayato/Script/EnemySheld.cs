using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySheld : MonoBehaviour {

    public float HP;

    public static bool sheld_flag;

    bool damage_flag;

	// Use this for initialization
	void Start () {
        sheld_flag = true;
        damage_flag = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if(other.gameObject.tag == "Bullet")
    //    {
    //        HP--;
    //        Debug.Log("sheld:" + HP);

    //        if (HP <= 0)
    //        {
    //            Destroy(this.gameObject);

    //            sheld_flag = false;
    //        }
    //    }
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" && damage_flag == true)
        {
            damage_flag = false;
            HP -= other.gameObject.GetComponent<Bullet>().m_Damage;
            Debug.Log("sheld:" + HP);

            if (HP <= 0)
            {
                Destroy(this.gameObject);

                sheld_flag = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        damage_flag = true;
    }
}
