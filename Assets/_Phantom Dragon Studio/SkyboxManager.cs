using Zenject;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{

    public MaterialSheet skyboxes;

    void Awake()
    {
        RenderSettings.skybox = skyboxes.loadingScreenMaterial;
    }

    private void SetSkyboxMaterial(Material skyboxMaterial)
    {
        RenderSettings.skybox = skyboxMaterial;
    }
}
