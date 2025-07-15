namespace Entities.Enums;

/*
it'll use to seed data
At Customer Door → Customer refuses items
With Delivery Company → Delivery person has the items
In Transit → Delivery company bringing items back
Received → Items physically returned to you
Under Inspection → You're checking item condition
Processed → Final outcome (restocked, disposed, etc.)
*/

public enum ReturnStatusEnum
{
    WithDeliveryCompany = 1,
    InTransit,
    Received,
    UnderInspection,
    Restocked,
    Disposed,
    Lost,
}
