using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material originalMaterial;  // ���� Material
    public Material ghostMaterial;  // �ٲ� Material
    private Renderer objectRenderer; // Mesh Renderer

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
    }

   

    public void ChangeMaterialTemporarily()
    {
        // Ghost�� ����
        objectRenderer.material = ghostMaterial;

        // 5�� �Ŀ� �ٽ� ������� ����
        Invoke("RestoreMaterial", 5f);
    }

    void RestoreMaterial()
    {
        objectRenderer.material = originalMaterial;
    }
}