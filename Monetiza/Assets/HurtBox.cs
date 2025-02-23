using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector3(Combat.SizeX, Combat.SizeY, 0);
        Destroy(gameObject, Combat.LiveTime);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Hit");
        //n funcionou ainda, deve ser pq n tem rigidbody... AMO UNITY :]]]]]]
    }
}
