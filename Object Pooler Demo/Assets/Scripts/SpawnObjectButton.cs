using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectButton : MonoBehaviour
{
    public void OnClick(string objectName)
    {
        var GO = ObjectPooler.SharedInstance.GetPooledObject(objectName);

        if(GO == null)
        {
            Debug.Log("MAX OBJECTS REACHED. PLEASE RESET THE OBJECTS");
            return;
        }

        GO.transform.position = new Vector3(0,5,0);
        GO.SetActive(true);
    }
}
