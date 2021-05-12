namespace discrete_event_simulation.code.events.nexteventchooserstrategy {
  /// <summary>
  ///   If the last event played had a dependant event, this implementation will return the dependant event to be played,
  ///   otherwise it will return the event with the smallest timestamp in the events list
  /// </summary>
  public class NextEventChooserStrategyExecuteAllDependantEventBeforeGettingSmallest : INextEventChooserStrategy {
    public Event Choose(EventsList eventsList, double currentClockTime) {
      var lastExecutedEvent = eventsList.GetLastReturnedEvent();

      if (lastExecutedEvent != null && lastExecutedEvent.DependantEvent != null) {
        var nextEventToExecute = lastExecutedEvent.DependantEvent;
        eventsList.RemoveEvent(nextEventToExecute);
        eventsList.SetLastExecutedEvent(nextEventToExecute);

        return nextEventToExecute;
      }
      else {
        INextEventChooserStrategy smallestTimestampStrategy = new NextEventChooserStrategySmallestTimestamp();

        var nextEventToExecute = smallestTimestampStrategy.Choose(eventsList, currentClockTime);
        eventsList.SetLastExecutedEvent(nextEventToExecute);

        return nextEventToExecute;
      }
    }
  }
}