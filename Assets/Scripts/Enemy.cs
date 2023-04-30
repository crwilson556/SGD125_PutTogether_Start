using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    int currentNode;
    int previousNode;

    //the # of nodes an enemy goes to is the # of node available - their enemyType #
    public int enemyType = 0;

    public enum EnemyState
    {
        patrol,
        chase
    };

    EnemyState enemyState = EnemyState.patrol;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        Randomizer();
        previousNode = currentNode;
       
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyState)
        {
            case EnemyState.patrol: Patrol(); break;
            case EnemyState.chase: Chase(); break;
            default: break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node" | other.tag == "Enemy" && enemyState == EnemyState.patrol)
        {
            Randomizer();
            while (currentNode==previousNode)
            {
                Randomizer();
            }
            previousNode = currentNode;
        }

        /*if (other.tag == "Player")
        {
            enemyState = EnemyState.chase;
        }*/
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyState = EnemyState.patrol;
        }
    }

    void Patrol()
    {
        agent.destination = GameManager.gm.nodes[currentNode].position;
    }

    void Chase()
    {
        agent.destination = GameManager.gm.player.transform.position;
    }

    void Randomizer() //randomly sets enemy destination
    {
        currentNode = Random.Range(0, GameManager.gm.nodes.Length- enemyType);
    }

    //enemny needs to know if player is close, where other enemy is going, and where they are going
}
