using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy {

    bool moveflag;

    void Start()
    {
        moveflag = true;
        damage_flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void OnWillRenderObject()
    {
#if UNITY_EDITOR

        if (Camera.current.name != "SceneCamera" && Camera.current.name != "PreviewCamera")

#endif
        {
            Vector2 enemyPos = this.gameObject.transform.localPosition;
            Vector3 scale = transform.localScale;

            float vx = target.transform.position.x - enemyPos.x;
            float vy = target.transform.position.y - enemyPos.y;
            float dx, radian;

            // Mathf.Atan2
            radian = Mathf.Atan2(enemyPos.y, vx); // ２つの座標から角度を計算
            dx = Mathf.Cos(radian) * speed; // Sin,Cosを使用してその方向へ移動

            if (moveflag == true)
            {
                // 移動制御
                if (Mathf.Abs(vx) < 0.1f)
                {
                    moveflag = false;
                }

                // 移動を反映
                enemyPos.x += dx;
            }

            if (moveflag == false)
            {
                // 移動を反映
                enemyPos.x += speed;

                if (vy > 0.1f)
                {
                    moveflag = true;
                }
            }

            if (dx >= 0)
            {
                // 右方向に移動中
                scale.x = -0.3f; // そのまま（右向き）
            }
            else {
                // 左方向に移動中
                scale.x = 0.3f; // 反転する（左向き）
            }
            // 代入し直す
            transform.localScale = scale;

            this.gameObject.transform.localPosition = enemyPos;
        }

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
    //        Debug.Log(HP);

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
