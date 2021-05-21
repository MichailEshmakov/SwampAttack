using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] float _maxRange;
    [SerializeField] float _standartRange;
    [SerializeField] float _standartPushDistance;
    [SerializeField] float _pushingSpeed;
    [SerializeField] float _standartDamage;
    [SerializeField] float _pushingChance;

    private void OnValidate()
    {
        if (_standartRange < 0)
            _standartRange = 0;

        if (_maxRange < _standartRange)
            _maxRange = _standartRange;

        if (_pushingChance > 1)
            _pushingChance = 1;

        if (_pushingChance < 0)
            _pushingChance = 0;
    }

    public override void Shoot(Transform shootPoint)
    {
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, Vector2.left, _maxRange);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                float distanceCoefficient = ComputeDistanceCoefficient(enemy.transform.position, shootPoint.position);
                
                if (Random.Range(0f, 1f) <= _pushingChance)
                {
                    float pushingDistance = _standartPushDistance * distanceCoefficient;
                    enemy.Push(pushingDistance, _pushingSpeed);
                }

                enemy.TakeDamage((int)(_standartDamage * distanceCoefficient));
            }
        }
    }

    private float ComputeDistanceCoefficient(Vector3 enemyPosition, Vector3 shootPositon)
    {
        float dictanceToEnemy = Vector3.Distance(enemyPosition, shootPositon);
        return (_maxRange - dictanceToEnemy) / (_maxRange - _standartRange);
    }
}
