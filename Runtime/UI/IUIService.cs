
using SUG.Essentials;
using UnityEngine;

public interface IUIService 
{
    public T OpenUI<T>() where T : UIBase;
    public void CloseUI<T>(bool destroy = false) where T : UIBase;
    public void HideAll();
}