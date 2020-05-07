using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    private float zOffset;
    private Vector3 cameraVelocity;

    [SerializeField]
    private float smoothTime = 0.5f;

    [SerializeField]
    private Transform topLimitTransform;
    [SerializeField]
    private Transform bottomLimitTransform;
    [SerializeField]
    private Transform leftLimitTransform;
    [SerializeField]
    private Transform rightLimitTransform;

    private float leftLimit;
    private float rightLimit;
    private float topLimit;
    private float bottomLimit;

    private Camera myCamera;

    private Vector3 lastOffsetPosition = Vector3.zero;
    private Coroutine lastShakeCoroutine = null;

    public static CameraFollow Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }else
        {
            Destroy(gameObject);
        }
        zOffset = transform.position.z;

        myCamera = GetComponent<Camera>();
        SetCameraLimits();
    }

    private void LateUpdate()
    {
        if (targetTransform != null)
        {
            Vector3 targetPosition = targetTransform.position;
            targetPosition.z = zOffset;

            targetPosition.x = Mathf.Clamp(targetPosition.x, leftLimit, rightLimit);
            targetPosition.y = Mathf.Clamp(targetPosition.y, bottomLimit, topLimit);


            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraVelocity, smoothTime);
        }
    }

    private IEnumerator DoShake(float duration, float range)
    {
        while(duration > 0f)
        {
            transform.localPosition -= lastOffsetPosition;
            lastOffsetPosition = Random.insideUnitSphere * range;
            lastOffsetPosition.z = 0f;
            transform.localPosition += lastOffsetPosition;

            if (duration < 0.5f)
            {
                range *= 0.90f;
            }

            duration -= Time.deltaTime;
            yield return null;
        }
        transform.localPosition -= lastOffsetPosition;
    }

    public void Shake(float duration, float range)
    {
        transform.localPosition -= lastOffsetPosition;
        if(lastShakeCoroutine != null)
        {
            StopCoroutine(lastShakeCoroutine);
        }
        lastShakeCoroutine = StartCoroutine(DoShake(duration, range));
    }

    public void SetCameraLimits()
    {
        float halfHeight = myCamera.orthographicSize;
        float halfWidth = halfHeight * myCamera.aspect;

        leftLimit = leftLimitTransform.position.x + halfWidth;
        rightLimit= rightLimitTransform.position.x - halfWidth;
        bottomLimit = bottomLimitTransform.position.y + halfHeight;
        topLimit= topLimitTransform.position.y - halfHeight;
    }

    public void SetLeftLimit(Transform left)
    {
        leftLimitTransform = left;
        SetCameraLimits();
    }

    public void SetRightLimit(Transform right)
    {
        rightLimitTransform= right;
        SetCameraLimits();
    }

    public void SetTopLimit(Transform top)
    {
        topLimitTransform = top;
        SetCameraLimits();
    }

    public void SetBottomLimit(Transform bottom)
    {
        bottomLimitTransform = bottom;
        SetCameraLimits();
    }
}
