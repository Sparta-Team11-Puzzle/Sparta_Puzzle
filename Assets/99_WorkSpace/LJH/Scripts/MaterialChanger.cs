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
        // Blood로 변경
        objectRenderer.material = ghostMaterial;

        // 1초 후에 다시 Glass로 변경
        Invoke("RestoreMaterial", 10f);
    }

    void RestoreMaterial()
    {
        objectRenderer.material = glassMaterial;
    }
}