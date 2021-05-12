namespace discrete_event_simulation.code.events {
  /// <summary>
  ///   This kind of event will be executed at the time it was meant to no matter what the current clock time is.
  ///   It is the kind of event that just happen at a certain time.
  /// </summary>
  public abstract class TimelessEvent : Event {
    protected TimelessEvent(Timestamp timestamp) : base(timestamp) { }

    /// <summary>
    ///   Called by the event base class in order to set the actual execution timestamp to the time it was meant to be
    ///   executed.
    /// </summary>
    /// <param name="clockTime">Unused since this kind of event are always executed at the time they were supposed to</param>
    protected override void SetActualTimestamp(double clockTime) {
      actualExecutionTimestamp = new Timestamp(possibleExecutionTimestamp.Time);
    }
  }
}