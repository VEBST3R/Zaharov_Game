using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rifle : MonoBehaviour
{
    public int ammo = 30;
    public int allAmmo = 0;
    public float fireRate = 10f;
    public float damage = 10f;
    public float range = 10f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    private float nextFireTime = 0f;
    public int magazineSize = 0;
    [SerializeField] private AudioSource Shoot;
    private Text reloadText;
    public GameObject bulletDecalPrefab;
    private void Awake()
    {
        reloadText = GameObject.Find("ReloadText").GetComponent<Text>();
    }
    private void OnEnable()
    {
        PlayerPrefs.SetInt("loadAmmo", ammo);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("AllAmmo", allAmmo);
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            if (ammo > 0)
            {
                Fire();
                ammo--;
                PlayerPrefs.SetInt("loadAmmo", ammo);
                PlayerPrefs.Save();
                nextFireTime = Time.time + 1f / fireRate;
                Shoot.Play();
            }
            else
            {
                if (PlayerPrefs.GetInt("AllAmmo") > 0)
                    StartCoroutine(Reload());
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {

            StartCoroutine(Reload());

        }
    }

    void Fire()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = (hit.point - bulletSpawnPoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(direction));
            bullet.GetComponent<Rigidbody>().velocity = direction * range;
        }
    }
    IEnumerator Reload()
    {
        // Перевіряємо, чи магазин не повний і чи є додаткові боєприпаси
        if (ammo < magazineSize && PlayerPrefs.GetInt("AllAmmo") > 0)
        {
            reloadText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);

            int ammoNeeded = magazineSize - ammo;
            int ammoToLoad = Mathf.Min(ammoNeeded, PlayerPrefs.GetInt("AllAmmo"));

            ammo += ammoToLoad;
            allAmmo = PlayerPrefs.GetInt("AllAmmo") - ammoToLoad;

            PlayerPrefs.SetInt("AllAmmo", allAmmo);
            PlayerPrefs.SetInt("loadAmmo", ammo);
            PlayerPrefs.Save();

            reloadText.gameObject.SetActive(false);
        }
    }
}