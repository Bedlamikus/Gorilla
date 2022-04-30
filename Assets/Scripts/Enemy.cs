using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _radiusView;

    [SerializeField]
    private int _enemySpeed;

    private Player _player;
    private NavMeshAgent _agent;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        if (_player == null)
        {
            throw new System.Exception("Enemy not find gameObject Player!!!");
        }
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            print(this.name + " can't find component NavMeshAgent!!");
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        if (_agent == null)
        {
            return;
        }
        MoovingEnemy(_player.transform.position);
    }

    private void MoovingEnemy(Vector3 direction)
    {
        if ((_player.transform.position - transform.position).magnitude >= _radiusView)
        {
            return;
        }
        transform.LookAt(direction);
        _agent.SetDestination(_player.transform.position);
        Quaternion _quaternion = transform.rotation;
        _quaternion.x = 0;
        _quaternion.z = 0;
        transform.rotation = _quaternion;
    }
}
