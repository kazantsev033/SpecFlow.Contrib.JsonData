using System;
using System.Collections.Generic;

namespace SpecFlow.Contrib.JsonData.SpecFlowPlugin.DataSources
{
    // Not used currently, will be needed for hierarchical data sets.
    public class DataList
    {
        public IList<DataValue> Items { get; } = new List<DataValue>();
    }
}
