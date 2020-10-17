using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    private BoxCollider spacing;
    public float spaceSize;
    private bool isColliding;

    public float speed = 12f;
    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 5.0f;
    bool isGrounded;
    

    public List<Transform> waypoints = new List<Transform>();
   
    private Transform target;
    private int targetIndex = 0;

    private float minDistance = 0.1f;
    private int lastWaypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[targetIndex];

        spacing = gameObject.AddComponent<BoxCollider>();
        spacing.size = new Vector3(spaceSize, spaceSize);
        spacing.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToWaypoint();
    }

    private void MoveToWaypoint() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float distance = Vector3.Distance(transform.position, target.position);
        CheckDistanceToWaypoint(distance);

        if (!isColliding) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        
    }

    private void CheckDistanceToWaypoint(float currentDistance) {
        if(currentDistance <= minDistance) {
            targetIndex++;
            UpdateTargetWaypoint();
        }
    }

    private void UpdateTargetWaypoint() {
        if (targetIndex < waypoints.Count) {
            target = waypoints[targetIndex];
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("collide");
        if (other.gameObject.CompareTag("AI")) {
            if (isInFrontOf(transform, other.gameObject.transform)) {
                isColliding = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("AI")) {
            isColliding = false;
        }
    }

    // TODO: check if other is in front of you
    private bool isInFrontOf(Transform thisTransform, Transform otherTransform) {
        return false;
    }
}
