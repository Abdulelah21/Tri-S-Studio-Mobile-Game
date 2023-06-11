using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour, ISlowable
{
    private Animator Animator;
    [SerializeField]
    private float StillDelay = 1f;
    private LookAtIK LookAt;
    private NavMeshAgent Agent;

    private Coroutine SlowCoroutine;
    private float BaseSpeed;
    private const string IsWalking = "IsWalking";

    private static NavMeshTriangulation Triangulation;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        LookAt = GetComponent<LookAtIK>();
        if (Triangulation.vertices == null || Triangulation.vertices.Length == 0)
        {
            Triangulation = NavMesh.CalculateTriangulation();
        }

        BaseSpeed = Agent.speed;
    }

    private void Start()
    {
        StartCoroutine(Roam());
        BaseSpeed = Agent.speed;
    }

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Agent.SetDestination(player.transform.position);

        Animator.SetBool(IsWalking, Agent.velocity.magnitude > 0.01f);
        if (LookAt != null)
        {
            LookAt.lookAtTargetPosition = Agent.steeringTarget + transform.forward;
        }
    }

    private IEnumerator Roam()
    {
        WaitForSeconds wait = new WaitForSeconds(StillDelay);

        while (enabled)
        {
            int index = Random.Range(1, Triangulation.vertices.Length);
            Agent.SetDestination(
                Vector3.Lerp(
                    Triangulation.vertices[index - 1],
                    Triangulation.vertices[index],
                    Random.value
                )
            );
            yield return new WaitUntil(() => Agent.remainingDistance <= Agent.stoppingDistance);
            yield return wait;
        }

        // My edit code below based on archero game :) 

        // Get the player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Set the player's position as the destination
        Agent.SetDestination(player.transform.position);

        // Wait until the agent reaches the destination
        yield return new WaitUntil(() => Agent.remainingDistance <= Agent.stoppingDistance);

        // Wait for a while before setting a new destination
        yield return wait;
    }




public void StopMoving()
    {
   /*     if (Agent.enabled)
        {*/
        
/*            Agent.isStopped = true;
*/            Agent.enabled = false;
        StopAllCoroutines();
        /*}*/
    }

    public void Slow(AnimationCurve SlowCurve)
    {
        if (SlowCoroutine != null)
        {
            StopCoroutine(SlowCoroutine);
        }
        if (gameObject.activeInHierarchy) // Check if the game object is active
        {
            SlowCoroutine = StartCoroutine(SlowDown(SlowCurve));
        }
    }

    private IEnumerator SlowDown(AnimationCurve SlowCurve)
    {
        float time = 0;

        while (time < SlowCurve.keys[^1].time)
        {
            Agent.speed = BaseSpeed * SlowCurve.Evaluate(time);
            time += Time.deltaTime;
            yield return null;
        }

        Agent.speed = BaseSpeed;
    }
}

