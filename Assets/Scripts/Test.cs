using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public GameObject obj;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.black);
                print(hit.transform.name);
                Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), hit.point, Quaternion.identity);
            }
            if (Input.GetMouseButtonDown(1))
            {
                hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
