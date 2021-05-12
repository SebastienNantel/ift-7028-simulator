namespace discrete_event_simulation.code.random {
  /// <summary>
  ///   Interface used in order to use any implementation of a random number generator
  /// </summary>
  public interface IRandom {
    int Next();
    int Next(int upperBound);
    int Next(int lowerBound, int upperBound);
    double NextDouble();
    void Reset(int seed);
  }
}