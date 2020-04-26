using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallHit : MonoBehaviour
{
    public bool isDead;
    public int health;
    //public Animator anim;
    public float counter;

    void Start()
    {
        //anim = GetComponent<Animator>();
        isDead = false;
        health = 3;
    }

    void Update()
    {
        if (health <= 0)
        {
            counter += Time.deltaTime;
            //anim.SetTrigger("playDeath");
            if (counter >= 1f)
            {
                isDead = true;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            health--;
            //anim.SetTrigger("Damage");
            Destroy(other.gameObject);
        }
    }
}
