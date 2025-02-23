using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject Hitbox, Orig;
    public Rigidbody2D Rigidbody;
    static public float DamageB, SizeX, SizeY; //tamanho e dano
    static public float LiveTime;//quanto tempo dura a hitbox
    private void Update()
    {
        //Combat aqui, podendo setar atributos da hitbox usar, diferente para cada golpe
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            SizeX = 1;
            SizeY = 0.5f;
            DamageB = 1;
            LiveTime = 0.1f;
            Instantiate(Hitbox, Orig.transform.position, Quaternion.identity);
           
        }
    }
}
