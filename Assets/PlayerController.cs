using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 move = Vector3.zero;
    Vector2 rotate;
    public float speed = 5f;
    public float rotateSpeed = 10f;
    public Transform cameraTransform; // ī�޶��� Transform
    public float cameraPitch = 0f;    // ī�޶��� ���� ȸ�� ���� (��/�Ʒ�)
    public float maxCameraAngle = 80f; // ī�޶��� �ִ� ȸ�� ����

    void Start()
    {
        cameraTransform=GameManager.Instance.playerCam.transform;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        CameraSet();
        Moving();
    }

    private void Moving()
    {
        if (move != Vector3.zero)
        {
            // �̵� ó��: ȸ���� ������ �������� �̵� ���͸� ���
            Vector3 moveDirection = rb.rotation * move.normalized;
            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        }
    }

    private void CameraSet()
    {
        // ȸ�� ó�� (ĳ���� �¿� ȸ��)
        if (rotate.x != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, rotate.x * rotateSpeed * Time.fixedDeltaTime, 0);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        // ī�޶� ȸ�� ó�� (��/�Ʒ� ȸ��)
        if (rotate.y != 0)
        {
            cameraPitch -= rotate.y * rotateSpeed * Time.fixedDeltaTime; // ���콺 y���� �ݴ�� ����
            cameraPitch = Mathf.Clamp(cameraPitch, -maxCameraAngle, maxCameraAngle); // ȸ�� ���� ����
            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 inputMove = value.Get<Vector2>();

        // �̵� ���� ���: Forward ����� Right ������ ����
        move = new Vector3(inputMove.x, 0, inputMove.y);
    }

    private void OnLook(InputValue value)
    {
        rotate = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.transform.forward, out RaycastHit hit, 10f)){
            Debug.Log("shoot");
            if (hit.transform.CompareTag("Enemy"))
            { 
                hit.transform.GetComponent<Enemy>().EnemyHealth -= 3f;
               
            }
        }
    }
   
}
