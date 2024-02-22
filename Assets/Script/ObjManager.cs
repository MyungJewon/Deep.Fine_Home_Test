using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{   
    public Mesh mesh; 
    public GameObject obj; 
    public MeshFilter meshFilter; 
    public MeshRenderer meshRenderer;
    void Awake(){
        mesh= new Mesh(); 
        obj= new GameObject("LoadedObject"); 
        meshFilter= obj.AddComponent<MeshFilter>(); 
        meshRenderer= obj.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
    }
}
