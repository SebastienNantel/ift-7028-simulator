namespace discrete_event_simulation.code.events {
  /// <summary>
  ///   Base class used by the temporal and timeless abstract event classes
  /// </summary>
  public abstract class Event {
    protected Timestamp actualExecutionTimestamp;
    protected Timestamp possibleExecutionTimestamp;

    /// <summary>
    ///   Constructor
    /// </summary>
    /// <param name="timestamp">Possible timestamp that the event can be executed. Event won't be executed before that time</param>
    protected Event(Timestamp timestamp) {
      possibleExecutionTimestamp = timestamp;
    }

    public Timestamp PossibleExecutionTimestamp {
      get => possibleExecutionTimestamp;
      set => possibleExecutionTimestamp = value;
    }

    public Timestamp ActualExecutionTimestamp {
      get => actualExecutionTimestamp;
      set => actualExecutionTimestamp = value;
    }

    public Event DependantEvent { get; private set; }

    /// <summary>
    ///   Chain a dependant event. Chained events can't be executed before the event that is chained is executed
    /// </summary>
    /// <param name="eventToChain">Event to chain to the current event</param>
    public void ChainDependantEvent(Event eventToChain) {
      DependantEvent = eventToChain;
    }

    /// <summary>
    ///   Code that will be executed when the event is chosen to be executed. Every concrete event must call base.execute in
    ///   order to set the right actual execution timestamp. If the event add a dependant event, the dependant event
    ///   is added the the event list.
    /// </summary>
    /// <param name="eventsList">List of all the events created. Used in order to add new events created by the current event</param>
    /// <param name="clock">
    ///   Simulation clock. When executing the event add time to the clock when event is performing action
    ///   that take time
    /// </param>
    public virtual void Execute(EventsList eventsList, Clock clock) {
      SetActualTimestamp(clock.CurrentTime);

      if (DependantEvent == null) return;
      
      DependantEvent.PossibleExecutionTimestamp = clock.GetCurrentTimestamp();
      eventsList.AddEvent(DependantEvent);
    }

    protected abstract void SetActualTimestamp(double currentClockTime);

    public abstract override string ToString();
  }
}