using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoleInfo", menuName = "SO/角色信息")]
public class RoleInfo : ScriptableObject
{
    [System.Serializable]
    public class RoleData
    {
        public int id,atk,def;
        public string name;
    }
    public List<RoleData> roles;
}
