using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Configurações do Ataque")]
    [Tooltip("Objeto vazio que define a origem do ataque.")]
    public Transform attackPoint;
    [Tooltip("Raio da área de ataque.")]
    public float attackRange = 0.5f;
    [Tooltip("Dano causado pelo ataque.")]
    public int attackDamage = 20;
    [Tooltip("Layer que contém os inimigos.")]
    public LayerMask enemyLayer;

    private SpriteRenderer spriteRenderer;

    public Animator animator;

    void Start()
    {
        // Obtém o SpriteRenderer do jogador para identificar a direção (flip)
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Se você quiser usar vidaEnemy, ela deve ser obtida do inimigo atingido, e não do próprio jogador
        // vidaEnemy = GetComponent<VidaEnemy>();  // Removido, pois não é necessário
    }

    void Update()
    {
        // Atualiza a posição do attackPoint com base na direção do personagem
        AtualizaPosicaoAttackPoint();

        // Ao pressionar a tecla Z, executa o ataque
        if (Input.GetKeyDown(KeyCode.Z) && !Movement.Jumping)
        {
            ExecutaAtaque();
            animator.SetTrigger("Punch");
        }
    }

    void ExecutaAtaque()
    {
        if (attackPoint == null)
        {
            Debug.LogWarning("attackPoint não foi atribuído!");
            return;
        }

        // Detecta todos os inimigos na área definida pelo OverlapCircle
        Collider2D[] inimigosAcertados = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D inimigo in inimigosAcertados)
        {
            Debug.Log("Inimigo atingido: " + inimigo.name);
            // Aplica dano ao inimigo, buscando o componente VidaEnemy dele
            VidaEnemy vidaEnemy = inimigo.GetComponent<VidaEnemy>();
            if (vidaEnemy != null)
            {
                vidaEnemy.TakeDamage(attackDamage);
            }
            else
            {
                Debug.LogWarning("O objeto " + inimigo.name + " não possui o componente VidaEnemy.");
            }
        }
    }

    void AtualizaPosicaoAttackPoint()
    {
        // Garante que o attackPoint seja posicionado de forma relativa ao jogador e seja "flipado" junto
        if (attackPoint == null || spriteRenderer == null)
            return;

        // Supondo que o attackPoint tenha sido posicionado inicialmente com um offset positivo em X
        Vector3 posicaoLocal = attackPoint.localPosition;

        // Se o personagem estiver "flipado", inverte o offset X
        if (spriteRenderer.flipX)
        {
            posicaoLocal.x = -Mathf.Abs(posicaoLocal.x);
        }
        else
        {
            posicaoLocal.x = Mathf.Abs(posicaoLocal.x);
        }

        attackPoint.localPosition = posicaoLocal;
    }

    // Desenha um Gizmo para visualizar a área de ataque na cena
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
