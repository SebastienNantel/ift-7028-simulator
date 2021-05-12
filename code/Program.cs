using discrete_event_simulation.code.events;
using discrete_event_simulation.code.events.nexteventchooserstrategy;
using discrete_event_simulation.code.random;

namespace discrete_event_simulation.code {
  class Program {
    private static void Main(string[] args) {
      const int seed = 1;
      IRandom random = new RandomGenerator(seed);
      var eventsList = new EventsList();
      var nextEventChooser = new NextEventChooserStrategySmallestTimestamp();
      var clock = new Clock();
    }
  }
}