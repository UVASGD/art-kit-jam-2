using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public GameObject m_TowerArrowPrefab;
    public GameObject m_fireOrigin;
    public float m_fireRate = 0.2f;
    public float m_fireVelocity = 10f;
    private float m_fireCooldown;
    private float m_lastFireTime;

    // Start is called before the first frame update
    void Start()
    {
        m_fireCooldown = 1f / m_fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= m_fireCooldown + m_lastFireTime) {
            m_lastFireTime = Time.time;
            Fire();
        }
    }
    private void Fire() {
        GameObject newArrow_GO = Instantiate(m_TowerArrowPrefab, m_fireOrigin.transform.position, m_fireOrigin.transform.rotation);
        Rigidbody newArrow_R = newArrow_GO.GetComponent<Rigidbody>();
        if (newArrow_R) {
            newArrow_R.velocity = newArrow_GO.transform.forward * m_fireVelocity;
        }
    }
}
