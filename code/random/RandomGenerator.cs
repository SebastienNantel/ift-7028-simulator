using System;

namespace discrete_event_simulation.code.random {
  /// <summary>
  ///   This implementation uses the PCG random number generator implementation. It is the same that is use in the
  ///   simsharp simulation framework
  /// </summary>
  public class RandomGenerator : IRandom {
    private readonly Pcg random;

    public RandomGenerator() {
      random = new Pcg();
    }

    public RandomGenerator(int seed) {
      random = new Pcg(seed);
    }

    public int Next() {
      return random.Next(1, int.MaxValue / 2);
    }

    public int Next(int upperBound) {
      return random.Next(upperBound);
    }

    public int Next(int lowerBound, int upperBound) {
      return random.Next(lowerBound, upperBound);
    }

    public double NextDouble() {
      return random.NextDouble();
    }

    public void Reset(int seed) {
      const ulong sequence = 721347520444481703ul;
      random.Initialize((ulong) seed, sequence);
    }
  }
}