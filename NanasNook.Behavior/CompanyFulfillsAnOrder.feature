Given a community
And a front office machine 
And a kitchen machine
Then kitchen sees empty backlog

When the front office places an order
Then kitchen sees a backlogged order
And the order is backlogged

When the kitchen pulls an order
And the kitchen commits to an order
Then the kitchen sees empty backlog
And the order is in progress

When the kitchen completes an order
Then the kitchen sees empty backlog
And the order is completed
