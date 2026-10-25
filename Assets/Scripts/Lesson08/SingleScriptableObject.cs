using UnityEngine;

public class SingleScriptableObject<T> : ScriptableObject where T: ScriptableObject
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance ==null)
            {
                //规则
                //1: 所有资源都在Resources文件夹下的ScriptableObject文件夹中
                //2: 需要复用的 唯一的资源文件名 必须和类名一样
                instance = Resources.Load<T>("ScriptableObject/"+typeof(T).Name);
            }

            if (instance ==null)
            {
                instance = CreateInstance<T>();
            }

            return instance;
        }
    }
}
