using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectsButton : MonoBehaviour
{
    public void OnClick()
    {
        ObjectPooler.SharedInstance.ResetSceneObjects();
    }
}
