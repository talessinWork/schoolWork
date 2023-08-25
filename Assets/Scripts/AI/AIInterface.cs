using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIInterface : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private  bool targetPlayer;
    private Animator bTree;
    public GameObject player;
    private Vector3 lastKnownPos;
    public NavMeshAgent Agent { get => agent;}
    public GameObject Player {get => player;}
    public Vector3 LastKnownPos {get => lastKnownPos; set => lastKnownPos = value; }
    public Animator Animator {get => bTree; set => bTree = value; }
    public StateMachine StateMachine {get => stateMachine;}
    
    [HideInInspector]
    
    Movement movement;
    public Movement Movement {get => movement;}
    
    Damagable health;
    
    [HideInInspector]
    
    public bool movingTowards;
    public int atacks;
    public PatrolPath path;
    [Header("SightValues")]
    public float sightDistance=20f;
    public float fieldOfView = 85f;
    public float eyeHeight;
    [SerializeField]
    private string currentState;    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bTree = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        movement = GetComponent<Movement>();
        health = GetComponent<Damagable>();
        stateMachine = GetComponent<StateMachine>();
        
        agent.updatePosition = false;
        atacks = 0;

        movement.lookAt = null;
    }
    public void InitStateMachine()
    {
         stateMachine.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(agent != null && movement != null)
        {
            atacks %= 3;
            //movement.moveVec = Vector3.zero;
            movement.moveVec = agent.desiredVelocity.normalized;
            if (targetPlayer)
            {
               
                agent.SetDestination(player.transform.position);
                agent.nextPosition = transform.position;
            }
            else if (movingTowards)
            {
                    //movement.moveVec = agent.desiredVelocity.normalized;
                    agent.nextPosition = transform.position;

                    if (!agent.pathPending && agent.remainingDistance < 0.1f)
                    {
                        movingTowards = false;
                    }
            }
            //Updates
            PlayerDistance();
            Health();
        }
    }
    public void TargetPlayer()
    {
        if (agent != null && movement != null)
        {
            movingTowards = false;
            targetPlayer = true;
        }
    }
    public void UnTarget()
    {
        targetPlayer = false;
    }
    public void MoveTo(Vector3 destination)
    {
        if (agent != null && movement != null)
        {
            agent.nextPosition = transform.position;
            targetPlayer = false;
            agent.SetDestination(destination);
            movingTowards = true;
        }
    }
    public void Attack()
    {
        if (agent != null && movement != null)
        {
            movement.lightAttack = true;
            atacks += 1;
            bTree.SetInteger("Attacks", atacks);
        }
    }
    public void Jump()
    {
        if (agent != null && movement != null)
        {
            movement.jump = true;
        }
    }
    public void LongJump()
    {
        if (agent != null && movement != null)
        {
            movement.jump = true;
            movement.longJump = true;
        }
    }
    public void Dash()
    {
        if (agent != null && movement != null)
        {
            movement.jump = true;
            movement.dash = true;
        }
    }
    public void RotateTowards(Transform target)
    {
        if (agent != null && movement != null)
        {
            movement.lookAt = target;
            Invoke("RotateEnd", 0.1f);
        }
    }
    private void RotateEnd()
    {
        if (agent != null && movement != null)
        {
            movement.lookAt = null;
        }
    }
    //update
    private void PlayerDistance()
    {
        bTree.SetFloat("PlayerDistance", Vector3.Distance(player.transform.position, transform.position));
    }
    private void Health()
    {
        if (health != null)
        {
            bTree.SetFloat("Hp", health.hp / health.maxHp);
        }
    }
    public bool CanSeePlayer()
    {
        if(player != null)
        {
            if(bTree.GetFloat("PlayerDistance") < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if(angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    
                    RaycastHit hitInfo = new RaycastHit();
                    if(Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        
                        if(hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                    
                }
            }
            
        }
        return false;
        
    }
}
