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
        if (IsEnabledAtStart)
        {
            IsEnabled = true;
            InitializeComponents();
        }
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
        if (_viewComponents == null) {
            _viewComponents = GetComponentsInChildren<ViewComponent>(true);
        }
    }

    protected void HideAllComponents()
    {
        foreach (ViewComponent component in _viewComponents)
        {
            if (component.isActiveAndEnabled == false)
            {
                component.gameObject.SetActive(true);
            }
            if (component.isAnimated)
            {
                component.AnimateOut();
                return;
            }
            component.gameObject.SetActive(false);
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
        if (IsEnabled == false)
        {
            ShowAllComponents();
            IsEnabled = true;
        }
    }
    public void DisableView()
    {
        if (IsEnabled == true)
        {
            HideAllComponents();
            IsEnabled = false;
        }
    }
}
