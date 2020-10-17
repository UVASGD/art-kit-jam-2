using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionManager : MonoBehaviour
{
    public List<GameObject> players {get; private set;}
    // Start is called before the first frame update

    // Get the instance of the FactionManager present in your scene.
    public static FactionManager GetInstance(GameObject you) {
        GameObject[] objs = you.scene.GetRootGameObjects();
        foreach (GameObject obj in objs) {
            FactionManager fm = obj.GetComponent<FactionManager>();
            if (fm) {
                return fm;
            }
        }
        Debug.LogWarning("Failed to acquire FactionManager");
        return null;
    }
    void Awake() {
        players = new List<GameObject>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void RegisterPlayerObject(GameObject obj) {
        players.Add(obj);
    }

    public void DeregisterPlayerObject(GameObject obj) {
        players.Remove(obj);
    }
}
