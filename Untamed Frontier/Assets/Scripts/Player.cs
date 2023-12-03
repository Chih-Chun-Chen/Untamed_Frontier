using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject playerHead;
    public GameObject playerBody;
    public GameObject arm;
    public GameObject bulletPrefab;
    public AudioClip bulletEffect;

    private Image image;
    private Image image1;

    private Animator pistolAnimator;
    private Rigidbody playerRigidbody;
    private Camera cameraComponent;

    private Vector3 cameraPositionOffset;
    private Vector3 cameraRotationOffset;

    private Vector3 bulletSpawnOffset;
    private float bulletSpeed = 100f;
    private float bulletPosition;

    private float jumpForce = 5f;
    private bool isGrounded = true;
    private float sprintSpeed = 6f;
    private float slowSpeed = 1.5f;

    private Vector3 armPositionOffset;
    private Vector3 armRotationOffset;


    private float mouseSensitivity;
    private float moveSpeed = 3f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        mouseSensitivity = MainManager.Instance.fpsSliderValue;
        // Get all Image components in the children of this GameObject
        Image[] images = GetComponentsInChildren<Image>();

        // Loop through them and assign them based on their GameObject's name
        foreach (Image img in images)
        {
            if (img.gameObject.name == "Image")
            {
                image = img;
            }
            else if (img.gameObject.name == "Image1")
            {
                image1 = img;
            }
        }

        pistolAnimator = transform.Find("Pistol_Knife").GetComponent<Animator>();
        pistolAnimator.SetBool("IsIdle", true);
        pistolAnimator.SetBool("IsRun", false);

        playerRigidbody = GetComponent<Rigidbody>();
        cameraComponent = playerCamera.GetComponent<Camera>();
        cameraComponent.fieldOfView = 60f;
        bulletSpawnOffset = new Vector3(0f, 0.28f, 0f);

        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;

        cameraPositionOffset = new Vector3(0, 0.03f, -0.08f);
        //armPositionOffset = new Vector3(0f, -1.68f, -8.89f);

        Vector3 armPosition = arm.transform.position;
        Vector3 bodyPosition = playerBody.transform.position;
        armPositionOffset = armPosition - bodyPosition;

        if (playerCamera != null)
        {
            playerCamera.transform.localPosition = cameraPositionOffset;
            playerCamera.transform.localRotation = Quaternion.Euler(cameraRotationOffset);
        }

        if (arm != null)
        {
            arm.transform.localPosition = armPositionOffset;
            arm.transform.localRotation = Quaternion.Euler(armRotationOffset);
        }


        if (MainManager.Instance.crosshairColorDropdownValue == 0)
        {
            image.color = Color.red;
            image1.color = Color.red;
        }
        else if (MainManager.Instance.crosshairColorDropdownValue == 1)
        {
            image.color = Color.blue;
            image1.color = Color.blue;
        }
        else
        {
            image.color = Color.green;
            image1.color = Color.green;
        }
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update the rotations
        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -72f, 28.5f); // Clamps the vertical rotation

        // Rotate the player's head for up/down look and y-axis rotation
        playerHead.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        if (playerCamera != null)
        {
            // Update camera position
            playerCamera.transform.position = playerHead.transform.position + cameraPositionOffset;
            // Apply rotation to the camera
            playerCamera.transform.rotation = playerHead.transform.rotation;
        }

        if (arm != null)
        {
            // Update arm position to be on the body
            arm.transform.position = playerBody.transform.position + playerBody.transform.right * armPositionOffset.x + playerBody.transform.up * armPositionOffset.y + playerBody.transform.forward * armPositionOffset.z;

            // Get the camera's rotation in Euler angles
            Vector3 cameraRotation = playerCamera.transform.eulerAngles;

            // Keep the arm's current X and Z rotations
            float armXRotation = arm.transform.eulerAngles.x;
            float armZRotation = arm.transform.eulerAngles.z;

            // Apply the camera's Y rotation to the arm, and keep its original X and Z rotations
            arm.transform.rotation = Quaternion.Euler(armXRotation, cameraRotation.y, armZRotation);

        }

        Action();
    }


    void FireBullet()
    {
        Vector3 bulletPosition = playerCamera.transform.position + playerCamera.transform.forward * 1.48f;
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, playerCamera.transform.rotation);
            // Add Rigidbody component to the bullet
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(playerCamera.transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Construction"))
        {
            isGrounded = true;
            pistolAnimator.SetBool("IsJump", false);
        }
    }

    void Action()
    {
        // Player movement
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        bool leftClick = Input.GetMouseButtonDown(0);
        bool leftShift = Input.GetKey(KeyCode.LeftShift);
        bool space = Input.GetKeyDown(KeyCode.Space);

        // Move the player in the direction the camera is looking
        Vector3 movement = playerCamera.transform.right * horizontal + playerCamera.transform.forward * vertical;
        // Ignore vertical movement along the y-axis
        movement.y = 0;

        transform.position += movement;

        if (leftClick)
        {
            FireBullet();

            // Play bullet sound effect
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.PlayOneShot(bulletEffect);
            Destroy(newAudioSource, bulletEffect.length);
        }

        if(leftClick)
        {
            pistolAnimator.SetBool("IsShot", true);
            StartCoroutine(ResetShoot());
        }

        IEnumerator ResetShoot()
        {
            yield return new WaitForSeconds(1f); // Wait for 0.5 seconds

            if (!Input.GetMouseButton(0)) // Check if left click is not held down
            {
                pistolAnimator.SetBool("IsShot", false);
            }
        }

        // Jumping
        if (space && isGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            pistolAnimator.SetBool("IsJump", true);
        }

        // Sprinting
        if (leftShift)
        {
            moveSpeed = sprintSpeed;
            pistolAnimator.SetBool("IsRun", true);
        }
        else if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            moveSpeed = slowSpeed;
            Debug.Log(moveSpeed);
        }
        else
        {
            moveSpeed = 3f; // Reset to normal speed when not sprinting
            pistolAnimator.SetBool("IsRun", false);
        }

        if (vertical != 0 && !leftShift)
        {
            pistolAnimator.SetBool("IsWalk", true);
        }
        else
        {
             pistolAnimator.SetBool("IsWalk", false);
        }
    }
}
