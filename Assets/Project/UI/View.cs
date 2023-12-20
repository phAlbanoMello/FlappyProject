using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private bool _isEnabledAtStart;

    public bool IsEnabledAtStart { get => _isEnabledAtStart; }

    private ViewComponent[] _viewComponents;
    protected bool IsEnabled = false;
    private GameObject _canvasObject;

    public virtual void Initialize()
    {
        InitCanvas();
        LoadViewComponents();

        if (!IsEnabledAtStart) { return; }

        IsEnabled = true;
        InitializeComponents();
    }

    private void InitCanvas()
    {
        _canvasObject = GetComponentInChildren<Canvas>().gameObject;
        _canvasObject.SetActive(false);
    }

    private void InitializeComponents()
    {
        foreach (ViewComponent viewComponent in _viewComponents)
        {
            viewComponent.Initialize();
        }
    }

    protected void LoadViewComponents()
    {
        if (_viewComponents != null){return;}
        _viewComponents = GetComponentsInChildren<ViewComponent>(true);
    }

    protected void HideAllComponents()
    {
        foreach (ViewComponent component in _viewComponents)
        {
            if (component.isActiveAndEnabled == false)
            {
                component.gameObject.SetActive(true);
            }
       
            component.AnimateOut();
        }
    }
    protected void ShowAllComponents()
    {
        foreach (ViewComponent component in _viewComponents)
        {
            if (component.isAnimated)
            {
                component.AnimateIn(() => { });
            }
        }
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
