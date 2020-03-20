using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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