using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(AudioSource))]
public class SlenderPlayerController : MonoBehaviour
{
    [Header("Camera")]
    public Camera playerCam;
    private Vector3 originalCamLocalPos;

    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpPower = 0f;
    public float gravity = 10f;

    [Header("Camera Look Settings")]
    public float lookSpeed = 2f;
    public float lookXLimit = 75f;

    [Header("Zoom Settings")]
    public int zoomFOV = 35;
    public int initialFOV = 60;
    public float cameraZoomSmooth = 5f;
    public AudioSource cameraZoomSound;
    private bool isZoomed = false;

    [Header("Head Bobbing")]
    public float walkBobSpeed = 10f;
    public float walkBobAmount = 0.05f;
    public float runBobSpeed = 14f;
    public float runBobAmount = 0.1f;
    private float bobTimer = 0f;

    [Header("Stamina System")]
    public float maxStamina = 6f;
    private float currentStamina;
    public float staminaRegenRate = 1.5f;
    private bool isRunning = false;

    [Header("Sound Settings")]
    public AudioSource playerAudio;
    public AudioClip[] footstepSounds;
    public AudioClip[] breathingSounds; // inhale / exhale variations
    public float stepInterval = 0.5f;
    private float stepTimer = 0f;

    [Header("Breathing Control")]
    public float minBreathInterval = 5f;
    public float maxBreathInterval = 12f;
    private float nextBreathTime = 0f;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool canMove = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerCam != null)
            originalCamLocalPos = playerCam.transform.localPosition;

        ScheduleNextBreath();
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
        HandleZoom();
        HandleHeadBob();
        HandleBreathing();
    }

    void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool wantsToRun = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f;
        isRunning = wantsToRun && controller.isGrounded;

        float speed = isRunning ? runSpeed : walkSpeed;
        float moveX = canMove ? speed * Input.GetAxis("Vertical") : 0;
        float moveZ = canMove ? speed * Input.GetAxis("Horizontal") : 0;

        float moveY = moveDirection.y;
        moveDirection = (forward * moveX) + (right * moveZ);

        // Stamina usage and regen
        if (isRunning)
            currentStamina -= Time.deltaTime;
        else
            currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRegenRate * Time.deltaTime);

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (Input.GetButton("Jump") && controller.isGrounded)
            moveDirection.y = jumpPower;
        else
            moveDirection.y = moveY;

        if (!controller.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);

        HandleFootsteps(speed);
    }

    void HandleLook()
    {
        if (!canMove) return;

        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void HandleZoom()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isZoomed = true;
            if (cameraZoomSound) cameraZoomSound.Play();
        }

        if (Input.GetButtonUp("Fire2"))
        {
            isZoomed = false;
            if (cameraZoomSound) cameraZoomSound.Play();
        }

        float targetFOV = isZoomed ? zoomFOV : initialFOV;
        playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, targetFOV, Time.deltaTime * cameraZoomSmooth);
    }

    void HandleHeadBob()
    {
        if (!controller.isGrounded) return;

        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            bobTimer += Time.deltaTime * (isRunning ? runBobSpeed : walkBobSpeed);
            float bobAmount = isRunning ? runBobAmount : walkBobAmount;

            Vector3 newPos = originalCamLocalPos;
            newPos.y += Mathf.Sin(bobTimer) * bobAmount;
            newPos.x += Mathf.Cos(bobTimer / 2) * bobAmount * 0.5f;
            playerCam.transform.localPosition = newPos;
        }
        else
        {
            playerCam.transform.localPosition = Vector3.Lerp(playerCam.transform.localPosition, originalCamLocalPos, Time.deltaTime * 5f);
            bobTimer = 0f;
        }
    }

    void HandleFootsteps(float speed)
    {
        if (!controller.isGrounded || moveDirection.magnitude < 0.1f) return;

        stepTimer += Time.deltaTime * (speed / walkSpeed);

        if (stepTimer > stepInterval)
        {
            stepTimer = 0f;
            if (footstepSounds.Length > 0)
            {
                AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
                playerAudio.PlayOneShot(clip, 0.7f);
            }
        }
    }

    void HandleBreathing()
    {
        if (Time.time > nextBreathTime)
        {
            if (breathingSounds.Length > 0)
            {
                AudioClip clip = breathingSounds[Random.Range(0, breathingSounds.Length)];
                playerAudio.PlayOneShot(clip, Random.Range(0.4f, 0.8f));
            }
            ScheduleNextBreath();
        }
    }

    void ScheduleNextBreath()
    {
        nextBreathTime = Time.time + Random.Range(minBreathInterval, maxBreathInterval);
    }
}
