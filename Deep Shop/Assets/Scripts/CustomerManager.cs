using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject positionCorner;
    [SerializeField]
    private GameObject positionAux;
    [SerializeField]
    private GameObject positionOutside;
    [SerializeField]
    private GameObject circleCostumer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject GetPositionCorner()
    {
        return positionCorner;
    }

    GameObject GetPositionOutside()
    {
        return positionOutside;
    }
}
