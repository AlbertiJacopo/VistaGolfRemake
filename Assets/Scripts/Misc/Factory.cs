using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Factory
{
    public static EventManager CreateEventManager()
    {
        return new EventManager();
    }
}
