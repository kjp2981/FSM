using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public bool IsEnemy { get; }

    public void Damage(float dmg, GameObject damageFactor);
}
