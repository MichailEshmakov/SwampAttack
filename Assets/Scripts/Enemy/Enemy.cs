using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;
    
    [SerializeField] private Player _target;

    public Player Target => _target;
    public int Reward => _reward;

    public event UnityAction<Enemy> Dying;

    public void Init(Player target)
    {
        _target = target;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            Dying?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public void Push(float pushingDistance, float pushingSpeed)
    {
        StartCoroutine(Pushing(pushingDistance, pushingSpeed));
    }

    private IEnumerator Pushing(float pushingDistance, float pushingSpeed)
    {
        float pushingResult = 0;
        float previousPushingResult;

        while (pushingResult < pushingDistance)
        {
            previousPushingResult = pushingResult;
            pushingResult = Mathf.MoveTowards(pushingResult, pushingDistance, pushingSpeed * Time.deltaTime);
            transform.position += Vector3.left * (pushingResult - previousPushingResult);
            yield return null;
        }
    }
}
