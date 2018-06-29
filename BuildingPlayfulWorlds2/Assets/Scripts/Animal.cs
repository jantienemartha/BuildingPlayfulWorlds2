using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour {

    public enum AnimalState
    {
        Idle, Walking, Fleeing, Eating
    }

    public AnimalState currentState;

    NavMeshAgent navAgent;
    Animator animator;

    float idleTimer = 0f;
    public float idleTime = 4f;
    float eatTimer = 0f;
    public float eatTime = 10f;
    

    public float eatChance = 25f;
    public float soundChance = 30f;

    public float fearRange;
    public float walkSpeed;
    public float runSpeed;

    AudioSource audioSource;

    

	void Start () {
        audioSource = GetComponent<AudioSource>();
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentState = AnimalState.Idle;
	}

    //Animal State Machine
    void Update () {
        if (currentState != AnimalState.Fleeing && InPlayerRange())
        {
            currentState = AnimalState.Fleeing;
            FleeingEntry();
        }

        switch (currentState)
        {
            case AnimalState.Idle:
                IdleDo();
                if (IdleTimePassed())
                {
                    IdleExit();
                    if (GetChance(eatChance))
                    {              
                        currentState = AnimalState.Eating;
                        EatingEntry();
                    }
                    else
                    {
                        currentState = AnimalState.Walking;
                        WalkingEntry();
                    }
                }
                break;
            case AnimalState.Walking:
                WalkingDo();
                if (ReachedDestination())
                {
                    WalkingExit();
                    currentState = AnimalState.Idle;
                    IdleEntry();
                }
                break;
            case AnimalState.Fleeing:
                FleeingDo();
                if (ReachedDestination())
                {
                    FleeingExit();
                    currentState = AnimalState.Idle;
                    IdleEntry();
                }
                break;
            case AnimalState.Eating:
                EatingDo();
                if (EatingTimePassed())
                {
                    EatingExit();
                    currentState = AnimalState.Idle;
                    IdleEntry();
                }
                break;
        }
	}

    void IdleEntry()
    {
        animator.SetTrigger("Idle");
    }

    void IdleDo()
    {
        idleTimer += Time.deltaTime;
    }

    void IdleExit()
    {
        idleTimer = 0;
    }

    void WalkingEntry()
    {
        navAgent.speed = walkSpeed;
        MoveToDestination();
        animator.SetTrigger("Walk");
        if (GetChance(soundChance))
        {
            audioSource.Play();
        }
    }

    void WalkingDo() { }

    void WalkingExit()
    {
        CancelMovement();
    }

    void EatingEntry()
    {
        animator.SetTrigger("Eat");
    }

    void EatingDo()
    {
        eatTimer += Time.deltaTime;
    }

    void EatingExit()
    {
        eatTimer = 0;
    }

    void FleeingEntry()
    {
        navAgent.speed = runSpeed;
        MoveToDestination();
        animator.SetTrigger("Run");
    }

    void FleeingDo()
    {
    }

    void FleeingExit()
    {

    }

    public Vector3 SelectDestination(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius,NavMesh.AllAreas))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public void MoveToDestination()
    {
        Vector3 destination = SelectDestination(20f);
        navAgent.SetDestination(destination);
    }

    public void MoveToDestination(float radius)
    {
        Vector3 destination = SelectDestination(radius);
        navAgent.SetDestination(destination);
    }

    void CancelMovement()
    {
        navAgent.ResetPath();
    }
 



    public bool InPlayerRange()
    {
        return Vector3.Distance(transform.position, Player.player.gameObject.transform.position) <= fearRange;
    }

    public bool EatingTimePassed()
    {
        return eatTimer >= eatTime;
    }

    public bool IdleTimePassed()
    {
        return idleTimer >= idleTime;
    }

    public bool ReachedDestination()
    {
        return navAgent.remainingDistance <= navAgent.stoppingDistance; 
    }

    public bool GetChance(float chance)
    {
        float percentage = Random.Range(0, 100);
        return percentage <= chance;
    }





}
