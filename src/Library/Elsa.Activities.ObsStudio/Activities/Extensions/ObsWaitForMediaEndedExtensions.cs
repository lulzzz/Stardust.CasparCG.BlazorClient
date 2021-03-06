using Elsa.Builders;
using Elsa.Services.Models;
using System;

namespace Elsa.Activities.ObsStudio.Activities.Extensions
{
    public static class ObsWaitForMediaEndedExtensions
    {
        public static ISetupActivity<ObsWaitForMediaEnded> UseSourceName(this ISetupActivity<ObsWaitForMediaEnded> activity, Func<ActivityExecutionContext, string> value) => activity.Set(x => x.SourceName, value);
        public static ISetupActivity<ObsWaitForMediaEnded> UseSourceName(this ISetupActivity<ObsWaitForMediaEnded> activity, Func<string> value) => activity.Set(x => x.SourceName, value);
        public static ISetupActivity<ObsWaitForMediaEnded> UseSourceName(this ISetupActivity<ObsWaitForMediaEnded> activity, string value) => activity.Set(x => x.SourceName, value);

        

    }
   
}
