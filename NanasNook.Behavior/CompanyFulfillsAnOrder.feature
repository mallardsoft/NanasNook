Given a front office machine and a kitchen machine in the same community
Then kitchen sees empty backlog

Given a front office machine and a kitchen machine in the same community
When the front office places an order
Then kitchen sees a backlogged order

Given a front office machine and a kitchen machine in the same community
When the front office places an order
And the kitchen pulls an order
Then the kitchen sees empty backlog