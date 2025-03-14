using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material originalMaterial;  // 원래 Material
    public Material ghostMaterial;  // 바뀐 Material
    private Renderer objectRenderer; // Mesh Renderer

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
    }

   

    public void ChangeMaterialTemporarily()
    {
        // Ghost로 변경
        objectRenderer.material = ghostMaterial;

        // 5초 후에 다시 원래대로 변경
        Invoke("RestoreMaterial", 5f);
    }

    void RestoreMaterial()
    {
        objectRenderer.material = originalMaterial;
    }
}