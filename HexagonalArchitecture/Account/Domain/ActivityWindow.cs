using System.Collections.Generic;
using System.Linq;

namespace HexagonalArchitecture.Account.Domain
{
    public class ActivityWindow
    {
        private readonly List<Activity> activities;

        public ActivityWindow(List<Activity> activities)
        {
            this.activities = activities;
        }

        public ActivityWindow(params Activity[] activities)
        {
            this.activities = activities.ToList();
        }

        public long GetStartTimestamp()
        {
            return activities
                    .Min(activity => activity.GetTimestamp());
        }

        public long GetEndTimestamp()
        {
            return activities
                        .Max(activity => activity.GetTimestamp());
        }

        public Money CalculateBalance(AccountId accountId)
        {
            return Money.Of(0);
        }

        public void AddActivity(Activity activity)
        {
            activities.Add(activity);
        }
    }
}
 