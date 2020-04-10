using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VirusControl : MonoBehaviour
{
  public Transform chaseTarget;
  public NavMeshAgent navMeshAgent;
  public bool isDead;

  private int health;

  void Start()
  {
    health = 5;
    isDead = false;
    chaseTarget = GameObject.FindWithTag("Player").transform;
  }

  void Update()
  {
    navMeshAgent.destination = chaseTarget.position;
    navMeshAgent.Resume();
    if(health == 0)
    {
      isDead = true;
    }
  }

  void OnTriggerEnter(Collider other)
  {
        if (other.tag == "Projectile") {
            Debug.Log("hit");
            health--;
            Destroy(other.gameObject);
            //Destroy(this.gameObject);
            //GameObject.Find("playField").GetComponent<fieldGenerator>().score +=1;
        }
  }
}
