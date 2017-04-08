using System;
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics;
using DemoApp.Data.Entities;

namespace DemoApp.Data
{
    public class AppContext : DbContext
    {
        public AppContext() 
            : base("DemoApp") //Name of the DB.
        {
            //Debug - see the output in console of database diagnostics
#if DEBUG
            var listeners = new TraceListener[] { new TextWriterTraceListener(Console.Out) };
            Debug.Listeners.AddRange(listeners);
            this.Database.Log = s => Debug.WriteLine(s);
#endif

            //Set the database initialiser to the AppInitialiser class provided
            Database.SetInitializer<AppContext>(new AppInitialiser());
        }

        /// <summary>
        /// Overloadable context constructor allowing a specific connection
        /// </summary>
        /// <param name="connection"></param>
        public AppContext(DbConnection connection)
            : base(connection, true)
        {
            //Debug - see the output in console of database diagnostics
#if DEBUG
            var listeners = new TraceListener[] { new TextWriterTraceListener(Console.Out) };
            Debug.Listeners.AddRange(listeners);
            this.Database.Log = s => Debug.WriteLine(s);
#endif

            //Set the database initialiser to the AppInitialiser class provided
            Database.SetInitializer<AppContext>(new AppInitialiser());
        }

        public DbSet<Person> People { get; set; }
    }
}
