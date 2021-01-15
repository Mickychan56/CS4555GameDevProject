using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public float rotationSpeed = 1;
    public Transform Target, Player;
    public GameObject crosshair;

    float mouseX, mouseY;
    float zoomSpeed = 2f;

    Transform Obstruction;

    void Start()
    {
        Obstruction = Target;
        crosshair.SetActive(false);
    }

    private void LateUpdate()
    {
        CamControl();
        viewObstruct(); // So player can see even behind walls

        if (Input.GetMouseButtonDown(1))
        {
            this.transform.localPosition = new Vector3(0.5f, 0, -0.5f);
            crosshair.SetActive(true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            this.transform.localPosition = new Vector3(0, 0, -2f);
            crosshair.SetActive(false);
        }
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        if (!PausedMenu.isPaused)
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
    }

    void viewObstruct()
    {
        RaycastHit hit;
        Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
        {
            if(hit.collider.gameObject.tag != "Player" || hit.collider.gameObject.tag != "Enemy")
            {
                Obstruction = hit.transform;
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                if(Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f) 
                {
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }
            }
            else
            {
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if(Vector3.Distance(transform.position, Target.position) < 4.5f)
                {
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                }
            }
        }
    }
}
