using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    enum TowerState {
        NONE,
        IDLE,
        TARGETING,
        FIRING
    }
    public GameObject m_TowerArrowPrefab;
    public GameObject m_fireOrigin;
    public float m_damage = 15f;
    public float m_fireRate = 0.2f;
    public float m_fireVelocity = 10f;
    public float m_targetRange = 15f;

    private TowerState m_state;
    public GameObject m_currentTarget;
    private float m_fireCooldown;
    private float m_lastFireTime;

    private float m_idleTime;

    private FactionManager factionManager;

    // Start is called before the first frame update
    void Start()
    {
        factionManager = FactionManager.GetInstance(this.gameObject);
        m_fireCooldown = 1f / m_fireRate;
        m_currentTarget = null;
        m_state = TowerState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_state) {
        case TowerState.IDLE:
        // occasional re-target 
            if (Time.time - m_idleTime > 1.0f) {
                m_state = TowerState.TARGETING;
            }
        break;
        case TowerState.TARGETING:
            AcquireTarget();
        break;
        case TowerState.FIRING:
            Vector3 horizontalPos = transform.position;
            horizontalPos.y = 0;
            Vector3 horizontalTargetPos = m_currentTarget.transform.position;
            if (Vector3.Distance(horizontalPos, horizontalTargetPos) > m_targetRange) { // if target is out of range, lose target
                m_currentTarget = null;
                m_state = TowerState.TARGETING;
            } else if (Time.time >= m_fireCooldown + m_lastFireTime) { // else, wait for the timer to say it's ok to fire
                m_lastFireTime = Time.time;
                Aim();
                Fire();
            }
        break;
        }

    }
    // Aims at the current target. TODO: add predictive ballistics.
    private void Aim() {
        m_fireOrigin.transform.LookAt(m_currentTarget.transform, Vector3.up);
        m_fireOrigin.transform.Rotate(new Vector3(-30f, 0, 0), Space.Self);
    }
    // Fires one arrow in the direction which m_fireOrigin is pointing. You should aim before calling this.
    private void Fire() {
        GameObject newArrow_GO = Instantiate(m_TowerArrowPrefab, m_fireOrigin.transform.position, m_fireOrigin.transform.rotation);
        Rigidbody newArrow_R = newArrow_GO.GetComponent<Rigidbody>();
        if (newArrow_R != null) {
            newArrow_R.velocity = newArrow_GO.transform.forward * m_fireVelocity;
        }
        TowerArrowScript newArrow_S = newArrow_GO.GetComponent<TowerArrowScript>();
        if (newArrow_S != null) {
            newArrow_S.m_damage = m_damage;
            newArrow_S.m_src = gameObject;
        }
    }


    // Attempts to find a target in range. 
    // If found, sets m_currentTarget to the closest player object and switches to firing state.
    private void AcquireTarget() {
        Vector3 horizontalPos = transform.position;
        horizontalPos.y = 0;

        List<GameObject> players = factionManager.players;
        GameObject closestObj = null;
        float closestObjDistance = float.PositiveInfinity;
        foreach (GameObject player in players) {
            Vector3 horizontalPlayerPos = player.transform.position;
            horizontalPos.y = 0;
            float distToPlayer = Vector3.Distance(horizontalPlayerPos, horizontalPos);
            if (distToPlayer < closestObjDistance) {
                closestObjDistance = distToPlayer;
                closestObj = player;
            }
        }

        if (closestObj == null || closestObjDistance > m_targetRange) {
            m_currentTarget = null;
            m_idleTime = Time.time;
            m_state = TowerState.IDLE;
        } else {
            m_currentTarget = closestObj;
            m_state = TowerState.FIRING;
        }
    }
}
