using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject playerHead;
    public GameObject gun;

    private Vector3 cameraPositionOffset;
    private Vector3 cameraRotationOffset;

    // Start is called before the first frame update
    void Start()
    {

        cameraPositionOffset = new Vector3(0, 0.03f, -0.39f);
        if (playerCamera != null)
        {
            playerCamera.transform.localPosition = cameraPositionOffset;
            playerCamera.transform.localRotation = Quaternion.Euler(cameraRotationOffset);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCamera != null)
        {
            playerCamera.transform.position = playerHead.transform.position + cameraPositionOffset;
        }
    }


}
