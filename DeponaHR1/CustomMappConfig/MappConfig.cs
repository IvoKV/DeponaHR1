using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace DeponaHR1.CustomMappConfig
{
    class MappConfig : ConfigurationSection
    {
        // Create a property that lets us accss the collection
        // of MyConfigTestPrototype

        // Specify the name of the element used for the property
        [ConfigurationProperty("instances")]
        // Specify the type of elements found in the collection
        [ConfigurationCollection(typeof(MappInstanceCollection))]
        public MappInstanceCollection MappInstances
        {
            get
            {
                // Get the collection and parse it
                return (MappInstanceCollection)this["instances"];
            }
        }
    }
}
