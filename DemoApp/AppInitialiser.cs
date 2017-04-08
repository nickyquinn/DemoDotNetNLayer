using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Data
{
    public class AppInitialiser : CreateDatabaseIfNotExists<AppContext>
    {
        /// <summary>
        /// Seed default data into the app on initial database creation.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(AppContext context)
        {

        }
    }
}
