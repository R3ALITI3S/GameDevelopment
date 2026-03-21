using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float aggroDistance;
    public float attackDistance;
    public float attackDuration;
    private GameObject player;
    public float moveSpeed;
    public int damage;
    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (playerDistance <= attackDistance)
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer(attackDuration));
            }
        }
        else if (playerDistance <= aggroDistance && playerDistance >= attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private IEnumerator AttackPlayer(float seconds)
    {
        isAttacking = true;
        yield return new WaitForSeconds(seconds);
        DamagePlayer(StatsManager.Instance.enemyDamage);
        isAttacking = false;
    }

    private void DamagePlayer(int damage)
    {
        //player.GetComponent<playerController>(); Change to where the player stats will be.
        //player.health -= damage;
    }
}
