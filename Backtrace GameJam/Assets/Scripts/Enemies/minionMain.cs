using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class minionMain : MonoBehaviour
{

    public GameObject player;
    public float persueDistance, attackDistance;
    Transform originalPos;

    float timePassed;
    public float waitingTime = 2;
    public int damage = 10;

    public int EnemyHealth = 20;

    Animator animator;

    NavMeshAgent navMeshAgent;
    bool once = false;
    bool returning = false;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        originalPos = transform;
        timePassed = Mathf.Infinity;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(player.transform);


        if (EnemyHealth > 0)
        {

            if (Vector3.Distance(transform.position, player.transform.position) <= persueDistance)
            {
                navMeshAgent.destination = player.transform.position + new Vector3(-2, 0 - 2);
                animator.SetBool("walking", true);
            }
            else
            {
                navMeshAgent.destination = originalPos.position;
                animator.SetBool("walking", false);
            }


            if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance && timePassed >= waitingTime)
            {
                timePassed = 0;
                animator.SetTrigger("attack");
                GameManager.instance.health -= damage;
                audioSource.Play();
            }


            timePassed += Time.deltaTime;

        }

        if(EnemyHealth <= 0 && !once)
        {
            once = true;
            animator.SetTrigger("died");
            StartCoroutine(deleteEnemy());
        }
           
        }


    IEnumerator deleteEnemy()
    {
        yield return new WaitForSeconds(3.5f);
        GameObject.Find("hit").GetComponent<playerHit>().enemyInSight = null;
        Destroy(gameObject);

    }


    }


  


