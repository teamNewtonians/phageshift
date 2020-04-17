using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VirusControl : MonoBehaviour
{
  public Transform chaseTarget;
  public NavMeshAgent navMeshAgent;
  public bool isDead;
  public int health;
  public Animator anim;
  public float counter;

  void Start()
  {
    anim = GetComponent<Animator>();
    isDead = false;
    health = 5;
    chaseTarget = GameObject.FindWithTag("Player").transform;
  }

  void Update()
  {
    navMeshAgent.destination = chaseTarget.position;
    navMeshAgent.isStopped = false;
    if(health <= 0)
    {
      counter += Time.deltaTime;
      anim.SetTrigger("playDeath");
      if(counter >= 1f)
      {
        isDead = true;
      }
    }

  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Projectile") {
      health--;
      Destroy(other.gameObject);
    }
  }
}
