using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCanva : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Transform canvasPosition;
    [SerializeField]
    private Vector3 canvasOffset = new Vector3(0, 1, 0);
    // Update is called once per frame
    void Update()
    {
        canvas.transform.rotation = Quaternion.identity;

        // Update canvas position
        if (canvas != null && canvasPosition != null)
        {
            canvas.transform.position = canvasPosition.position + canvasOffset; // Apply offset
        }
    }
}
