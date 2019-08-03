using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DestructableObjectPool : MonoBehaviour
{
    private List<GameObject> myChildren = new List<GameObject>();
    private List<string> myChildrenName = new List<string>();

    void Start()
    {
        //Ensuring it is in zero position to prevent errors
        transform.position = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
    }

    public void getObjectFromPool(GameObject pPrefab, Transform pTransform)
    {
        GameObject destructable = null;

        //See if a matchig object already exist that can be used
        for (int i = 0; i < myChildren.Count; i++)
        {
            if (myChildrenName[i] == pPrefab.name && myChildren[i].activeSelf == false)
            {
                destructable = myChildren[i];
                destructable.SetActive(true);
                
                foreach (DestructablePieces piece in destructable.GetComponentsInChildren<DestructablePieces>())
                {
                    piece.ResetTransform();
                }

                break;
            }
        }

        //If no object already exists instantiate it
        if (destructable == null)
        {
            destructable = Instantiate(pPrefab);
            destructable.transform.SetParent(transform);

            myChildren.Add(destructable);
            myChildrenName.Add(pPrefab.name);
        }

        setObject(destructable, pTransform);
    }

    private void setObject(GameObject pObject, Transform pTransform)
    {
        pObject.transform.position = pTransform.position;
        pObject.transform.rotation = pTransform.rotation;
        pObject.transform.localScale = pTransform.localScale;
    }
}
