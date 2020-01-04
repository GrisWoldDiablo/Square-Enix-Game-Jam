using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.y);
            var mray = _camera.ScreenPointToRay(screenPoint);
            var mrot = Quaternion.LookRotation(mray.direction, this.transform.up);
            
            Destroy(Instantiate(_ball, _camera.transform.position, mrot, this.transform),5.0f);
        }
    }
}
