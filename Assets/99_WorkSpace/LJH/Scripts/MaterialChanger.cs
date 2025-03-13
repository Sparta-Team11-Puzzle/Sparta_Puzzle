using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material glassMaterial;  // Glass Material
    public Material ghostMaterial;  // Blood Material
    private Renderer objectRenderer; // Mesh Renderer

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    public void ChangeMaterialTemporarily()
    {
        // Blood�� ����
        objectRenderer.material = ghostMaterial;

        // 1�� �Ŀ� �ٽ� Glass�� ����
        Invoke("RestoreMaterial", 10f);
    }

    void RestoreMaterial()
    {
        objectRenderer.material = glassMaterial;
    }
}