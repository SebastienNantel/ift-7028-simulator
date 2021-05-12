namespace discrete_event_simulation.code.events {
  /// <summary>
  ///   This kind of events will be executed at the time it was meant to or later depending on the availability of a
  ///   resource
  /// </summary>
  public abstract class TemporalEvent : Event {
    protected TemporalEvent(Timestamp timestamp) : base(timestamp) { }

    /// <summary>
    ///   Called by the Event base class when the execute method is call, it sets the actual current timestamp to the clock
    ///   time
    /// </summary>
    /// <param name="currentClockTime">Current time that the clock is at when the execution occurs</param>
    protected override void SetActualTimestamp(double currentClockTime) {
      actualExecutionTimestamp = new Timestamp(currentClockTime);
    }
  }
}