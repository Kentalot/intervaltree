﻿using System;

namespace IntervalTreeLib {
  /// <summary>
  /// The Interval class maintains an interval with some associated data
  /// </summary>
  /// <typeparam name="T">The type of data being stored</typeparam>
  public class Interval<T, D> : IComparable<Interval<T,D>> where D : IComparable<D> {

    private D start;
    private D end;
    private T data;

    public Interval(D start, D end, T data) {
      this.start = start;
      this.end = end;
      this.data = data;
    }

    public D Start {
      get { return start; }
      set { start = value; }
    }

    public D End {
      get { return end; }
      set { end = value; }
    }

    public T Data {
      get { return data; }
      set { data = value; }
    }

    public bool Contains(D time, ContainConstrains constraint) {
      bool isContained;

      switch (constraint) {
        case ContainConstrains.None:
          isContained = Contains(time);
          break;
        case ContainConstrains.IncludeStart:
          isContained = ContainsWithStart(time);
          break;
        case ContainConstrains.IncludeEnd:
          isContained = ContainsWithEnd(time);
          break;
        case ContainConstrains.IncludeStartAndEnd:
          isContained = ContainsWithStartEnd(time);
          break;
        default:
          throw new ArgumentException("Ivnalid constraint " + constraint);
      }

      return isContained;
    }

    /// <summary>
    /// true if this interval contains time (inclusive)
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool Contains(D time) {
      //return time < end && time > start;
      return time.CompareTo(end) < 0 && time.CompareTo(start) > 0;
    }

    /// <summary>
    /// true if this interval contains time (including start).
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool ContainsWithStart(D time) {
      return time.CompareTo(end) < 0 && time.CompareTo(start) >= 0;
    }

    /// <summary>
    /// true if this interval contains time (including end).
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool ContainsWithEnd(D time) {
      return time.CompareTo(end) <= 0 && time.CompareTo(start) > 0;
    }

    /// <summary>
    /// true if this interval contains time (include start and end).
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool ContainsWithStartEnd(D time) {
      return time.CompareTo(end) <= 0 && time.CompareTo(start) >= 0;
    }

    /// <summary>
    /// return true if this interval intersects other
    /// </summary>
    /// <param name="?"></param>
    /// <returns></returns>
    public bool Intersects(Interval<T,D> other) {
      //return other.End > start && other.Start < end;
      return other.End.CompareTo(start) > 0 && other.Start.CompareTo(end) < 0;
    }


    /// <summary>
    /// Return -1 if this interval's start time is less than the other, 1 if greater
    /// In the event of a tie, -1 if this interval's end time is less than the other, 1 if greater, 0 if same
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(Interval<T,D> other) {
      if (start.CompareTo(other.Start) < 0)
        return -1;
      else if (start.CompareTo(other.Start) > 0)
        return 1;
      else if (end.CompareTo(other.End) < 0)
        return -1;
      else if (end.CompareTo(other.End) > 0)
        return 1;
      else
        return 0;
      //if (start < other.Start)
      //  return -1;
      //else if (start > other.Start)
      //  return 1;
      //else if (end < other.End)
      //  return -1;
      //else if (end > other.End)
      //  return 1;
      //else
      //  return 0;
    }

    public override string ToString() {
      return string.Format("{0}-{1}", start, end);
    }
  }

  public enum ContainConstrains {
    None,
    IncludeStart,
    IncludeEnd,
    IncludeStartAndEnd
  }
}
