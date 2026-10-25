using UnityEngine;

public class Lesson08 : MonoBehaviour
{
    void Start()
    {
        print($"Atk:{TestData.Instance.atk}\tDef:{TestData.Instance.def}");
        TestData.Instance.Wound();
    }
}
