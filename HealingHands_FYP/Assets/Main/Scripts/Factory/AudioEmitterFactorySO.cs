using HH.Factory;
using UnityEngine;

[CreateAssetMenu(fileName = "New AudioEmitterFactorySO", menuName = "Factory /AudioEmitter Factory")]
public class AudioEmitterFactorySO : FactorySO<AudioEmitter>
{
    public AudioEmitter _prefab = default;

    public override AudioEmitter Create()
    {
        return Instantiate(_prefab);
    }
}
