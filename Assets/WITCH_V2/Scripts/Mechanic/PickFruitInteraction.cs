using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickFruitInteraction : Interaction
{
    [Header("Setting")]
    [SerializeField] float ModifyPositionY = 1;
    [SerializeField] float AnimateDuration = 1;
    [SerializeField] MoveMethod MoveMethod;

    [Header("Object Reference")]
    [SerializeField] GardenFruit GardenFruit;
    [SerializeField] Moveable Moveable;
    [SerializeField] SpriteFader SpriteFader;

    private Transform _initialGroup;
    private Transform fruitTransform => transform.parent;

    protected override void OnAwake()
    {
        base.OnAwake();
        _initialGroup = fruitTransform.parent;
    }

    protected override void OnComponentReset()
    {
        base.OnComponentReset();
        if (transform.parent != null)
        {
            fruitTransform.TryGetComponent(out Moveable);
            fruitTransform.TryGetComponent(out SpriteFader);
            fruitTransform.TryGetComponent(out GardenFruit);
        }
    }

    public override void Interact()
    {
        base.Interact();
        // At the frame action list called
        GardenFruit.DoPickUp();
        SpriteFader.Fade(FadeType.fadeOut, AnimateDuration, 1);
        Moveable.Move(Moveable.transform.position + (Vector3.up * ModifyPositionY), MoveMethod, true, AnimateDuration, TransformType.Translate, false , null, false);
        Invoke("OnFinishAnimate", AnimateDuration + .1f);
    }

    private void OnFinishAnimate()
    {
        fruitTransform.SetParent(_initialGroup);
        fruitTransform.transform.localPosition = Vector3.zero;
    }
}
