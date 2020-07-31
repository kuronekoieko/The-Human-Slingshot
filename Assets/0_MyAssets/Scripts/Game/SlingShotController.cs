using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotController : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer rightElastic;
    [SerializeField] SkinnedMeshRenderer leftElastic;
    [SerializeField] SkinnedMeshRenderer leather;
    [SerializeField] SkinnedMeshRenderer leatherLine;
    [SerializeField] LineRenderer rightElasticLine;
    [SerializeField] LineRenderer leftElasticLine;

    void Start()
    {
        rightElasticLine.useWorldSpace = true;
        leftElasticLine.useWorldSpace = true;
        rightElasticLine.SetPosition(0, rightElasticLine.transform.position);
        leftElasticLine.SetPosition(0, leftElasticLine.transform.position);
    }

    public void SetPosition(Vector3 projectilePos)
    {

        //Disables the elastic mesh renderer.
        rightElastic.enabled = false;
        leftElastic.enabled = false;
        leather.enabled = false;

        //Activates the elastic line, so the movement becomes more fluid and beautiful.
        rightElasticLine.gameObject.SetActive(true);
        leftElasticLine.gameObject.SetActive(true);
        leatherLine.enabled = true;

        var offsetZ = -0.3f;
        var offsetY = 0.5f;
        //For the elastic to stretch the value of the z axis is increased to - 7, maximum of the stretch.
        rightElasticLine.SetPosition(1, projectilePos + new Vector3(1.5f, offsetY, offsetZ));
        //The lines are growing and the value of the z axis is increased.
        leftElasticLine.SetPosition(1, projectilePos + new Vector3(-1.5f, offsetY, offsetZ));
        //Leather and metallic sphere follow the movement of the line.
        Vector3 offset = new Vector3(0, offsetY, offsetZ);
        leather.transform.position = projectilePos + offset;
        leatherLine.transform.position = projectilePos + offset;
    }

    public void Release()
    {
        //When the left mouse button is released, the metallic sphere will be thrown and the elastic will be decompressed with the movement.
        //Activates the elastic mesh renderer.
        rightElastic.enabled = true;
        leftElastic.enabled = true;
        leather.enabled = true;

        //Disables the elastic line.
        rightElasticLine.gameObject.SetActive(false);
        leftElasticLine.gameObject.SetActive(false);
        leatherLine.enabled = false;
    }
}
