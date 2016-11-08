using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_EnemyController : MonoBehaviour {

    public int speed;

    public GameObject bullet;

    public GameObject target;

    public GameObject shootposition;

    public int HP;

    // Use this for initialization
    IEnumerator Start () {

        shootposition = gameObject.transform.FindChild("BulletPosition").gameObject;

        while (true)
        {
            // 弾をプレイヤーと同じ位置で発射
            Instantiate(bullet, transform.position, shootposition.transform.rotation);
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
        }
        else
        {
            scale.x = 0.3f;
        }

        transform.localScale = scale;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            HP--;

            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
