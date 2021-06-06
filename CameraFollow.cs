using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("UI")]
    public GameObject canvas;
    public GameObject panCanvas;

    [Header("Camera Follow")]
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [Header("Camera Animation")]
    [SerializeField]private bool movedCam = false;
    public Transform endPos;
    [SerializeField]private float speed;
    [SerializeField]private bool level3;

    private void Start()
    {
        if (!level3)
        {
            // Pauses timer
            FindObjectOfType<TimeLeft>().pause = true;
        }
        // Pauses player movement
        FindObjectOfType<PlayerMovement>().canMove = false;

        canvas.gameObject.SetActive(false);
        panCanvas.gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;

        if (transform.position != endPos.position && movedCam == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos.position, step);
        }
        else if (transform.position == endPos.position && movedCam == false)
        {
            movedCam = true;
            canvas.gameObject.SetActive(true);
            panCanvas.gameObject.SetActive(false);
        }

        if (movedCam == true)
        {
            if (!level3)
            {
                // Continues timer 
                FindObjectOfType<TimeLeft>().pause = false;
            }

            // Continues player movement
            FindObjectOfType<PlayerMovement>().canMove = true;

            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;
        }
    }
}
