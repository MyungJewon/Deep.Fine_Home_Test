using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman;


public class LoaderModuleAsync : MonoBehaviour
{
    public GameObject LoadAsset(string assetName)
    {
        GameObject loadedAsset = new OBJLoader().Load(assetName);

        return loadedAsset;
    }
}
