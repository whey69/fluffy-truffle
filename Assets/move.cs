using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class move : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] float jumpPower;
    [SerializeField] private bool onGround = true;
    [SerializeField] private GameObject platPrefab;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private List<Material> materials = new List<Material>();
    private Vector3 curPos;
    private Material curMaterial;
    private Rigidbody rb;
    private MeshRenderer mr;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        mr = gameObject.GetComponent<MeshRenderer>();
        curMaterial = materials[5];
    }

    void Update()
    {
        float dir = Input.GetAxis("Horizontal");
        rb.MovePosition(rb.position + new Vector3(speed * -dir * Time.deltaTime, 0, 0));
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            onGround = false;
            rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
        if (gameObject.transform.position.y < -10)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("platform"))
        {
            onGround = true;
            GameObject newPlat = Instantiate(platPrefab);
            curPos += new Vector3(-9 - Random.Range(0, 3), Random.Range(-3, 2));
            newPlat.transform.position = curPos;
            Material material = curMaterial;
            while (material == curMaterial) {
                material = materials[Random.Range(0, materials.Count)];
            }
            newPlat.GetComponent<MeshRenderer>().material = material;
            if (Random.Range(1, 2) == 1)
            {
                GameObject newball = Instantiate(ballPrefab);
                newball.transform.position = curPos + new Vector3(0, 1, 0);
                newball.GetComponent<MeshRenderer>().material = curMaterial != null ? curMaterial : materials[5];
                newball.GetComponent<ballmove>().origin = curPos + new Vector3(0, 1, 0);
            }
            curMaterial = material;
            mr.material.color = col.gameObject.GetComponent<MeshRenderer>().material.color;
        }
    }

    public void Die()
    {
        // thou shall not live
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}