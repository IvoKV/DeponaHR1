using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace DeponaHR1.CustomProcessConfig
{
    class ProcessConfig : ConfigurationSection
    {
        // Create a property that lets us accss the collection
        // of MyConfigTestPrototype

        // Specify the name of the element used for the property
        [ConfigurationProperty("instances")]
        // Specify the type of elements found in the collection
        [ConfigurationCollection(typeof(ProcessInstanceCollection))]
        public ProcessInstanceCollection ProcessInstances
        {
            get
            {
                // Get the collection and parse it
                return (ProcessInstanceCollection)this["instances"];
            }
        }
    }
}
