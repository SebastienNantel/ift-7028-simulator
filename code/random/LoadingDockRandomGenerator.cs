using System;

namespace discrete_event_simulation.code.random {
  public class LoadingDockRandomGenerator {
    private readonly IRandom random;
    private readonly double averageTimeBetweenArrivals;
    private readonly double averageLoadingTime;

    public LoadingDockRandomGenerator(IRandom random,
      double averageTimeBetweenArrivals,
      double averageLoadingTime) {
      this.averageTimeBetweenArrivals = averageTimeBetweenArrivals;
      this.averageLoadingTime = averageLoadingTime;
      this.random = random;
    }

    public double DurationBetweenArrival() {
      return -Math.Log(1.0 - random.Next() % 10000 / 10000.0, Math.E) * averageTimeBetweenArrivals;
    }

    public double LoadingTimeDuration() {
      return -Math.Log(1.0 - random.Next() % 10000 / 10000.0, Math.E) * averageLoadingTime;
    }
  }
}