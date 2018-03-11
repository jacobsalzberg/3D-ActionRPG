using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    Transform target; //our player
    [SerializeField] // its related to our camera movement
    float smoothing = 5f;

    Vector3 offset; //offset between the player and the camera at the beggining

    private void Awake()
    {
        Assert.IsNotNull(target);
    }
    // Use this for initialization
    void Start () {
        offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos,smoothing*Time.deltaTime);
	}
}
