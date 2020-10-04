using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // Damages the entity. Returns true if it "died".
    bool DealDamage(GameObject src, float damage);
}
