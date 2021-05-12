namespace discrete_event_simulation.code {
  /// <summary>
  /// The simulation clock
  /// </summary>
  public class Clock {
    /// <summary>
    /// Constructor. Set the current clock to 0.
    /// </summary>
    public Clock() {
      CurrentTimestamp = new Timestamp(0);
    }

    /// <summary>
    /// Constructor. Current clock time is set at a given time.
    /// </summary>
    /// <param name="startingTime">The time at what the simulation will start</param>
    public Clock(int startingTime) {
      CurrentTimestamp = new Timestamp(startingTime);
    }

    public double CurrentTime => CurrentTimestamp.Time;
    public Timestamp CurrentTimestamp { get; set; }

    /// <summary>
    /// Add time to the current clock time
    /// </summary>
    /// <param name="timeToAdd">The amount of time in a timestamp format</param>
    public void AddTime(Timestamp timeToAdd) {
      CurrentTimestamp += timeToAdd;
    }

    /// <summary>
    /// Add time to the current clock time
    /// </summary>
    /// <param name="timeToAdd">The amount of time to add</param>
    public void AddTime(double timeToAdd) {
      var tempTimestamp = new Timestamp(timeToAdd);
      CurrentTimestamp += tempTimestamp;
    }

    /// <summary>
    /// Get the current clock time in a timestamp format
    /// </summary>
    /// <returns>Return the current clock time in a timestamp format</returns>
    public Timestamp GetCurrentTimestamp() {
      return new Timestamp(CurrentTimestamp.Time);
    }
  }
}