using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShooting : MonoBehaviour
{
    private bool isDragging = false;
    private Rigidbody rb;
    private Camera mainCamera;

    public float maxThrowForce = 500.0f; // �ִ� ������ �� ���Ѱ�

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
                    rb.isKinematic = false; // Ư�� ��ü�� Kinematic ��带 ��Ȱ��ȭ
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;

            // �巡�� ���� ��ġ�� ���� ��ġ�� ���̸� ����Ͽ� ������ ���ͷ� ���
            Vector3 throwDirection = -transform.up;

            // ���� ũ�⸦ ���� (�ִ� ������ �� ���Ѱ� ���)
            float throwForceMagnitude = maxThrowForce;
            rb.AddForce(throwDirection * throwForceMagnitude, ForceMode.Impulse);
        }
    }
}
