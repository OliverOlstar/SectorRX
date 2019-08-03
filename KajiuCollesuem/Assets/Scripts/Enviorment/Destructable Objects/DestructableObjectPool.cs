using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjectPool : MonoBehaviour
{
    public void getObjectFromPool(GameObject pPrefab, Transform pTransform)
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        GameObject destructable = null;
        
        //See if a matchig object already exist that can be used
        foreach(Transform child in children)
        {
            if (child.gameObject.name == pPrefab.name + "(Clone)" && child.gameObject.activeSelf == false)
            {
                destructable = child.gameObject;
                destructable.GetComponent<Rigidbody>().velocity = Vector3.zero;
                destructable.SetActive(true);
                break;
            }
        }

        //If no object already exists instantiate it
        if (destructable == null)
        {
            destructable = Instantiate(pPrefab);
            destructable.transform.SetParent(this.transform);
        }

        setObject(destructable);
    }

    private void setObject(GameObject pObject)
    {
        pObject.transform.position = transform.position;
        pObject.transform.rotation = transform.rotation;
        pObject.transform.localScale = transform.localScale;
    }
}
