using Cysharp.Threading.Tasks;
public interface BaseModel
{
    bool Init();
    UniTask<bool> InitAysnc();
    void Release();

#region 生命周期函数
    void OnStart();
    void OnUpdate();
    void OnFixUpdate();
    void OnLateUpdate();
#endregion

}
