# Event System with Scriptable Object
Based on [https://unity3d.com/es/how-to/architect-with-scriptable-objects](https://unity3d.com/es/how-to/architect-with-scriptable-objects)

Added support for
- implementing new listeners by inheritance
- subscribe methods to events by delegates
- subscribe or raise events via static methods (uncomment static methods first in GameEvent.cs class) 

To create a new event, ```right click in project folder --> create --> Game Event```.

To reference events statically, place them under ```Resources/Events/```.