using System.Collections.Generic;

namespace Manager.Struct.Extensions
{
    public static class RefillerExtensions
    {
        public static string SchTitle()
        {
            var rnd = RandomExtensions.CustomRandom(5);
            var title = new List<string>();

            title.Add("");
            title.Add("INFORMATION SHARING MEETING");
            title.Add("DISCUSS IMPORTANT TOPICS MEETING");
            title.Add("TEAM ALIGNMENT MEETING");
            title.Add("FEEDBACK MEETING");
            title.Add("DECISION MAKING MEETING");
            title.Add("DISCUSS OF THE PROJECT STRUCTURE");

            var rndTitle = title[rnd];

            return rndTitle;
        }

        public static string TkTitle()
        {
            var rnd = RandomExtensions.CustomRandom(10);
            var title = new List<string>();

            title.Add("");
            title.Add("COACHING");
            title.Add("PLANNING");
            title.Add("FORECASTING THE FUTURE");
            title.Add("MOTIVATING EMPLOYEES");
            title.Add("CONTROLLING");
            title.Add("DELEGATION");
            title.Add("PROGRAMMING");
            title.Add("CLEANING");
            title.Add("RUNNING");
            title.Add("DRIVING CAR");
            title.Add("LOOKING");
            title.Add("FUCKING");

            var rndTitle = title[rnd];

            return rndTitle;
        }

        public static string SchDescription()
        {
            var rnd = RandomExtensions.CustomRandom(5);
            var description = new List<string>();

            description.Add("");
            description.Add("Sometimes the only reason for a team to get together is to make decisions, as in “who’s doing what?” or whether it’s time to get new bowling shirts.");
            description.Add("Sometimes it’s useful for team members to get together for no other reason than to give and receive feedback");
            description.Add("Sometimes teams simply need to get together to get on the same page. Or, if not on the same page, then in the same book.");
            description.Add("ome meetings require nothing more than a talking head session — a bunch of people sitting around a table and talking about this, that, and the other thing.");
            description.Add("This is probably the most common kind of meeting — a chance for people to update each other, share research, and reflect on changes impacting whatever projects they are working on together.");
            description.Add("Discuss of the project structure");

            var rndDescription = description[rnd];

            return rndDescription;
        }

        public static string TkDescription()
        {
            var rnd = RandomExtensions.CustomRandom(5);
            var description = new List<string>();

            description.Add("");
            description.Add("One of the most important management tasks is coaching.");
            description.Add("Planning is one of the management functions and one of the most important everyday tasks of the managers.");
            description.Add("Forecasting is another managerial task that will provide a picture of how the future will look like from the business perspective.");
            description.Add("Employees must be motivated if you want to get the best results from their work. You can’t find the person who will work for nothing.");
            description.Add("Controlling is also one of the managerial functions like planning, motivating, organizing, and staffing.");
            description.Add("Discuss of the project structure");

            var rndDescription = description[rnd];

            return rndDescription;
        }

        public static string Location()
        {
            var rnd = RandomExtensions.CustomRandom(10);
            var location = new List<string>();

            location.Add("");
            location.Add("Katowice.");
            location.Add("Berlin");
            location.Add("London");
            location.Add("New York");
            location.Add("Toronto");
            location.Add("Rio");
            location.Add("Warszawa");
            location.Add("Praha");
            location.Add("Paris");
            location.Add("Madrid");
            location.Add("Barcelona");
            location.Add("Gliwice");

            var rndLocation = location[rnd];

            return rndLocation;
        }
    }
}