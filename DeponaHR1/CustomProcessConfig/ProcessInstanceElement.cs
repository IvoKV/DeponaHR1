using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace DeponaHR1.CustomProcessConfig
{
    class ProcessInstanceElement : ConfigurationElement
    {
        // Create a property to store the name of the Sage CRM Instance
        // - The "name" is the name of the XML attribute for the property
        // - The IsKey setting specifies that this field is used to identify
        //   element uniquely
        // - The IsRequired setting specifies that a value is required
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                // Return the value of the 'name' attribute as a string
                return (string)base["name"];
            }
            set
            {
                // Set the value of the 'name' attribute
                base["name"] = value;
            }
        }

        // Create a property to store the server of the Sage CRM Instance
        // - The "server" is the name of the XML attribute for the property
        // - The IsRequired setting specifies that a value is required
        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get
            {
                // Return the value of the 'server' attribute as a string
                return (string)base["value"];
            }
            set
            {
                // Set the value of the 'server' attribute
                base["value"] = value;
            }
        }
    }
}
