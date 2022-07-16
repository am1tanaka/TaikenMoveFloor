using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("左右の移動速度"), SerializeField]
    float walkSpeed = 4;
    [Tooltip("ジャンプ力"), SerializeField]
    float jumpPower = 5f;

    CharacterController chr;
    Vector3 velocity;
    Vector3 startPosition;

    private void Awake()
    {
        chr = GetComponent<CharacterController>();
        startPosition = transform.position;
    }

    void Update()
    {
        velocity.x = walkSpeed * Input.GetAxisRaw("Horizontal");
        velocity.y += Time.deltaTime * Physics.gravity.y;
        chr.Move(Time.deltaTime * velocity);
        if (chr.isGrounded)
        {
            // 着地している
            velocity.y = 0;

            // ジャンプチェック
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpPower;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            Debug.Log($"CLEAR!!");
            ToStart();
        }
        if (other.CompareTag("Miss"))
        {
            ToStart();
        }
    }

    void ToStart()
    {
        chr.enabled = false;
        velocity = Vector3.zero;
        transform.position = startPosition;
        chr.enabled = true;
    }
}
