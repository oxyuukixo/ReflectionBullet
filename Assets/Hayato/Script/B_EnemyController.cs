using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_EnemyController : Enemy{

    public GameObject bullet;

    public GameObject shootposition;

    int direction = -1;

    bool shot_flag = false;

    float shot_time;
    float time = 0.0f;

    // Use this for initialization
    protected override void Start() {

        base.Start();

        damage_flag = true;

        shot_time = 1.0f;

        shootposition = gameObject.transform.FindChild("BulletPosition").gameObject;   
    }
	
	// Update is called once per frame
	void Update () {

        time = time + Time.deltaTime;

        if(time >= shot_time)
        {
            if (shot_flag == true)
            {
                bullet.GetComponent<Bullet>().SpawnBullet(transform.position, new Vector2(direction, 0));

                time = 0;
            }
        }

	}

    void OnWillRenderObject()
    {
        if (Camera.current.name != "SceneCamera" && Camera.current.name != "PreviewCamera")
        {
            Vector3 scale = transform.localScale;
            float dx = (target.transform.position.x - transform.position.x);

            shot_flag = true;

            if (dx > 0.1f)
            {
                scale.x = -0.3f;
                direction = 1;
            }
            else
            {
                scale.x = 0.3f;
                direction = -1;
            }

            transform.localScale = scale;
        }
        //else
        //{
        //    shot_flag = false;
        //}
    }


    //        void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Bullet")
    //    {
    //        HP -= other.gameObject.GetComponent<Bullet>().m_Damage;

    //        if (HP <= 0)
    //        {
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Bullet" && damage_flag == true && other.gameObject.GetComponent<Bullet>().m_Type == Bullet.BulletType.Penetration)
    //    {
    //        damage_flag = false;
    //        HP -= other.gameObject.GetComponent<Bullet>().m_Damage;
            

    //        if (HP <= 0)
    //        {
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    damage_flag = true;
    //}
}
