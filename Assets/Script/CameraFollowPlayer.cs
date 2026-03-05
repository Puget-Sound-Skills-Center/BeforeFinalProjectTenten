using System;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private float followSpeed = 0.1f;
    [SerializeField] private float lookHoldTime = 0.4f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 y;
    [HideInInspector] public PlayerStateList pState;
    PlayerController player;
    private float lookUpTimer;
    private float lookDownTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PlayerController.Instance;
        pState = player.pState;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position + offset, followSpeed);
        LookUpDown();
    }
    void LookUpDown()
    {
        Vector3 targetPosition = player.transform.position + offset;

        if (Input.GetButton("LookUp") && player.Grounded() && !pState.walking)
        {
            lookUpTimer += Time.deltaTime;

            if (lookUpTimer >= lookHoldTime)
            {
                targetPosition += y;
            }
        }
        else
        {
            lookUpTimer = 0;
        }

        if (Input.GetButton("LookDown") && player.Grounded() && !pState.walking)
        {
            lookDownTimer += Time.deltaTime;

            if (lookDownTimer >= lookHoldTime)
            {
                targetPosition -= y;
            }
        }
        else
        {
            lookDownTimer = 0;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed);
    }
}
