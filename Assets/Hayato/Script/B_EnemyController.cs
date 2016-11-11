using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_EnemyController : MonoBehaviour {

    public int speed;

    public GameObject bullet;

    public GameObject target;

    public GameObject shootposition;

    public float HP;

    bool damage_flag;

    int direction = -1;

    // Use this for initialization
    IEnumerator Start () {

        damage_flag = true;

        shootposition = gameObject.transform.FindChild("BulletPosition").gameObject;

        while (true)
        {
            bullet.GetComponent<Bullet>().SpawnBullet(transform.position,new Vector2(direction,0));
            // 1秒待つ
            yield return new WaitForSeconds(1.0f);
        }
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 scale = transform.localScale;
        float dx = (target.transform.position.x - transform.position.x);

        if(dx > 0.1f)
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            HP -= other.gameObject.GetComponent<Bullet>().m_Damage;

            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" && damage_flag == true && other.gameObject.GetComponent<Bullet>().m_Type == Bullet.BulletType.Penetration)
        {
            damage_flag = false;
            HP -= other.gameObject.GetComponent<Bullet>().m_Damage;
            

            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        damage_flag = true;
    }
}
