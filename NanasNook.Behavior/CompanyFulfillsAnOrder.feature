Given a community
And a front office machine 
And a kitchen machine
Then kitchen sees empty backlog

Given a community
And a front office machine 
And a kitchen machine
When the front office places an order
Then kitchen sees a backlogged order

Given a community
And a front office machine 
And a kitchen machine
When the front office places an order
And the kitchen pulls an order
Then the kitchen sees empty backlog