using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PermissionRequest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Permission.RequestUserPermission(Permission.Camera);
    }

}
