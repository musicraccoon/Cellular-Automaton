using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CameraController : MonoBehaviour
{
    private Camera _camera;

    private const int MoveSpeed = 20;
    private const int ScrollSpeed = 200;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f )
        {
            _camera.orthographicSize += -Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed * Time.deltaTime;
        }
        
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            var newPosition = new Vector3();
            newPosition.x = Input.GetAxis("Mouse X") * MoveSpeed * Time.deltaTime;
            newPosition.y = Input.GetAxis("Mouse Y") * MoveSpeed * Time.deltaTime;
            transform.Translate(-newPosition);
        }
    }
}
