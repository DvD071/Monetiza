using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Referências e Configurações Gerais")]
    public Transform player;               // Referência ao jogador
    public Transform rayOrigin;            // Ponto de origem do Raycast
    public LayerMask playerLayer;          // Layer do jogador (para raycast e overlap)

    [Header("Configurações de Perseguição e Ataque")]
    public float moveSpeed = 3f;           // Velocidade de movimento ao perseguir
    public float attackRange = 1.5f;       // Distância para iniciar o ataque
    public float rayDistance = 10f;        // Distância do Raycast para ataque
    // Temporizadores para ataque
    private float detectionTimer = 0f;     // Tempo que o jogador foi detectado pelo raycast
    private float attackCooldown = 0f;     // Cooldown entre ataques

    [Header("Configurações de Patrulha")]
    public float patrolDistance = 5f;      // Distância de patrulha para cada lado
    public float patrolSpeed = 2f;         // Velocidade durante a patrulha
    private Vector2 startPosition;         // Posição inicial do inimigo
    private bool movingRight = true;       // Direção atual da patrulha

    [Header("Detecção de Player via Overlap")]
    public float detectionRadius = 3f;     // Raio do Overlap que inicia a perseguição
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

        // Verifica se o Player está na área de detecção (OverlapCircle)
        bool playerInDetectionArea = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer) != null;

        if (playerInDetectionArea)
        {
            // Se detectado, persegue o Player e tenta atacar
            ChaseAndAttack();
        }
        else
        {
            // Caso contrário, reseta o timer de ataque e patrulha
            detectionTimer = 0f;
            Patrol();
        }
    }

    void ChaseAndAttack()
    {
        // Movimento em direção ao Player
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        // Determina a origem do Raycast (usa rayOrigin se atribuído, senão a posição do inimigo)
        Vector2 origin = rayOrigin ? rayOrigin.position : transform.position;
        // Converte a posição do Player para Vector2
        Vector2 playerPos = new Vector2(player.position.x, player.position.y);
        Vector2 direction = (playerPos - origin).normalized;

        // Lança o Raycast a partir do origin definido
        bool rayDetected = Physics2D.Raycast(origin, direction, rayDistance, playerLayer);

        // Acumula ou reseta o tempo de detecção conforme o resultado do Raycast
        if (rayDetected)
        {
            detectionTimer += Time.deltaTime;
        }
        else
        {
            detectionTimer = 0f;
        }

        // Verifica se o inimigo está próximo o suficiente para atacar
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
        // Calcula o destino da patrulha com base na direção atual
        float targetX = movingRight ? startPosition.x + patrolDistance : startPosition.x - patrolDistance;
        Vector2 targetPosition = new Vector2(targetX, transform.position.y);

        // Move o inimigo em direção ao destino
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);

        // Quando atinge o destino, inverte a direção
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingRight = !movingRight;
        }
    }

    void Attack()
    {
        // Aqui você pode colocar a lógica de dano ao jogador
        Debug.Log("Inimigo ataca o jogador!");
        if (attackCooldown > 0)
        {
            vidaPlayer.GetComponent<VidaPlayer>().TakeDamage(danoEnemy);
        }
    }

    // Desenha Gizmos para visualização na cena
    private void OnDrawGizmos()
    {
        // Gizmo do Raycast para ataque (vermelho)
        Vector2 origin = rayOrigin ? rayOrigin.position : transform.position;
        Vector2 direction = player ? ((Vector2)player.position - origin).normalized : Vector2.right;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + direction * rayDistance);

        // Gizmo da área de detecção via Overlap (azul)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Gizmo dos limites de patrulha (verde)
        Gizmos.color = Color.green;
        // Se estiver em PlayMode, use a posição inicial; senão, use a posição atual para visualização
        Vector2 originPatrol = Application.isPlaying ? startPosition : (Vector2)transform.position;
        Vector2 leftLimit = new Vector2(originPatrol.x - patrolDistance, transform.position.y);
        Vector2 rightLimit = new Vector2(originPatrol.x + patrolDistance, transform.position.y);
        Gizmos.DrawLine(leftLimit, rightLimit);
    }
}
