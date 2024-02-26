using System; 
using System.Collections;
using UnityEngine;

public class ballmove : MonoBehaviour
{
    public Vector3 origin;
    private double time;
    void Update()
    {
        gameObject.transform.position = origin + new Vector3((float) Math.Sin(time) * 4, 0, 0);
        time += (1 * (Math.PI)) / 180;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && 
            collision.gameObject.GetComponent<MeshRenderer>().material.color == gameObject.GetComponent<MeshRenderer>().material.color)
        {
            GameObject.Destroy(gameObject);
        }else if (collision.gameObject.CompareTag("Player"))
        {
            // get pranked rofl lmaooo
            collision.gameObject.GetComponent<move>().speed = 0f; 
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ; 
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("platform"))
            {
                obj.GetComponent<BoxCollider>().isTrigger = true;
            }
            // collision.gameObject.GetComponent<move>().Die();
            GameObject.Destroy(gameObject);
        }
    }
}
