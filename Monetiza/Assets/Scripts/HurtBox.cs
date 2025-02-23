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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        if (collision.gameObject.tag!="Player" )
        {

            Debug.Log("Bateu Certo");
        }
    }
}
