using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using System.Linq;

[Serializable]
public class JSONData {

    public List<Location> locations;

}

[Serializable]
public class Location {

    public Coordinates coordinates;
    public String country;
    public String country_code;
    public String province;
    public int id;
    public Stats latest;
    public Timelines timelines;

}

[Serializable]
public class Coordinates {

    public String latitude;
    public String longitude;

}

[Serializable]
public class Stats {

    public int confirmed;
    public int deaths;
    public int recovered;

}


[Serializable]
public class Timelines {
    public Timeline confirmed;
    public Timeline deaths;
    public Timeline recovered;
}

[Serializable]
public class Timeline {
    
    public int latest;
    public Dictionary<String, int> timeline;

    public List<TimelineEntry> GetHistory() {
        List<TimelineEntry> entries = new List<TimelineEntry>();
        List<string> dateStrings = new List<string>(this.timeline.Keys);
        foreach (string dateString in dateStrings) {
            DateTime dateTime = DateParser.parse(dateString);
            int value = timeline[dateString];
            TimelineEntry entry = new TimelineEntry();
            entry.date = dateTime;
            entry.value = value;
            entries.Add(entry);
        }
        List<TimelineEntry> SortedList = entries.OrderBy(o=>o.date).ToList();
        return entries;
    }

}

public class TimelineEntry {

    public DateTime date;
    public int value;

}

public class DateParser
{
	public static DateTime parse(string date)
	{
		string validformat = "yyyy-MM-ddTHH:mm:ssZ";

		CultureInfo provider = CultureInfo.InvariantCulture;
		
		try {
			DateTime dateTime = DateTime.ParseExact(date, validformat, provider);
			return dateTime;
		}
		catch (FormatException)
		{
			return new DateTime();
		}
	}
}