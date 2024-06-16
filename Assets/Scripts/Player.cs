using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public float jumpForce = 2.0f;
    private bool isJumping = false;
    private Rigidbody rb;
    public GameObject playerCamera;
    private float cameraRotationX = 0f;
    private float cameraRotationY = 0f;
    public GameObject[] guns;
    private Text reloadtext;
    public static int health = 100;
    void Start()
    {
        health = 100;
        PlayerPrefs.SetInt("Kills", 0);
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        if (PlayerPrefs.HasKey("Sensivity") != false)
        {
            rotationSpeed = PlayerPrefs.GetFloat("Sensivity");
        }
        else
        {
            Debug.LogError("Sensivity not found");
        }
        guns[0].SetActive(true);
        guns[1].SetActive(false);
        reloadtext = GameObject.Find("ReloadText").GetComponent<Text>();
        reloadtext.gameObject.SetActive(false);
        PlayerPrefs.Save();
    }

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;
        transform.Translate(strafe, 0, translation);

        cameraRotationX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        cameraRotationY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        cameraRotationY = Mathf.Clamp(cameraRotationY, -90f, 90f);
        playerCamera.transform.localEulerAngles = new Vector3(cameraRotationY, 0f, 0f);
        transform.localEulerAngles = new Vector3(0f, cameraRotationX, 0f);

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = true;
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f && reloadtext.gameObject.activeSelf == false)
        {
            foreach (GameObject obj in guns)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(15);
        }

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {

        gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        DieManager.DiePlayer(gameObject);
        gameObject.GetComponent<Player>().enabled = false;
    }
}
