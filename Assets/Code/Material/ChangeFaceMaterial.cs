using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFaceMaterial : MonoBehaviour
{
    [SerializeField] private Material newFaceMaterial;
    [SerializeField] private SkinnedMeshRenderer facerender;

    // Start is called before the first frame update
    void Start()
    {
        //ChangeFace();
        //facerender.materials[2] = newFaceMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeFace();
        }
    }

    void ChangeFace()
    {
        Material[] materials = facerender.materials;
        int faceMaterialIndex = 2; // �� ���͸����� �ε��� Ȯ�� �� ����
        materials[faceMaterialIndex] = newFaceMaterial;
        facerender.materials = materials;

        //facerender.materials[2] = newFaceMaterial;
    }
}
