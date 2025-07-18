namespace Entities.Enums;

/*
it'll use to seed data
At Customer Door → Customer refuses items
With Delivery Company → Delivery person has the items
In Transit → Delivery company bringing items back
Received → Items physically returned to business.
Under Inspection → Checking item condition.
Restocked → Return item is restocked.
Disposed → Return item is disposed.
Lost → Return item is lost.
*/

public enum enReturnStatus
{
    WithDeliveryCompany = 1,
    InTransit,
    Received,
    UnderInspection,
    Restocked,
    Disposed,
    Lost,
}
