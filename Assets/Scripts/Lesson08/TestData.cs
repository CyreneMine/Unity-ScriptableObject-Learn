using UnityEngine;

[CreateAssetMenu(fileName = "TestData", menuName = "SO/Lesson08/TestData")]
public class TestData : SingleScriptableObject<TestData>
{
    public int atk;
    public int def;
    public void Wound()
    {
        Debug.Log("受伤");
    }
}
