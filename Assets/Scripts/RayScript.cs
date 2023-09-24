using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    RaycastHit hit;
    float speed = 15f;

    void Update()
    {
        // ����Ű �Է� ó��
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ĳ���� �̵�
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime;
        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.DrawRay(transform.position, transform.forward * speed, Color.blue, 0.3f);
            if (Physics.Raycast(transform.position, transform.forward, out hit, speed))
            {
                hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}
