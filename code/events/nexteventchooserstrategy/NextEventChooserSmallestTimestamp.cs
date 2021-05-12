namespace discrete_event_simulation.code.events.nexteventchooserstrategy {
  /// <summary>
  ///   Always choose the next event that as the smallest timestamp in the events list.
  /// </summary>
  public class NextEventChooserStrategySmallestTimestamp : INextEventChooserStrategy {
    public Event Choose(EventsList eventsList, double currentClockTime) {
      return eventsList.GetEventWithSmallestTimestamp(currentClockTime);
    }
  }
}