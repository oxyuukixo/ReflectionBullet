﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyController : MonoBehaviour {

    public GameObject target;
    public float speed = 0.1f; // 移動量

    public int HP;

    bool moveflag;

    // Use this for initialization
    void Start () {
        moveflag = true;
    }
	
	// Update is called once per frame
	void Update () {

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

        if(EnemySheld.sheld_flag == false)
        {
            speed = 0.1f;
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
