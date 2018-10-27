using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

    public GameObject Mug;
    private List<GameObject> mugs;
    
	void Start () {
        mugs = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            mugs.Add(createMug());
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < mugs.Count; i++)
            {
                Destroy(mugs[i]);
                mugs[i] = createMug();
            }
        }
	}

    private GameObject createMug()
    {
        int posIndex = Random.Range(0, transform.childCount - 1);

        Vector3 mugTransform = transform.GetChild(posIndex).transform.position;
        return Instantiate(Mug, mugTransform, Quaternion.identity);
    }
}
