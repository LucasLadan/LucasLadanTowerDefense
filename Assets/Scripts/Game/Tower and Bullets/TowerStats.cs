using UnityEngine;

[CreateAssetMenu(fileName = "TowerStats", menuName = "Scriptable Objects/TowerStats")]
public class TowerStats : ScriptableObject
{
    public int _health;
    public float _fireRate;
    public int _cost;
    public float _range;
    public float _checkTime;
    public Bullet _bullet;
    public TowerType _type;
    public Sprite _sprite;

    public enum TowerType
    {
        shoot,produce,wall,instant
    }

}
