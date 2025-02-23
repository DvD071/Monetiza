using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Refer�ncias e Configura��es Gerais")]
    public Transform player;               // Refer�ncia ao jogador
    public Transform rayOrigin;            // Ponto de origem do Raycast
    public LayerMask playerLayer;          // Layer do jogador (para raycast e overlap)

    [Header("Configura��es de Persegui��o e Ataque")]
    public float moveSpeed = 3f;           // Velocidade de movimento ao perseguir
    public float attackRange = 1.5f;       // Dist�ncia para iniciar o ataque
    public float rayDistance = 10f;        // Dist�ncia do Raycast para ataque
    // Temporizadores para ataque
    private float detectionTimer = 0f;     // Tempo que o jogador foi detectado pelo raycast
    private float attackCooldown = 0f;     // Cooldown entre ataques

    [Header("Configura��es de Patrulha")]
    public float patrolDistance = 5f;      // Dist�ncia de patrulha para cada lado
    public float patrolSpeed = 2f;         // Velocidade durante a patrulha
    private Vector2 startPosition;         // Posi��o inicial do inimigo
    private bool movingRight = true;       // Dire��o atual da patrulha

    [Header("Detec��o de Player via Overlap")]
    public float detectionRadius = 3f;     // Raio do Overlap que inicia a persegui��o
    public VidaPlayer vidaPlayer;
    [Header("Dano do inimigo")]
    public int danoEnemy=10;

    void Start()
    {
        startPosition = transform.position;
        vidaPlayer = GetComponent<VidaPlayer>();    
    }

    void Update()
    {
        if (player == null)
            return;

        // Verifica se o Player est� na �rea de detec��o (OverlapCircle)
        bool playerInDetectionArea = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer) != null;

        if (playerInDetectionArea)
        {
            // Se detectado, persegue o Player e tenta atacar
            ChaseAndAttack();
        }
        else
        {
            // Caso contr�rio, reseta o timer de ataque e patrulha
            detectionTimer = 0f;
            Patrol();
        }
    }

    void ChaseAndAttack()
    {
        // Movimento em dire��o ao Player
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        // Determina a origem do Raycast (usa rayOrigin se atribu�do, sen�o a posi��o do inimigo)
        Vector2 origin = rayOrigin ? rayOrigin.position : transform.position;
        // Converte a posi��o do Player para Vector2
        Vector2 playerPos = new Vector2(player.position.x, player.position.y);
        Vector2 direction = (playerPos - origin).normalized;

        // Lan�a o Raycast a partir do origin definido
        bool rayDetected = Physics2D.Raycast(origin, direction, rayDistance, playerLayer);

        // Acumula ou reseta o tempo de detec��o conforme o resultado do Raycast
        if (rayDetected)
        {
            detectionTimer += Time.deltaTime;
        }
        else
        {
            detectionTimer = 0f;
        }

        // Verifica se o inimigo est� pr�ximo o suficiente para atacar
        bool inAttackRange = Vector2.Distance(transform.position, player.position) <= attackRange;

        // Atualiza o cooldown do ataque
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

        // Se estiver em alcance, com o Player detectado por pelo menos 1 segundo e sem cooldown, ataca
        if (inAttackRange && detectionTimer >= 1f && attackCooldown <= 0f)
        {
            Attack();
            attackCooldown = 2f; // Cooldown de 2 segundos entre ataques
        }
    }

    void Patrol()
    {
        // Calcula o destino da patrulha com base na dire��o atual
        float targetX = movingRight ? startPosition.x + patrolDistance : startPosition.x - patrolDistance;
        Vector2 targetPosition = new Vector2(targetX, transform.position.y);

        // Move o inimigo em dire��o ao destino
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);

        // Quando atinge o destino, inverte a dire��o
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingRight = !movingRight;
        }
    }

    void Attack()
    {
        // Aqui voc� pode colocar a l�gica de dano ao jogador
        Debug.Log("Inimigo ataca o jogador!");
        if (attackCooldown > 0)
        {
            vidaPlayer.GetComponent<VidaPlayer>().TakeDamage(danoEnemy);
        }
    }

    // Desenha Gizmos para visualiza��o na cena
    private void OnDrawGizmos()
    {
        // Gizmo do Raycast para ataque (vermelho)
        Vector2 origin = rayOrigin ? rayOrigin.position : transform.position;
        Vector2 direction = player ? ((Vector2)player.position - origin).normalized : Vector2.right;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + direction * rayDistance);

        // Gizmo da �rea de detec��o via Overlap (azul)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Gizmo dos limites de patrulha (verde)
        Gizmos.color = Color.green;
        // Se estiver em PlayMode, use a posi��o inicial; sen�o, use a posi��o atual para visualiza��o
        Vector2 originPatrol = Application.isPlaying ? startPosition : (Vector2)transform.position;
        Vector2 leftLimit = new Vector2(originPatrol.x - patrolDistance, transform.position.y);
        Vector2 rightLimit = new Vector2(originPatrol.x + patrolDistance, transform.position.y);
        Gizmos.DrawLine(leftLimit, rightLimit);
    }
}
