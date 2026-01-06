using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/Split Rules")]
public sealed class SplitRules : ScriptableObject
{
    public SplitConfig[] rules;

    public bool TryGetRule(AsteroidSize size, out SplitConfig rule)
    {
        if (rules != null)
        {
            foreach (var r in rules)
            {
                if (r != null && r.inputSize == size && r.canSplit)
                {
                    rule = r;
                    return true;
                }
            }
        }
        rule = null;
        return false;
    }
}