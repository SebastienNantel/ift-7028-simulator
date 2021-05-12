using System;
using System.Collections.Generic;
using System.IO;
using discrete_event_simulation.code.events;
using discrete_event_simulation.code.events.nexteventchooserstrategy;
using static System.Double;

namespace discrete_event_simulation.code {
  /// <summary>
  ///   The simulator base class is use to simulate and get the report of the simulation
  /// </summary>
  public abstract class Simulator {
    protected readonly Clock Clock;
    protected readonly EventsList EventsList;
    protected readonly double WarmupDuration;
    private  double warmupDurationForReset;
    private readonly List<Event> executedEvent;
    private readonly INextEventChooserStrategy nextEventChooserStrategy;
    private readonly double simulationDuration;
    private readonly StreamWriter streamWriter;

    protected Simulator(EventsList eventsList,
      INextEventChooserStrategy nextEventChooserStrategy,
      Clock clock,
      double simulationDuration,
      double warmupDuration,
      string dumpSimulationStatusFilename) {
      EventsList = eventsList;
      this.nextEventChooserStrategy = nextEventChooserStrategy;
      this.simulationDuration = simulationDuration;
      WarmupDuration = warmupDuration;
      warmupDurationForReset = warmupDuration;
      Clock = clock;
      executedEvent = new List<Event>();

      try {
        var fs = File.Open(dumpSimulationStatusFilename, FileMode.Open);
        fs.SetLength(0);
        fs.Close();
        streamWriter = new StreamWriter(dumpSimulationStatusFilename);
      }
      catch (Exception) {
        // ignored
      }
    }

    /// <summary>
    ///   Mothod use to simulate. It will reset statistic if the current clock time is bigger than the warmup
    ///   duration. It will only reset statistics once.
    /// </summary>
    public void Simulate() {
      while (Clock.CurrentTime < simulationDuration) {
        if (Clock.CurrentTime > warmupDurationForReset) {
          ResetStat();
          warmupDurationForReset = MaxValue;
        }

        if (EventsList.NumberOfEvents() <= 0) continue;

        var currentEvent = nextEventChooserStrategy.Choose(EventsList, Clock.CurrentTime);

        if (Clock.CurrentTime < currentEvent.PossibleExecutionTimestamp.Time)
          Clock.AddTime(currentEvent.PossibleExecutionTimestamp.Time - Clock.CurrentTime);

        currentEvent.Execute(EventsList, Clock);

        executedEvent.Add(currentEvent);

        DumpSimulationStatus(streamWriter);
      }

      CleanStreamWriter();
    }

    /// <summary>
    ///   This method is use in order to initialise the simulation, add events that must occur and that will
    ///   make the simulation playable for the whole duration
    /// </summary>
    protected abstract void InitializeSimulation();
    
    /// <summary>
    /// Generate a report for the current simulation in a string format
    /// </summary>
    /// <returns>Return the simulation report</returns>
    public abstract string GetSimulationReport();

    protected abstract void ResetStat();

    /// <summary>
    /// When using a GUI when there is a change in the simulation this method is triggered
    /// </summary>
    public virtual void OnChange() {
    }

    /// <summary>
    /// Dump the simulation status in a file
    /// </summary>
    /// <param name="streamWriter">The stream writer to write the simulation status</param>
    protected virtual void DumpSimulationStatus(StreamWriter streamWriter) {
    }

    private void CleanStreamWriter() {
      streamWriter?.Close();
    }


    public Timestamp GetCurrentSimulationTime() {
      return Clock.GetCurrentTimestamp();
    }
  }
}