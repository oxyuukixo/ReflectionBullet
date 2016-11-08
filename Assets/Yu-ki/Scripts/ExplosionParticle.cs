using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticle : MonoBehaviour {

    private float mLength;
    private float mCur;

    // Use this for initialization
    void Start () {
        Animator animOne = GetComponent<Animator>();
        AnimatorStateInfo infAnim = animOne.GetCurrentAnimatorStateInfo(0);
        mLength = infAnim.length;
        mCur = 0;
    }
	
	// Update is called once per frame
	void Update () {

        mCur += Time.deltaTime;
        if (mCur > mLength)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
