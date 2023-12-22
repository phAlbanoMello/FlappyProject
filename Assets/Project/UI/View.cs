using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private bool _isEnabledAtStart;

    public bool IsEnabledAtStart { get => _isEnabledAtStart; }

    private ViewComponent[] _viewComponents;
    protected bool IsEnabled = false;
 
    public virtual void Initialize()
    {
        LoadViewComponents();
        DisableView();

        if (!IsEnabledAtStart) { return; }

        EnableView();
    }

    protected void LoadViewComponents()
    {
        if (_viewComponents != null){return;}
        _viewComponents = GetComponentsInChildren<ViewComponent>(true);
        _viewComponents.Initialize();
    }

    protected void HideAllComponents()
    {
        foreach (ViewComponent component in _viewComponents)
        {
            ITweenAnimation animation = component.TweenAnimation;
            if (animation != null)
            {
                animation.AnimateOut();
            }
        }
        ToggleCanvasVisibility(false);
    }


    protected void ShowAllComponents()
    {
        ToggleCanvasVisibility(true);
        foreach (ViewComponent component in _viewComponents)
        {
            ITweenAnimation animation = component.TweenAnimation;
            if (animation != null)
            {
                animation.AnimateIn();
            }
        }
    }
    private void ToggleCanvasVisibility(bool visible)
    {
        CanvasGroup canvasGroup = gameObject.GetComponentInChildren<CanvasGroup>();
        int targetAlpha = visible? 1 : 0;
        canvasGroup.LeanAlpha(targetAlpha, 0.15f);
    }

    public void EnableView()
    {
        ShowAllComponents();
        IsEnabled = true;
    }
    public void DisableView()
    {
        HideAllComponents();
        IsEnabled = false;
    }
}
