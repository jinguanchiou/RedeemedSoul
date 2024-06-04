using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCameraFollow : MonoBehaviour
{
    #region Variables

    private Vector3 _offset;
    [SerializeField] private Transform pixelCamera;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;
    public bool frameStutter = false;

    public int frames = 10;
    int i;

    #endregion

    #region Unity callbacks

    private void Awake() => _offset = transform.position - pixelCamera.position;
    void Start()
    {
        i = frames + 1;
    }

    private void LateUpdate()
    {
        if (frameStutter)
        {
            if (i > frames)
            {
                pixelCamera.GetComponent<Camera>().enabled = true;
                i = 0;
            }
            else
            {
                pixelCamera.GetComponent<Camera>().enabled = false;
            }
            i++;
        }
            Vector3 targetPosition = pixelCamera.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }

    #endregion
}
