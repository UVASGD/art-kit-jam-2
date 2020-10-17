using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTarget : MonoBehaviour, IDamageable
{
    public float m_health = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool DealDamage(GameObject src, float damage) {
        m_health -= damage;
        if (m_health <= 0f) {
            Destroy(gameObject);
            return true;
        } 
        return false;
    }
}
