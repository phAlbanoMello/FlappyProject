using FlappyProject.Interfaces;
using UnityEngine;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using Unity.Loading;

public class UIManager : MonoBehaviour, IManager
{
    private Stack<View> _viewStack = new Stack<View>();
    private Queue<View> _viewQueue = new Queue<View>();
    private List<View> _loadedViews = new List<View>();

    [SerializeField] private bool _autoStart;
    public bool ShouldInitializeAtStart { get { return _autoStart; } }
    public bool HasInitiated { get; private set; }

    public void Init()
    {
       LoadViewsFromContainer();
        HasInitiated = true;
    }

    public void Stop()
    {
       //Unsubscribe events
    }

    public void UpdateManager(float deltaTime)
    {
    }

    public void LoadViewsFromContainer()
    {
        _loadedViews.Clear();

        View[] viewsInContainer = GetComponentsInChildren<View>(true);

        foreach (View view in viewsInContainer)
        {
            _loadedViews.Add(view);
            if (view.IsEnabledAtStart){
                view.gameObject.SetActive(true);
                continue;
            }
            view.gameObject.SetActive(false);
        }
    }

    public void PushView(View view)
    {
        if (view != null)
        {
            if (_viewStack.Count > 0)
            {
                View topView = _viewStack.Peek();
                topView.gameObject.SetActive(false);
            }

            view.gameObject.SetActive(true);
            _viewStack.Push(view);
        }
    }

    public View PopView()
    {
        if (_viewStack.Count > 0)
        {
            View topView = _viewStack.Pop();
            topView.gameObject.SetActive(false);

            if (_viewStack.Count > 0)
            {
                View newTopView = _viewStack.Peek();
                newTopView.gameObject.SetActive(true);
            }
            return topView;
        }

        return null;
    }

    public void EnqueueView(View view)
    {
        if (view != null)
        { 
            _viewQueue.Enqueue(view);
        }
    }

    public void ProcessNextQueuedView()
    {
        if (_viewQueue.Count > 0)
        {
            View nextView = _viewQueue.Dequeue();
            PushView(nextView);
        }
    }

    public void ClearViews()
    {
        foreach (View view in _viewStack)
        {

            view.gameObject.SetActive(false);
        }
        _viewStack.Clear();

        foreach (View view in _viewQueue)
        {
            view.gameObject.SetActive(false);
        }
        _viewQueue.Clear();
    }
}

