using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MatrixContoller : MonoBehaviour
{
    
    private GameObject[,,] cubes;
    public int cubesPerRow = 3;
    public GameObject prefab;
    public float cubeSize = 1f;
    public static event Action<Vector3> OnCubeShrink;

    // Start is called before the first frame update
    void Start()
    {
        float padding = cubeSize / 5;
        Vector3 cubeSizeVector = new Vector3(cubeSize,cubeSize,cubeSize);
        cubes = new GameObject[cubesPerRow, cubesPerRow, cubesPerRow];
        for (int i = 0; i < cubesPerRow; i++) {
            for (int j = 0; j < cubesPerRow; j++) {
                for (int k = 0; k < cubesPerRow; k++) {
                    Vector3 loc = new Vector3(
                        (i * cubeSize) + (i * padding), 
                        (j * cubeSize) + (j * padding), 
                        (k * cubeSize) + (k * padding));                
                    cubes[i,j,k] = (GameObject) Instantiate(prefab, loc, Quaternion.identity);
                    cubes[i,j,k].transform.localScale = cubeSizeVector;
                    cubes[i,j,k].GetComponent<CubeContoller>().OnShrink  += triggerCascade;
                    //cubes[i,j,k].onShrink += triggerCascade;
                }
            }
        }
        //FindObjectOfType<CubeContoller>().onShrink += triggerCascade;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void triggerCascade(Vector3 source) {
        OnCubeShrink(source);
    }

}
