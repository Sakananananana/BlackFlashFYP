using UnityEngine;
using HH.Pool;
using HH.Factory;

[CreateAssetMenu(fileName = "NewAudioEmitterPool", menuName = "Pool /AudioEmitter Pool")]
public class AudioEmitterPoolSO : ComponentPoolSO<AudioEmitter>
{
    [SerializeField] private AudioEmitterFactorySO _factory;

    public override IFactory<AudioEmitter> Factory
    {
        get { return _factory; }
        set { _factory = value as AudioEmitterFactorySO; }
    }

}
