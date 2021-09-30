using System;

namespace SpecFlow.Contrib.JsonData.SpecFlowPlugin.DataSources.Selectors
{
    public class DataSourceSelectorParser
    {
        public DataSourceSelector Parse(string selectorExpression)
        {
            return new FieldNameSelector(selectorExpression);
        }
    }
}
