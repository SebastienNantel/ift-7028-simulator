using System.Collections.Generic;
using discrete_event_simulation.code.events;

namespace discrete_event_simulation.code.statistics {
  /// <summary>
  ///   Interface used in order to use different implementation of a statistic generator
  /// </summary>
  public interface IStatsGenerator {
    void ResetStats();
    string GenerateStats(IEnumerable<Event> events);
  }
}