using UnityEngine;
public class ParticleLimitedPool : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _smokePrefab;
    [SerializeField]
    private Canvas _mainCanvas;

    private ParticleSystem _smokeFirst;
    private ParticleSystem _smokeSecond;

    private bool FirstWasLastUsed = false;

    private void Start()
    {
        _smokeFirst = CreateParticle();
        _smokeSecond = CreateParticle();
        FirstWasLastUsed = false;
    }
    private ParticleSystem CreateParticle()
    {
        return Instantiate(_smokePrefab, _mainCanvas.transform, false);
    }

    public ParticleSystem GetParticle(Vector3 position)
    {
        var result = FirstWasLastUsed ? _smokeSecond : _smokeFirst;
        FirstWasLastUsed = !FirstWasLastUsed;
        result.transform.localPosition = position;
        result.Stop();
        return result;
    }
}
