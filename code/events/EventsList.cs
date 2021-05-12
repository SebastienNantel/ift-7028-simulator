using System.Collections.Generic;
using System.Linq;

namespace discrete_event_simulation.code.events {
  /// <summary>
  ///   Class that contain and manipulate all the events currently waiting to be played
  /// </summary>
  public class EventsList {
    private readonly SortedDictionary<Timestamp, List<Event>> temporalEvents;
    private readonly SortedDictionary<Timestamp, List<Event>> timelessEvents;
    private Event lastReturnedEvent;
    public EventsList() {
      temporalEvents = new SortedDictionary<Timestamp, List<Event>>();
      timelessEvents = new SortedDictionary<Timestamp, List<Event>>();
    }
    public Event GetLastReturnedEvent() {
      return lastReturnedEvent;
    }
    /// <summary>
    ///   Method that choose the event with the smallest timeless timestamp if it is before the current clock time,
    ///   if there is no timeless event it will choose the temporal event with the smallest timestamp
    /// </summary>
    /// <param name="currentClockTime">Current simulation time</param>
    /// <returns>
    ///   Event with the smallest timestamp, either a temporal or timeless event depending on the current
    ///   simulation time
    /// </returns>
    public Event GetEventWithSmallestTimestamp(double currentClockTime) {
      // Find the timeless event with the smallest timestamp
      Event timelessSmallestEvent = null;
      if (timelessEvents.Count > 0) {
        var firstTimeless = timelessEvents.First();
        var timelessValues = firstTimeless.Value;
        timelessSmallestEvent = timelessValues.First();
      }

      // Find the temporal event with the smallest timestamp
      Event temporalSmallestEvent = null;
      if (temporalEvents.Count > 0) {
        var firstTemporal = temporalEvents.First();
        var temporalValues = firstTemporal.Value;
        temporalSmallestEvent = temporalValues.First();
      }

      // if there is a timeless event and it's possible execution time is before the clock we play it
      if (timelessSmallestEvent != null &&
          timelessSmallestEvent.PossibleExecutionTimestamp.Time <= currentClockTime) {
        RemoveEvent(timelessSmallestEvent);

        return timelessSmallestEvent;
        // if the possible execution time of the timeless event was too big we play the first temporal event
      }

      if (temporalSmallestEvent != null) {
        RemoveEvent(temporalSmallestEvent);

        return temporalSmallestEvent;

        // if there was no temporal event to play we play the next timeless event, the clock will be update by the simulator
      }

      RemoveEvent(timelessSmallestEvent);

      return timelessSmallestEvent;
    }

    public void SetLastExecutedEvent(Event evt) {
      lastReturnedEvent = evt;
    }

    /// <summary>
    ///   Will remove an event from the events list
    /// </summary>
    /// <param name="evt">The event to remove</param>
 
   public void RemoveEvent(Event evt) {
      if (typeof(TemporalEvent) == evt.GetType().BaseType)
        RemoveTemporalEvent(evt);
      else
        RemoveTimelessEvent(evt);
    }

    private void RemoveTemporalEvent(Event evt) {
      var evts = temporalEvents[evt.PossibleExecutionTimestamp];
      evts.Remove(evt);

      if (evts.Count == 0) temporalEvents.Remove(evt.PossibleExecutionTimestamp);
    }

    private void RemoveTimelessEvent(Event evt) {
      var evts = timelessEvents[evt.PossibleExecutionTimestamp];
      evts.Remove(evt);

      if (evts.Count == 0) timelessEvents.Remove(evt.PossibleExecutionTimestamp);
    }

    /// <summary>
    ///   Will add an event to the events list
    /// </summary>
    /// <param name="evt">Event to add</param>
    public void AddEvent(Event evt) {
      if (typeof(TemporalEvent) == evt.GetType().BaseType)
        AddTemporalEvent(evt);
      else
        AddTimelessEvent(evt);
    }

    private void AddTemporalEvent(Event evt) {
      if (temporalEvents.ContainsKey(evt.PossibleExecutionTimestamp)) {
        temporalEvents[evt.PossibleExecutionTimestamp].Add(evt);
      }
      else {
        var list = new List<Event> {evt};
        temporalEvents.Add(evt.PossibleExecutionTimestamp, list);
      }
    }

    private void AddTimelessEvent(Event evt) {
      if (timelessEvents.ContainsKey(evt.PossibleExecutionTimestamp)) {
        timelessEvents[evt.PossibleExecutionTimestamp].Add(evt);
      }
      else {
        var list = new List<Event> {evt};
        timelessEvents.Add(evt.PossibleExecutionTimestamp, list);
      }
    }

    public int NumberOfEvents() {
      return temporalEvents.Keys.Sum(key => temporalEvents[key].Count) +
             timelessEvents.Keys.Sum(key => timelessEvents[key].Count);
    }
  }
}