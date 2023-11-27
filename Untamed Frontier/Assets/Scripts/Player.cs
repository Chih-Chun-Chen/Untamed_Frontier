using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject playerHead;
    public GameObject playerBody;
    public GameObject gun;
    public GameObject bulletPrefab;

    private Camera cameraComponent;

    private Vector3 cameraPositionOffset;
    private Vector3 cameraRotationOffset;

    private Vector3 bulletSpawnOffset;
    private float bulletSpeed = 100f;
    private float bulletPosition;

    private Vector3 gunPositionOffset;
    private Vector3 gunRotationOffset;

    private float mouseSensitivity = 150f;
    private float moveSpeed = 5f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private int scopeUp = -1;

    void Start()
    {
        cameraComponent = playerCamera.GetComponent<Camera>();
        cameraComponent.fieldOfView = 60f;
        bulletSpawnOffset = new Vector3(0f, 0.28f, 0f);

        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;

        cameraPositionOffset = new Vector3(0, 0.03f, -0.08f);
        gunPositionOffset = new Vector3(0.351f, -0.402f, 0.44f);

        if (playerCamera != null)
        {
            playerCamera.transform.localPosition = cameraPositionOffset;
            playerCamera.transform.localRotation = Quaternion.Euler(cameraRotationOffset);
        }

        if (gun != null)
        {
            gun.transform.localPosition = gunPositionOffset;
            gun.transform.localRotation = Quaternion.Euler(gunRotationOffset);
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

        if (gun != null)
        {
            // Update gun position to be on the right side of the camera
            gun.transform.position = playerCamera.transform.position + playerCamera.transform.right * gunPositionOffset.x + playerCamera.transform.up * gunPositionOffset.y + playerCamera.transform.forward * gunPositionOffset.z;

            // Gun rotation matches the camera rotation
            gun.transform.rotation = playerCamera.transform.rotation;
        }

        // Player movement
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // Move the player in the direction the camera is looking
        Vector3 movement = playerCamera.transform.right * horizontal + playerCamera.transform.forward * vertical;
        // Ignore vertical movement along the y-axis
        movement.y = 0;

        transform.position += movement;

        if (Input.GetMouseButtonDown(0))
        {
            FireBullet(); // Call method to instantiate the bullet
        }

        // Scope in/out
        if (Input.GetMouseButtonDown(1))
        {
            scopeUp *= -1;
            ScopeUp();
        }
    }

    void ScopeUp()
    {
        if (scopeUp == 1)
        {
            cameraComponent.fieldOfView = 13.5f;
            gunPositionOffset = new Vector3(0f, -0.305f, 0.97f);
        }
        else
        {
            cameraComponent.fieldOfView = 60f;
            gunPositionOffset = new Vector3(0.351f, -0.402f, 0.44f);
        }
    }

    void FireBullet()
    {
        Vector3 bulletPosition = playerCamera.transform.position + playerCamera.transform.forward * 1.48f;
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, playerCamera.transform.rotation);
            // Add Rigidbody component to the bullet
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(playerCamera.transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}
