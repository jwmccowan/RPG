using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public float speed = 3f;
    public Transform toFollow;
    Transform _transform;
    
    void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position = Vector3.Lerp(_transform.position, toFollow.position, speed * Time.deltaTime);
    }
}
