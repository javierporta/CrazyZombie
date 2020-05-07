using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKey : MonoBehaviour
{
    [SerializeField]
    private Transform newCameraRightLimit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraFollow.Instance.SetRightLimit(newCameraRightLimit);
            Destroy(gameObject);
        }
    }
}
