using System;
using System.Collections.Generic;
using Gherkin.Ast;

namespace SpecFlow.Contrib.JsonData.SpecFlowPlugin.DataSources
{
    public interface ISpecificationProvider
    {
        ExternalDataSpecification GetSpecification(IEnumerable<Tag> tags, string sourceFilePath);
    }
}
