namespace discrete_event_simulation.code.events.nexteventchooserstrategy {
  /// <summary>
  ///   Choose the next event to be played by the simulator.
  /// </summary>
  public interface INextEventChooserStrategy {
    /// <summary>
    ///   Method use in order to choose the next event to be played by the simulator
    /// </summary>
    /// <param name="eventsList">
    ///   Object containing all events that are planned to be played. Will choose the next event
    ///   to be played from this list of events
    /// </param>
    /// <param name="currentClockTime">Current simulation time</param>
    /// <returns>The next event to be played</returns>
    Event Choose(EventsList eventsList, double currentClockTime);
  }
}