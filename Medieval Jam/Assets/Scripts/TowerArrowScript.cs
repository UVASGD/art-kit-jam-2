using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArrowScript : MonoBehaviour
{
    public enum ArrowState {
        FLYING,
        HIT
    }
    public ArrowState m_state;
    public float m_damage;
    public GameObject m_src; // the entity who fired me

    private float m_spawnTime;
    private float m_despawnTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_state = ArrowState.FLYING;
        m_spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - m_spawnTime > m_despawnTime) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision) {
        
        if (m_state == ArrowState.FLYING) {
            IDamageable other_D = collision.gameObject.GetComponent<IDamageable>();
            if (other_D != null) {
                other_D.DealDamage(m_src!=null ? m_src : gameObject, m_damage);
            }

            m_state = ArrowState.HIT;
            m_spawnTime = Time.time;
        }
    }
}
