using UnityEngine;
using UnityEngine.EventSystems;

public enum Layer
{
    WorldSpaceCanvas = 8,
    InWorldObject = 9,
    RaycastEndStop = 30,
}

public class Reticle : MonoBehaviour
{

    public Layer[] layerPriorities = {
        Layer.WorldSpaceCanvas,
        Layer.InWorldObject,
    };
    private Transform reticleTransform, controller;
    private float maxRayDistance = 50;
    RaycastHit m_hit;
    Layer m_layerHit;

    public RaycastHit Hit
    {
            get { return m_hit; }
        }
     
    public Layer LayerHit
    {
        get { return m_layerHit; }
    }

    void Start()
    {
        controller = this.transform.parent.transform;
        reticleTransform = this.transform;
    } 
      
    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                m_hit = hit.Value;
                m_layerHit = layer;
                UpdateReticleLocation(m_hit.point);;
                if (m_hit.transform.gameObject.GetComponent<AdvancedUIButton>())
                {
                    var temp = m_hit.collider.transform.gameObject.GetComponent<AdvancedUIButton>();
                    IPointerEnterHandler hoverHandler = temp.gameObject.GetComponent<IPointerEnterHandler>();
                    if (hoverHandler != null)
                    {
                        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                        temp.OnPointerEnter(pointerEventData);
                    }
                }
                return;
            }
        }
        //Otherwise return background hit
        m_hit.distance = maxRayDistance;
        m_layerHit = Layer.RaycastEndStop;
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = new Ray(controller.position, reticleTransform.forward);
        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, maxRayDistance, layerMask);
        if (hasHit)
            {
                return hit;
            }
        return null;
    }

    private void UpdateReticleLocation(Vector3 rayHitLoc)
    {
        reticleTransform.position = rayHitLoc;
    }
}