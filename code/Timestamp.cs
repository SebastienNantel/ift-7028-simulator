using System;

namespace discrete_event_simulation.code {
  /// <summary>
  /// This class is used in order to represent the time inside the simulation
  /// </summary>
  public class Timestamp : IComparable<Timestamp> {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="time">The time that we wish to represent</param>
    public Timestamp(double time) {
      Time = time;
    }
    public double Time { get; }
    public int CompareTo(Timestamp other) {
      return Time.CompareTo(other.Time);
    }
    public static bool operator >(Timestamp current, Timestamp other) {
      return current.Time > other.Time;
    }
    public static bool operator <(Timestamp current, Timestamp other) {
      return current.Time < other.Time;
    }
    public static bool operator >=(Timestamp current, Timestamp other) {
      return current.Time >= other.Time;
    }
    public static bool operator <=(Timestamp current, Timestamp other) {
      return current.Time <= other.Time;
    }
    public override string ToString() {
      return $"Time : {Time.ToString()}";
    }
    public static Timestamp operator +(Timestamp first, Timestamp other) {
      return new Timestamp(first.Time + other.Time);
    }
  }
}