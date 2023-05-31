using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace software_engineering_project
{
    internal class DateEvent
    {
        public enum EventType
        {
            NO_REPEATE,
            REPEATE_DAILY,
            REPEATE_WEEKLY,
            REPEATE_MONTHLY,
        }

        public DateTime date;
        public EventType eventType;
        public Action action;

        public DateEvent(DateTime date, EventType eventType, Action action)
        {
            this.date = date;
            this.eventType = eventType;
            this.action = action;
        }

        public bool ShouldCallAction()
        {
            DateTime now = DateTime.Now;

            switch (eventType)
            {
                case EventType.NO_REPEATE:
                    return (now == date);
                case EventType.REPEATE_DAILY:
                    return (now.Hour == date.Hour &&
                        now.Minute == date.Minute &&
                        now.Second == date.Second);
                case EventType.REPEATE_WEEKLY:
                    break;
                case EventType.REPEATE_MONTHLY:
                    break;
            }

            return false;
        }
    }

    internal class AutomaticEvent
    {
        // Creates an array of lists of the events
        private static readonly List<DateEvent> events = new List<DateEvent>();
        private static List<int> eventsToRun = new List<int>();

        public static Thread eventCheckingThread;
        public static bool threadRunning = true;

        static AutomaticEvent()
        {
            events = new List<DateEvent>();
        }

        // Add an event to the events list
        public static void AddEvent(DateTime date, DateEvent.EventType eventType, Action action)
        {
            events.Add(new DateEvent(date, eventType, action));
        }

        // Create thread to check for all events, even when "paused" waiting for user input
        public static void StartCheckingEvents()
        {
            eventCheckingThread = new Thread(CheckEvents);
            eventCheckingThread.Name = "CheckEvents";
            eventCheckingThread.Start();
            eventCheckingThread.IsBackground = true;
        }

        public static void TerminateEventChecking()
        {
            //eventCheckingThread.Abort(100);
            //eventCheckingThread.Join();
            threadRunning = false;
        }

        private static void CheckEvents()
        {
            while (threadRunning)
            {
                foreach (DateEvent e in events)
                {
                    if (e.ShouldCallAction())
                    {
                        e.action();
                    }
                }
            }
        }
    }
}
