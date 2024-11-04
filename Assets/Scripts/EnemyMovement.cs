using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; // Radio de movimiento de wandering
    public float detectionRange = 10f; // Rango en el cual el enemigo detecta al jugador
    public Transform player; // Referencia al transform del jugador
    public Transform centrePoint; // Centro del area de movimiento wandering

    private enum State { Wandering, Seeking }
    private State currentState = State.Wandering;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Wandering:
                Wander();
                DetectPlayer();
                break;
            case State.Seeking:
                SeekPlayer();
                break;
        }
    }

    void Wander()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
            }
        }
    }

    void DetectPlayer()
    {
        // Verificar si el jugador est� dentro del rango de detecci�n
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            // Realizar un Raycast hacia el jugador para verificar la l�nea de visi�n
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
            {
                if (hit.transform == player)
                {
                    currentState = State.Seeking; // Cambiar a estado de b�squeda
                }
            }
        }
    }

    void SeekPlayer()
    {
        agent.SetDestination(player.position);

        // Si el jugador sale del rango de detecci�n, regresar a wandering
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange)
        {
            currentState = State.Wandering;
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    void OnDrawGizmosSelected()
    {
        // Dibujar un c�rculo en el rango de detecci�n del jugador en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
