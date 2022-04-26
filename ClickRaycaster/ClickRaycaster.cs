// ----------------------------------------------------------------------------
// Click raycaster
// 
// Author: streep
// Date:   24/03/2022
// ----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ClickRaycaster : MonoBehaviour
{
    public LayerMask clickable;

    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 50f, clickable))
            {
                Transform objectHit = hit.transform;
                IClickable clickable = objectHit.GetComponent<IClickable>();
                clickable?.onClick();
            }
        }
    }
}
