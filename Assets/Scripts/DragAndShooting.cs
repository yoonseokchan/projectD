using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShooting : MonoBehaviour
{
    private bool isDragging = false;
    private Rigidbody rb;
    private Camera mainCamera;

    public float maxThrowForce = 500.0f; // 최대 던지는 힘 제한값

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float distance;

            if (plane.Raycast(ray, out distance))
            {
                Vector3 newPosition = ray.GetPoint(distance);
                rb.MovePosition(newPosition);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    rb.isKinematic = false; // 특정 물체의 Kinematic 모드를 비활성화
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;

            // 드래그 시작 위치와 현재 위치의 차이를 계산하여 역방향 벡터로 사용
            Vector3 throwDirection = -transform.up;

            // 힘의 크기를 제한 (최대 던지는 힘 제한값 사용)
            float throwForceMagnitude = maxThrowForce;
            rb.AddForce(throwDirection * throwForceMagnitude, ForceMode.Impulse);
        }
    }
}
