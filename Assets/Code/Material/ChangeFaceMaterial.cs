using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFaceMaterial : MonoBehaviour
{
    [SerializeField] private Material[] newFaceMaterial;
    [SerializeField] private SkinnedMeshRenderer facerender;

    // Start is called before the first frame update
    void Start()
    {
        //ChangeFace();
        //facerender.materials[2] = newFaceMaterial;
    }


    public void ChangeFace(int index)
    {
        Material[] materials = facerender.materials;
        int faceMaterialIndex = 2; // 얼굴 머터리얼의 인덱스 확인 후 수정
        materials[faceMaterialIndex] = newFaceMaterial[index];
        facerender.materials = materials;

        //facerender.materials[2] = newFaceMaterial;
    }
    public void ChangeEnemyFace(int index)
    {
        Material[] materials = facerender.materials;
        int faceMaterialIndex = 0; // 얼굴 머터리얼의 인덱스 확인 후 수정
        materials[faceMaterialIndex] = newFaceMaterial[index];
        facerender.materials = materials;

        //facerender.materials[2] = newFaceMaterial;
    }
}
