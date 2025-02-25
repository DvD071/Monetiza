using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool CantUp, CantDown, Ignore = false;
    
    public static bool Jumping = false, CanMove = true, andando = false, Pulando = false;
    public float HMove, VMove, VVelo;
    public float SetYPos;
    public GameObject Shadown;
    public static Animator animator;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtém o SpriteRenderer
    }
    void Update()
    {
        //isso é so pa pegar a sombra e botar no lugar certin, para consegui a pespectiva melhor
        if (Jumping)
        {
            Shadown.transform.position = new Vector3(transform.position.x, SetYPos - 0.5f, 0);
        }
        else 
        {
            Shadown.transform.position = new Vector3(transform.position.x, transform.position.y -0.5f, 0);
        }

        //salvo a altura q o pulo começou(SetYPos), e faço uma variavel que fica diminuindo como fosse a gravidade
        if(Input.GetAxisRaw("Jump") > 0 && CanMove)
        {
            if (!Jumping)
            {
                SetYPos = transform.position.y;
                VVelo = 3;
            }
            Jumping = true;
            Ignore = true;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        HMove = horizontal * 3.5f * Time.deltaTime;
        VMove = vertical * 2.5f * Time.deltaTime;

        // Verifica se o personagem está se movendo em qualquer direção
        andando = (horizontal != 0f || vertical != 0f);
        animator.SetBool("Walking", andando);

        if(Jumping && VVelo > 0) 
        {
            animator.SetBool("Jumping", true);
        }
        else 
        {
            animator.SetBool("Jumping", false);
        }
        if(Jumping && VVelo < 0) 
        {
            animator.SetBool("Faling", true);
        }
        else 
        {
            animator.SetBool("Faling", false);
        }

        // Flipa o sprite com base no movimento horizontal
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }  

        if (Jumping)
        {
            if (transform.position.y >= SetYPos)
            {
                VMove = VVelo * Time.deltaTime;
            }
            else 
            {
                Jumping = false;
                Ignore = false;
            }
            VVelo -= 0.1f;
        }
        else
        {
            VMove = Input.GetAxisRaw("Vertical") * 2.5f * Time.deltaTime;
        }

        //colisão ativa aqui
        if (!Ignore)
        {
            if (CantUp && VMove > 0)
            {
                VMove = 0;
            }
            if (CantDown && VMove < 0)
            {
                VMove = 0;
            }
        }

        //CanMove pode ser usado para parar o player em outro script, para ativar ataques e talz, do que eu testei, não vai parar no ar :]
        if(!CanMove) 
        {
            if (Jumping)
            {
                transform.Translate(new Vector2(0, VMove));
            }
            else 
            {
                transform.Translate(new Vector2(0, 0));
            }
        }
        else 
        {
            transform.Translate(new Vector2(HMove, VMove));
        }
    }
    //Comentei pq estava bugando 
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    //'colição' pq so usar so usar o rigdbody pa parar ficou bugando
    //    if(collision.transform.position.y > transform.position.y) 
    //    {
    //        CantUp = true;
    //    }
    //    if (collision.transform.position.y < transform.position.y)
    //    {
    //        CantDown = true;
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    CantUp = false;
    //    CantDown = false;
    //}
}
