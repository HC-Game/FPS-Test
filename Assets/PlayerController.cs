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
    public Transform cameraTransform; // 카메라의 Transform
    public float cameraPitch = 0f;    // 카메라의 현재 회전 각도 (위/아래)
    public float maxCameraAngle = 80f; // 카메라의 최대 회전 각도

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
            // 이동 처리: 회전된 방향을 기준으로 이동 벡터를 계산
            Vector3 moveDirection = rb.rotation * move.normalized;
            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        }
    }

    private void CameraSet()
    {
        // 회전 처리 (캐릭터 좌우 회전)
        if (rotate.x != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, rotate.x * rotateSpeed * Time.fixedDeltaTime, 0);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        // 카메라 회전 처리 (위/아래 회전)
        if (rotate.y != 0)
        {
            cameraPitch -= rotate.y * rotateSpeed * Time.fixedDeltaTime; // 마우스 y값을 반대로 적용
            cameraPitch = Mathf.Clamp(cameraPitch, -maxCameraAngle, maxCameraAngle); // 회전 각도 제한
            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 inputMove = value.Get<Vector2>();

        // 이동 벡터 계산: Forward 방향과 Right 방향을 결합
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
