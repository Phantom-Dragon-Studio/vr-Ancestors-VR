using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New UI Data Container", menuName = "Phantom Dragon Studios/Advanced UI System/Skin Data Container", order = 1)]
public class _AdvancedUISkinData : ScriptableObject {

    public Sprite elementSprite;
    public SpriteState elementSpriteState;

    public Color defaultButtonColor;
    public Sprite defaultButtonSprite;

    public Color confirmButtonColor;
    public Sprite confirmButtonSprite;

    public Color declinedButtonColor;
    public Sprite declinedButtonSprite;

    public Color warningButtonColor;
    public Sprite warningButtonSprite;

    public Color hoverButtonDisabledColor;
    public Color hoverButtonHighlightColor;
    public Color hoverButtonPressedColor;


}
