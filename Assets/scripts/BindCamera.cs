using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindCamera : MonoBehaviour {
    public GameObject character;
    public float stretchDistance = 0.1F;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero; // cache for smooth moving

    // Use this for initialization
    void Start () {
    }
	
    void Update()
    {
        var camera = this.GetComponent<Camera>();
        Vector3 point = camera.WorldToViewportPoint(character.transform.position);
        Vector3 delta = character.transform.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }

}
