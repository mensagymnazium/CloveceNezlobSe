using System.Numerics;
using System.Reflection;

namespace CloveceNezlobSe.DiceMaster;

class Xoshiro
{
    private ulong _s0, _s1, _s2, _s3;

    public Xoshiro(ulong s0, ulong s1, ulong s2, ulong s3)
    {
        _s0 = s0;
        _s1 = s1;
        _s2 = s2;
        _s3 = s3;
    }

    public ulong NextULong()
    {
        ulong s0 = _s0, s1 = _s1, s2 = _s2, s3 = _s3;

        ulong result = BitOperations.RotateLeft(s1 * 5, 7) * 9;
        ulong t = s1 << 17;

        s2 ^= s0;
        s3 ^= s1;
        s1 ^= s2;
        s0 ^= s3;

        s2 ^= t;
        s3 = BitOperations.RotateLeft(s3, 45);

        _s0 = s0;
        _s1 = s1;
        _s2 = s2;
        _s3 = s3;

        return result;
    }

    public int Next()
    {
        while (true)
        {
            ulong result = NextULong() >> 33;
            if (result != int.MaxValue)
            {
                return (int)result;
            }
        }
    }

    public int Next(int minValue, int maxValue)
    {
        ulong exclusiveRange = (ulong)((long)maxValue - minValue);

        if (exclusiveRange > 1)
        {
            int bits = 64 - BitOperations.LeadingZeroCount(exclusiveRange);

            while (true)
            {
                ulong result = NextULong() >> (sizeof(ulong) * 8 - bits);
                if (result < exclusiveRange)
                {
                    return (int)result + minValue;
                }
            }
        }

        return minValue;
    }
}

static class RandomReverser
{
    private static ulong GetSeedValue(object random, string name)
    {
        Type type = random.GetType();
        FieldInfo field = type.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic)!;
        return (ulong)field.GetValue(random)!;
    }

    public static Xoshiro CopySharedRandom()
    {
        Type randomType = Random.Shared.GetType();
        FieldInfo implInfo = randomType.GetField("t_random", BindingFlags.Static | BindingFlags.NonPublic)!;
        object impl = implInfo.GetValue(null)!;

        ulong s0 = GetSeedValue(impl, "_s0");
        ulong s1 = GetSeedValue(impl, "_s1");
        ulong s2 = GetSeedValue(impl, "_s2");
        ulong s3 = GetSeedValue(impl, "_s3");

        return new Xoshiro(s0, s1, s2, s3);
    }
}
