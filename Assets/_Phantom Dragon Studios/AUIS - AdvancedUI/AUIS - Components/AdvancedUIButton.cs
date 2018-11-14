using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using CurvedUI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class AdvancedUIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region VARIABLES   
    private TooltipHandler tooltipHandler;
    private Image image, icon;
    private Button button;
    private CurvedUIVertexEffect myVeretexEffect;

    public _AdvancedUISkinData UISkinData;
    public  _AdvancedUITooltipData myTooltipData;
    public ButtonType buttonType;

    public enum ButtonType
    {
        Default,
        Confirm,
        Decline,
        Warning
    }
    #endregion

    #region GLOBAL BUTTON GUI SKINNING
    protected virtual void OnSkinUI()
    {
        icon = transform.GetChild(0).GetComponentInChildren<Image>();
        button = GetComponent<Button>();


        button.transition = Selectable.Transition.SpriteSwap;
        button.targetGraphic = image;

        image.sprite = UISkinData.elementSprite;
        button.spriteState = UISkinData.elementSpriteState;
        image.type = Image.Type.Sliced;

        switch (buttonType)
        {
            case ButtonType.Confirm:
                image.color = UISkinData.confirmButtonColor;
                icon.sprite = UISkinData.confirmButtonSprite;
                break;
            case ButtonType.Decline:
                image.color = UISkinData.declinedButtonColor;
                icon.sprite = UISkinData.declinedButtonSprite;
                break;
            case ButtonType.Warning:
                image.color = UISkinData.warningButtonColor;
                icon.sprite = UISkinData.warningButtonSprite;
                break;
            default:
                image.color = UISkinData.defaultButtonColor;
                icon.sprite = UISkinData.defaultButtonSprite;
                break;
        }
    }
    #endregion

    #region INITIALIZATION
    public void Awake()
    {
        myVeretexEffect = this.gameObject.GetComponent<CurvedUIVertexEffect>();
        tooltipHandler = FindObjectOfType<TooltipHandler>().GetComponent<TooltipHandler>();
        image = GetComponent<Image>();
    }
    #endregion

    #region POINTERDATA RECIEVING EVENTS
    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        OnHoverEnter(this.gameObject);
    }
    #endregion

    #region DETECTING INFORMATION TO SEND
    public void OnHoverEnter(GameObject triggeringObject)
    {
        //Types of information to check for
        if (triggeringObject.GetComponent<UsableAbility>() != null)
        {
            _AbilityData isAbility;
            var temp = triggeringObject.GetComponent<UsableAbility>();
            isAbility = temp.abilityInfo;
            tooltipHandler.AssignTooltipData(isAbility);
            return;
        }
        else if (triggeringObject.GetComponent<TalentNode>() != null)
        {
            _AbilityData isAbility;
            var temp = triggeringObject.GetComponent<TalentNode>();
            isAbility = temp.nodeInfo;
            tooltipHandler.AssignTooltipData(isAbility);
            return;
        }
        else if (triggeringObject.GetComponent<AdvancedUIButton>() != null)
        {
            AdvancedUIButton isUIButton;
            isUIButton = triggeringObject.GetComponent<AdvancedUIButton>();
            tooltipHandler.AssignTooltipData(isUIButton);
            return;
        }
        //World Objects wouldn't have a UI UI button on them, could possibly be made to work. Reevaluate later.
        //else if (triggeringObject.GetComponent<WorldObjectTooltipAgent>() != null)
        //{
        //    WorldObjectTooltipAgent isWorldObject;
        //    isWorldObject = triggeringObject.GetComponent<WorldObjectTooltipAgent>();
        //    tooltipHandler.AssignTooltipData(isWorldObject);
        //    return;
        //}

        //IF ITEM DO STRUFF
    }

    private void OnHoverExit()
    {
        image.color = UISkinData.defaultButtonColor;
        myVeretexEffect.SetDirty();
    }
    private void OnClick()
    {
        image.color = UISkinData.hoverButtonPressedColor;
        myVeretexEffect.SetDirty();
    }

    private void ResetButtonState()
    {
        image.color = UISkinData.hoverButtonHighlightColor;
        myVeretexEffect.SetDirty();
    }
    #endregion
}
