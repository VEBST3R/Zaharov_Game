using UnityEngine;

public class ColliderObj : MonoBehaviour
{
    [SerializeField] private GameObject Sphere;
    [SerializeField] private Material _SphereMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other == Sphere.GetComponent<Collider>())
        {
            _SphereMaterial.color = Color.black;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other == Sphere.GetComponent<Collider>())
        {
            _SphereMaterial.color = Color.green;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Sphere.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
