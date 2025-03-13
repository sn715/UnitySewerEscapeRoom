using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float lookSpeedX = 6;
    float lookSpeedY = 3;

    Transform camTrans;
    float xRotation;
    float yRotation;

    Rigidbody rb;
    int moveSpeed = 10;
    float jumpForce = 300;
    public bool grounded = false;

    public GameObject feet;

    public LayerMask groundLayer;
    public Transform feetTrans;
    float groundCheckDist = .5f;

    //public GameObject playerPrefab;
    //public GameObject spawnPlayer;


    void Start()
    {
        camTrans = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxis("Horizontal");
        moveDir *= moveSpeed;
        moveDir.y = rb.linearVelocity.y;
        rb.linearVelocity = moveDir;

        grounded = Physics.CheckSphere(feetTrans.position, groundCheckDist, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(feet.transform.position, groundCheckDist);
    }
    // Update is called once per frame

    //public void Respawn()
    //{
       // Instantiate(playerPrefab, this.transform.position, this.transform.rotation);
    //}

    void Update()
    {
        //if (this.transform.position.y < -30)
        //{
           // Destroy(this.gameObject);
           // FPSPlayer.Respawn();
       //}
       if (this.transform.position.y < -20)
        {
            SceneManager.LoadScene("SampleScene");
        }

        yRotation += Input.GetAxis("Mouse X") * lookSpeedX;
        xRotation -= Input.GetAxis("Mouse Y") * lookSpeedY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        camTrans.localEulerAngles = new Vector3(xRotation, 0, 0);
        transform.eulerAngles = new Vector3(0, yRotation, 0);

        if (grounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
    }
}
