using System.Collections.Generic;
using System.Linq;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Mementos;
using System;

/**
/ For use with http://graphviz.org/
digraph "NanasNook.Model"
{
    rankdir=BT
    ProvisionFrontOffice -> Machine
    ProvisionFrontOffice -> Company
    DeprovisionFrontOffice -> ProvisionFrontOffice
    Kitchen -> Company
    ProvisionKitchen -> Machine
    ProvisionKitchen -> Kitchen
    DeprovisionKitchen -> ProvisionKitchen
    Order -> Company [color="red"]
    ContactInformation -> Order
    ContactInformation -> ContactInformation [label="  *"]
    CakeSize -> Company
    CakeFlavor -> Company
    FrostingFlavor -> Company
    FrostingColor -> Company
    CakeDetail -> Order
    CakeDetail -> CakeSize
    CakeDetail -> CakeFlavor
    CakeDetail -> FrostingFlavor
    CakeDetail -> FrostingColor
    CakeDetail -> FrostingColor
    CakeDetail -> CakeDetail [label="  *"]
    City -> Company
    DeliveryDetail -> Order
    DeliveryDetail -> City
    DeliveryDetail -> DeliveryDetail [label="  *"]
    Pull -> Order
    Pull -> Kitchen
    Commitment -> Pull
    Commitment -> CakeDetail [label="  *"]
    Commitment -> Commitment [label="  *"]
    Completed -> Commitment
    Delivered -> Completed
    Delivered -> DeliveryDetail
    Closed -> Delivered
}
**/

namespace NanasNook.Model
{
    [CorrespondenceType]
    public partial class Machine : CorrespondenceFact
    {
        // Roles

        // Queries
        public static Query QueryCurrentProvisionFrontOffice = new Query()
            .JoinSuccessors(ProvisionFrontOffice.RoleMachine, Condition.WhereIsEmpty(ProvisionFrontOffice.QueryIsCurrent)
            )
            ;
        public static Query QueryCurrentProvisionKitchen = new Query()
            .JoinSuccessors(ProvisionKitchen.RoleMachine, Condition.WhereIsEmpty(ProvisionKitchen.QueryIsCurrent)
            )
            ;

        // Predecessors

        // Unique
        [CorrespondenceField]
        private Guid _unique;

        // Fields

        // Results
        private Result<ProvisionFrontOffice> _currentProvisionFrontOffice;
        private Result<ProvisionKitchen> _currentProvisionKitchen;

        // Business constructor
        public Machine(
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
        }

        // Hydration constructor
        private Machine(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _currentProvisionFrontOffice = new Result<ProvisionFrontOffice>(this, QueryCurrentProvisionFrontOffice);
            _currentProvisionKitchen = new Result<ProvisionKitchen>(this, QueryCurrentProvisionKitchen);
        }

        // Predecessor access

        // Field access

        // Query result access
        public IEnumerable<ProvisionFrontOffice> CurrentProvisionFrontOffice
        {
            get { return _currentProvisionFrontOffice; }
        }
        public IEnumerable<ProvisionKitchen> CurrentProvisionKitchen
        {
            get { return _currentProvisionKitchen; }
        }

        // Predicate result access
    }
    
    [CorrespondenceType]
    public partial class Company : CorrespondenceFact
    {
        // Roles

        // Queries
        public static Query QueryBackloggedOrders = new Query()
            .JoinSuccessors(Order.RoleCompany, Condition.WhereIsEmpty(Order.QueryIsBacklogged)
            )
            ;
        public static Query QueryPendingOrders = new Query()
            .JoinSuccessors(Order.RoleCompany, Condition.WhereIsEmpty(Order.QueryIsPending)
            )
            ;
        public static Query QueryCakeSizes = new Query()
            .JoinSuccessors(CakeSize.RoleCompany)
            ;
        public static Query QueryCakeFlavors = new Query()
            .JoinSuccessors(CakeFlavor.RoleCompany)
            ;
        public static Query QueryFrostingFlavors = new Query()
            .JoinSuccessors(FrostingFlavor.RoleCompany)
            ;
        public static Query QueryFrostingColors = new Query()
            .JoinSuccessors(FrostingColor.RoleCompany)
            ;
        public static Query QueryCities = new Query()
            .JoinSuccessors(City.RoleCompany)
            ;

        // Predecessors

        // Fields
        [CorrespondenceField]
        private string _identifier;

        // Results
        private Result<Order> _backloggedOrders;
        private Result<Order> _pendingOrders;
        private Result<CakeSize> _cakeSizes;
        private Result<CakeFlavor> _cakeFlavors;
        private Result<FrostingFlavor> _frostingFlavors;
        private Result<FrostingColor> _frostingColors;
        private Result<City> _cities;

        // Business constructor
        public Company(
            string identifier
            )
        {
            InitializeResults();
            _identifier = identifier;
        }

        // Hydration constructor
        private Company(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _backloggedOrders = new Result<Order>(this, QueryBackloggedOrders);
            _pendingOrders = new Result<Order>(this, QueryPendingOrders);
            _cakeSizes = new Result<CakeSize>(this, QueryCakeSizes);
            _cakeFlavors = new Result<CakeFlavor>(this, QueryCakeFlavors);
            _frostingFlavors = new Result<FrostingFlavor>(this, QueryFrostingFlavors);
            _frostingColors = new Result<FrostingColor>(this, QueryFrostingColors);
            _cities = new Result<City>(this, QueryCities);
        }

        // Predecessor access

        // Field access
        public string Identifier
        {
            get { return _identifier; }
        }

        // Query result access
        public IEnumerable<Order> BackloggedOrders
        {
            get { return _backloggedOrders; }
        }
        public IEnumerable<Order> PendingOrders
        {
            get { return _pendingOrders; }
        }
        public IEnumerable<CakeSize> CakeSizes
        {
            get { return _cakeSizes; }
        }
        public IEnumerable<CakeFlavor> CakeFlavors
        {
            get { return _cakeFlavors; }
        }
        public IEnumerable<FrostingFlavor> FrostingFlavors
        {
            get { return _frostingFlavors; }
        }
        public IEnumerable<FrostingColor> FrostingColors
        {
            get { return _frostingColors; }
        }
        public IEnumerable<City> Cities
        {
            get { return _cities; }
        }

        // Predicate result access
    }
    
    [CorrespondenceType]
    public partial class ProvisionFrontOffice : CorrespondenceFact
    {
        // Roles
        public static Role<Machine> RoleMachine = new Role<Machine>("machine");
        public static Role<Company> RoleCompany = new Role<Company>("company");

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(DeprovisionFrontOffice.RoleProvision)
            ;

        // Predecessors
        private PredecessorObj<Machine> _machine;
        private PredecessorObj<Company> _company;

        // Unique
        [CorrespondenceField]
        private Guid _unique;

        // Fields

        // Results
        private Result<CorrespondenceFact> _isCurrent;

        // Business constructor
        public ProvisionFrontOffice(
            Machine machine
            ,Company company
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _machine = new PredecessorObj<Machine>(this, RoleMachine, machine);
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
        }

        // Hydration constructor
        private ProvisionFrontOffice(FactMemento memento)
        {
            InitializeResults();
            _machine = new PredecessorObj<Machine>(this, RoleMachine, memento);
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        _isCurrent = new Result<CorrespondenceFact>(this, QueryIsCurrent);
        }

        // Predecessor access
        public Machine Machine
        {
            get { return _machine.Fact; }
        }
        public Company Company
        {
            get { return _company.Fact; }
        }

        // Field access

        // Query result access

        // Predicate result access
        public bool IsCurrent
        {
            get { return !_isCurrent.Any(); }
        }
    }
    
    [CorrespondenceType]
    public partial class DeprovisionFrontOffice : CorrespondenceFact
    {
        // Roles
        public static Role<ProvisionFrontOffice> RoleProvision = new Role<ProvisionFrontOffice>("provision");

        // Queries

        // Predecessors
        private PredecessorObj<ProvisionFrontOffice> _provision;

        // Fields

        // Results

        // Business constructor
        public DeprovisionFrontOffice(
            ProvisionFrontOffice provision
            )
        {
            InitializeResults();
            _provision = new PredecessorObj<ProvisionFrontOffice>(this, RoleProvision, provision);
        }

        // Hydration constructor
        private DeprovisionFrontOffice(FactMemento memento)
        {
            InitializeResults();
            _provision = new PredecessorObj<ProvisionFrontOffice>(this, RoleProvision, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ProvisionFrontOffice Provision
        {
            get { return _provision.Fact; }
        }

        // Field access

        // Query result access

        // Predicate result access
    }
    
    [CorrespondenceType]
    public partial class Kitchen : CorrespondenceFact
    {
        // Roles
        public static Role<Company> RoleCompany = new Role<Company>("company");

        // Queries
        public static Query QueryPullsInProgress = new Query()
            .JoinSuccessors(Pull.RoleKitchen, Condition.WhereIsEmpty(Pull.QueryIsCompleted)
            )
            ;
        public static Query QueryPullsCompleted = new Query()
            .JoinSuccessors(Pull.RoleKitchen)
            .JoinSuccessors(Commitment.RolePull)
            .JoinSuccessors(Completed.RoleCommitment, Condition.WhereIsEmpty(Completed.QueryIsDelivered)
            )
            ;

        // Predecessors
        private PredecessorObj<Company> _company;

        // Fields
        [CorrespondenceField]
        private string _name;

        // Results
        private Result<Pull> _pullsInProgress;
        private Result<Completed> _pullsCompleted;

        // Business constructor
        public Kitchen(
            Company company
            ,string name
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
            _name = name;
        }

        // Hydration constructor
        private Kitchen(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _pullsInProgress = new Result<Pull>(this, QueryPullsInProgress);
            _pullsCompleted = new Result<Completed>(this, QueryPullsCompleted);
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
        }

        // Field access
        public string Name
        {
            get { return _name; }
        }

        // Query result access
        public IEnumerable<Pull> PullsInProgress
        {
            get { return _pullsInProgress; }
        }
        public IEnumerable<Completed> PullsCompleted
        {
            get { return _pullsCompleted; }
        }

        // Predicate result access
    }
    
    [CorrespondenceType]
    public partial class ProvisionKitchen : CorrespondenceFact
    {
        // Roles
        public static Role<Machine> RoleMachine = new Role<Machine>("machine");
        public static Role<Kitchen> RoleKitchen = new Role<Kitchen>("kitchen");

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(DeprovisionKitchen.RoleProvision)
            ;

        // Predecessors
        private PredecessorObj<Machine> _machine;
        private PredecessorObj<Kitchen> _kitchen;

        // Unique
        [CorrespondenceField]
        private Guid _unique;

        // Fields

        // Results
        private Result<CorrespondenceFact> _isCurrent;

        // Business constructor
        public ProvisionKitchen(
            Machine machine
            ,Kitchen kitchen
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _machine = new PredecessorObj<Machine>(this, RoleMachine, machine);
            _kitchen = new PredecessorObj<Kitchen>(this, RoleKitchen, kitchen);
        }

        // Hydration constructor
        private ProvisionKitchen(FactMemento memento)
        {
            InitializeResults();
            _machine = new PredecessorObj<Machine>(this, RoleMachine, memento);
            _kitchen = new PredecessorObj<Kitchen>(this, RoleKitchen, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        _isCurrent = new Result<CorrespondenceFact>(this, QueryIsCurrent);
        }

        // Predecessor access
        public Machine Machine
        {
            get { return _machine.Fact; }
        }
        public Kitchen Kitchen
        {
            get { return _kitchen.Fact; }
        }

        // Field access

        // Query result access

        // Predicate result access
        public bool IsCurrent
        {
            get { return !_isCurrent.Any(); }
        }
    }
    
    [CorrespondenceType]
    public partial class DeprovisionKitchen : CorrespondenceFact
    {
        // Roles
        public static Role<ProvisionKitchen> RoleProvision = new Role<ProvisionKitchen>("provision");

        // Queries

        // Predecessors
        private PredecessorObj<ProvisionKitchen> _provision;

        // Fields

        // Results

        // Business constructor
        public DeprovisionKitchen(
            ProvisionKitchen provision
            )
        {
            InitializeResults();
            _provision = new PredecessorObj<ProvisionKitchen>(this, RoleProvision, provision);
        }

        // Hydration constructor
        private DeprovisionKitchen(FactMemento memento)
        {
            InitializeResults();
            _provision = new PredecessorObj<ProvisionKitchen>(this, RoleProvision, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ProvisionKitchen Provision
        {
            get { return _provision.Fact; }
        }

        // Field access

        // Query result access

        // Predicate result access
    }
    
    [CorrespondenceType]
    public partial class Order : CorrespondenceFact
    {
        // Roles
        public static Role<Company> RoleCompany = new Role<Company>("company", RoleRelationship.Pivot);

        // Queries
        public static Query QueryIsBacklogged = new Query()
            .JoinSuccessors(Pull.RoleOrder)
            ;
        public static Query QueryIsInProgress = new Query()
            .JoinSuccessors(Pull.RoleOrder)
            .JoinSuccessors(Commitment.RolePull)
            .JoinSuccessors(Completed.RoleCommitment)
            ;
        public static Query QueryIsCompleted = new Query()
            .JoinSuccessors(Pull.RoleOrder)
            .JoinSuccessors(Commitment.RolePull)
            .JoinSuccessors(Completed.RoleCommitment)
            .JoinSuccessors(Delivered.RoleCompleted)
            ;
        public static Query QueryIsPending = new Query()
            .JoinSuccessors(Pull.RoleOrder)
            .JoinSuccessors(Commitment.RolePull)
            .JoinSuccessors(Completed.RoleCommitment)
            .JoinSuccessors(Delivered.RoleCompleted)
            .JoinSuccessors(Closed.RoleDelivered)
            ;
        public static Query QueryCurrentContactInformationSet = new Query()
            .JoinSuccessors(ContactInformation.RoleOrder, Condition.WhereIsEmpty(ContactInformation.QueryIsCurrent)
            )
            ;
        public static Query QueryCurrentCakeDetails = new Query()
            .JoinSuccessors(CakeDetail.RoleOrder, Condition.WhereIsEmpty(CakeDetail.QueryIsCurrent)
            )
            ;
        public static Query QueryCurrentDeliveryDetails = new Query()
            .JoinSuccessors(DeliveryDetail.RoleOrder, Condition.WhereIsEmpty(DeliveryDetail.QueryIsCurrent)
            )
            ;
        public static Query QueryPulls = new Query()
            .JoinSuccessors(Pull.RoleOrder)
            ;

        // Predecessors
        private PredecessorObj<Company> _company;

        // Unique
        [CorrespondenceField]
        private Guid _unique;

        // Fields

        // Results
        private Result<ContactInformation> _currentContactInformationSet;
        private Result<CakeDetail> _currentCakeDetails;
        private Result<DeliveryDetail> _currentDeliveryDetails;
        private Result<Pull> _pulls;
        private Result<CorrespondenceFact> _isBacklogged;
        private Result<CorrespondenceFact> _isInProgress;
        private Result<CorrespondenceFact> _isCompleted;
        private Result<CorrespondenceFact> _isPending;

        // Business constructor
        public Order(
            Company company
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
        }

        // Hydration constructor
        private Order(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _currentContactInformationSet = new Result<ContactInformation>(this, QueryCurrentContactInformationSet);
            _currentCakeDetails = new Result<CakeDetail>(this, QueryCurrentCakeDetails);
            _currentDeliveryDetails = new Result<DeliveryDetail>(this, QueryCurrentDeliveryDetails);
            _pulls = new Result<Pull>(this, QueryPulls);
        _isBacklogged = new Result<CorrespondenceFact>(this, QueryIsBacklogged);
        _isInProgress = new Result<CorrespondenceFact>(this, QueryIsInProgress);
        _isCompleted = new Result<CorrespondenceFact>(this, QueryIsCompleted);
        _isPending = new Result<CorrespondenceFact>(this, QueryIsPending);
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
        }

        // Field access

        // Query result access
        public IEnumerable<ContactInformation> CurrentContactInformationSet
        {
            get { return _currentContactInformationSet; }
        }
        public IEnumerable<CakeDetail> CurrentCakeDetails
        {
            get { return _currentCakeDetails; }
        }
        public IEnumerable<DeliveryDetail> CurrentDeliveryDetails
        {
            get { return _currentDeliveryDetails; }
        }
        public IEnumerable<Pull> Pulls
        {
            get { return _pulls; }
        }

        // Predicate result access
        public bool IsBacklogged
        {
            get { return !_isBacklogged.Any(); }
        }
        public bool IsInProgress
        {
            get { return !_isInProgress.Any(); }
        }
        public bool IsCompleted
        {
            get { return !_isCompleted.Any(); }
        }
        public bool IsPending
        {
            get { return !_isPending.Any(); }
        }
    }
    
    [CorrespondenceType]
    public partial class ContactInformation : CorrespondenceFact
    {
        // Roles
        public static Role<Order> RoleOrder = new Role<Order>("order");
        public static Role<ContactInformation> RolePrior = new Role<ContactInformation>("prior");

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(ContactInformation.RolePrior)
            ;

        // Predecessors
        private PredecessorObj<Order> _order;
        private PredecessorList<ContactInformation> _prior;

        // Fields
        [CorrespondenceField]
        private string _name;
        [CorrespondenceField]
        private string _phoneNumber;

        // Results
        private Result<CorrespondenceFact> _isCurrent;

        // Business constructor
        public ContactInformation(
            Order order
            ,IEnumerable<ContactInformation> prior
            ,string name
            ,string phoneNumber
            )
        {
            InitializeResults();
            _order = new PredecessorObj<Order>(this, RoleOrder, order);
            _prior = new PredecessorList<ContactInformation>(this, RolePrior, prior);
            _name = name;
            _phoneNumber = phoneNumber;
        }

        // Hydration constructor
        private ContactInformation(FactMemento memento)
        {
            InitializeResults();
            _order = new PredecessorObj<Order>(this, RoleOrder, memento);
            _prior = new PredecessorList<ContactInformation>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        _isCurrent = new Result<CorrespondenceFact>(this, QueryIsCurrent);
        }

        // Predecessor access
        public Order Order
        {
            get { return _order.Fact; }
        }
        public IEnumerable<ContactInformation> Prior
        {
            get { return _prior; }
        }
     
        // Field access
        public string Name
        {
            get { return _name; }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
        }

        // Query result access

        // Predicate result access
        public bool IsCurrent
        {
            get { return !_isCurrent.Any(); }
        }
    }
    
    [CorrespondenceType]
    public partial class CakeSize : CorrespondenceFact
    {
        // Roles
        public static Role<Company> RoleCompany = new Role<Company>("company");

        // Queries

        // Predecessors
        private PredecessorObj<Company> _company;

        // Fields
        [CorrespondenceField]
        private string _name;

        // Results

        // Business constructor
        public CakeSize(
            Company company
            ,string name
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
            _name = name;
        }

        // Hydration constructor
        private CakeSize(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
        }

        // Field access
        public string Name
        {
            get { return _name; }
        }

        // Query result access

        // Predicate result access
    }
    
    [CorrespondenceType]
    public partial class CakeFlavor : CorrespondenceFact
    {
        // Roles
        public static Role<Company> RoleCompany = new Role<Company>("company");

        // Queries

        // Predecessors
        private PredecessorObj<Company> _company;

        // Fields
        [CorrespondenceField]
        private string _name;

        // Results

        // Business constructor
        public CakeFlavor(
            Company company
            ,string name
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
            _name = name;
        }

        // Hydration constructor
        private CakeFlavor(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
        }

        // Field access
        public string Name
        {
            get { return _name; }
        }

        // Query result access

        // Predicate result access
    }
    
    [CorrespondenceType]
    public partial class FrostingFlavor : CorrespondenceFact
    {
        // Roles
        public static Role<Company> RoleCompany = new Role<Company>("company");

        // Queries

        // Predecessors
        private PredecessorObj<Company> _company;

        // Fields
        [CorrespondenceField]
        private string _name;

        // Results

        // Business constructor
        public FrostingFlavor(
            Company company
            ,string name
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
            _name = name;
        }

        // Hydration constructor
        private FrostingFlavor(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
        }

        // Field access
        public string Name
        {
            get { return _name; }
        }

        // Query result access

        // Predicate result access
    }
    
    [CorrespondenceType]
    public partial class FrostingColor : CorrespondenceFact
    {
        // Roles
        public static Role<Company> RoleCompany = new Role<Company>("company");

        // Queries

        // Predecessors
        private PredecessorObj<Company> _company;

        // Fields
        [CorrespondenceField]
        private string _name;

        // Results

        // Business constructor
        public FrostingColor(
            Company company
            ,string name
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
            _name = name;
        }

        // Hydration constructor
        private FrostingColor(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
        }

        // Field access
        public string Name
        {
            get { return _name; }
        }

        // Query result access

        // Predicate result access
    }
    
    [CorrespondenceType]
    public partial class CakeDetail : CorrespondenceFact
    {
        // Roles
        public static Role<Order> RoleOrder = new Role<Order>("order");
        public static Role<CakeSize> RoleSize = new Role<CakeSize>("size");
        public static Role<CakeFlavor> RoleCakeFlavor = new Role<CakeFlavor>("cakeFlavor");
        public static Role<FrostingFlavor> RoleFrostingFlavor = new Role<FrostingFlavor>("frostingFlavor");
        public static Role<FrostingColor> RoleMainColor = new Role<FrostingColor>("mainColor");
        public static Role<FrostingColor> RoleDecorationColor = new Role<FrostingColor>("decorationColor");
        public static Role<CakeDetail> RolePrior = new Role<CakeDetail>("prior");

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(CakeDetail.RolePrior)
            ;

        // Predecessors
        private PredecessorObj<Order> _order;
        private PredecessorObj<CakeSize> _size;
        private PredecessorObj<CakeFlavor> _cakeFlavor;
        private PredecessorObj<FrostingFlavor> _frostingFlavor;
        private PredecessorObj<FrostingColor> _mainColor;
        private PredecessorObj<FrostingColor> _decorationColor;
        private PredecessorList<CakeDetail> _prior;

        // Fields
        [CorrespondenceField]
        private string _message;
        [CorrespondenceField]
        private string _cakeInstructions;

        // Results
        private Result<CorrespondenceFact> _isCurrent;

        // Business constructor
        public CakeDetail(
            Order order
            ,CakeSize size
            ,CakeFlavor cakeFlavor
            ,FrostingFlavor frostingFlavor
            ,FrostingColor mainColor
            ,FrostingColor decorationColor
            ,IEnumerable<CakeDetail> prior
            ,string message
            ,string cakeInstructions
            )
        {
            InitializeResults();
            _order = new PredecessorObj<Order>(this, RoleOrder, order);
            _size = new PredecessorObj<CakeSize>(this, RoleSize, size);
            _cakeFlavor = new PredecessorObj<CakeFlavor>(this, RoleCakeFlavor, cakeFlavor);
            _frostingFlavor = new PredecessorObj<FrostingFlavor>(this, RoleFrostingFlavor, frostingFlavor);
            _mainColor = new PredecessorObj<FrostingColor>(this, RoleMainColor, mainColor);
            _decorationColor = new PredecessorObj<FrostingColor>(this, RoleDecorationColor, decorationColor);
            _prior = new PredecessorList<CakeDetail>(this, RolePrior, prior);
            _message = message;
            _cakeInstructions = cakeInstructions;
        }

        // Hydration constructor
        private CakeDetail(FactMemento memento)
        {
            InitializeResults();
            _order = new PredecessorObj<Order>(this, RoleOrder, memento);
            _size = new PredecessorObj<CakeSize>(this, RoleSize, memento);
            _cakeFlavor = new PredecessorObj<CakeFlavor>(this, RoleCakeFlavor, memento);
            _frostingFlavor = new PredecessorObj<FrostingFlavor>(this, RoleFrostingFlavor, memento);
            _mainColor = new PredecessorObj<FrostingColor>(this, RoleMainColor, memento);
            _decorationColor = new PredecessorObj<FrostingColor>(this, RoleDecorationColor, memento);
            _prior = new PredecessorList<CakeDetail>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        _isCurrent = new Result<CorrespondenceFact>(this, QueryIsCurrent);
        }

        // Predecessor access
        public Order Order
        {
            get { return _order.Fact; }
        }
        public CakeSize Size
        {
            get { return _size.Fact; }
        }
        public CakeFlavor CakeFlavor
        {
            get { return _cakeFlavor.Fact; }
        }
        public FrostingFlavor FrostingFlavor
        {
            get { return _frostingFlavor.Fact; }
        }
        public FrostingColor MainColor
        {
            get { return _mainColor.Fact; }
        }
        public FrostingColor DecorationColor
        {
            get { return _decorationColor.Fact; }
        }
        public IEnumerable<CakeDetail> Prior
        {
            get { return _prior; }
        }
     
        // Field access
        public string Message
        {
            get { return _message; }
        }
        public string CakeInstructions
        {
            get { return _cakeInstructions; }
        }

        // Query result access

        // Predicate result access
        public bool IsCurrent
        {
            get { return !_isCurrent.Any(); }
        }
    }
    
    [CorrespondenceType]
    public partial class City : CorrespondenceFact
    {
        // Roles
        public static Role<Company> RoleCompany = new Role<Company>("company");

        // Queries

        // Predecessors
        private PredecessorObj<Company> _company;

        // Fields
        [CorrespondenceField]
        private string _name;

        // Results

        // Business constructor
        public City(
            Company company
            ,string name
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
            _name = name;
        }

        // Hydration constructor
        private City(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
        }

        // Field access
        public string Name
        {
            get { return _name; }
        }

        // Query result access

        // Predicate result access
    }
    
    [CorrespondenceType]
    public partial class DeliveryDetail : CorrespondenceFact
    {
        // Roles
        public static Role<Order> RoleOrder = new Role<Order>("order");
        public static Role<City> RoleCity = new Role<City>("city");
        public static Role<DeliveryDetail> RolePrior = new Role<DeliveryDetail>("prior");

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(DeliveryDetail.RolePrior)
            ;

        // Predecessors
        private PredecessorObj<Order> _order;
        private PredecessorObj<City> _city;
        private PredecessorList<DeliveryDetail> _prior;

        // Fields
        [CorrespondenceField]
        private string _streetAddress;
        [CorrespondenceField]
        private DateTime _expectedDeliveryDate;
        [CorrespondenceField]
        private string _deliveryInstructions;

        // Results
        private Result<CorrespondenceFact> _isCurrent;

        // Business constructor
        public DeliveryDetail(
            Order order
            ,City city
            ,IEnumerable<DeliveryDetail> prior
            ,string streetAddress
            ,DateTime expectedDeliveryDate
            ,string deliveryInstructions
            )
        {
            InitializeResults();
            _order = new PredecessorObj<Order>(this, RoleOrder, order);
            _city = new PredecessorObj<City>(this, RoleCity, city);
            _prior = new PredecessorList<DeliveryDetail>(this, RolePrior, prior);
            _streetAddress = streetAddress;
            _expectedDeliveryDate = expectedDeliveryDate;
            _deliveryInstructions = deliveryInstructions;
        }

        // Hydration constructor
        private DeliveryDetail(FactMemento memento)
        {
            InitializeResults();
            _order = new PredecessorObj<Order>(this, RoleOrder, memento);
            _city = new PredecessorObj<City>(this, RoleCity, memento);
            _prior = new PredecessorList<DeliveryDetail>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        _isCurrent = new Result<CorrespondenceFact>(this, QueryIsCurrent);
        }

        // Predecessor access
        public Order Order
        {
            get { return _order.Fact; }
        }
        public City City
        {
            get { return _city.Fact; }
        }
        public IEnumerable<DeliveryDetail> Prior
        {
            get { return _prior; }
        }
     
        // Field access
        public string StreetAddress
        {
            get { return _streetAddress; }
        }
        public DateTime ExpectedDeliveryDate
        {
            get { return _expectedDeliveryDate; }
        }
        public string DeliveryInstructions
        {
            get { return _deliveryInstructions; }
        }

        // Query result access

        // Predicate result access
        public bool IsCurrent
        {
            get { return !_isCurrent.Any(); }
        }
    }
    
    [CorrespondenceType]
    public partial class Pull : CorrespondenceFact
    {
        // Roles
        public static Role<Order> RoleOrder = new Role<Order>("order");
        public static Role<Kitchen> RoleKitchen = new Role<Kitchen>("kitchen");

        // Queries
        public static Query QueryIsCompleted = new Query()
            .JoinSuccessors(Commitment.RolePull)
            .JoinSuccessors(Completed.RoleCommitment)
            ;
        public static Query QueryCurrentCommitments = new Query()
            .JoinSuccessors(Commitment.RolePull, Condition.WhereIsEmpty(Commitment.QueryIsCurrent)
            )
            ;

        // Predecessors
        private PredecessorObj<Order> _order;
        private PredecessorObj<Kitchen> _kitchen;

        // Fields

        // Results
        private Result<Commitment> _currentCommitments;
        private Result<CorrespondenceFact> _isCompleted;

        // Business constructor
        public Pull(
            Order order
            ,Kitchen kitchen
            )
        {
            InitializeResults();
            _order = new PredecessorObj<Order>(this, RoleOrder, order);
            _kitchen = new PredecessorObj<Kitchen>(this, RoleKitchen, kitchen);
        }

        // Hydration constructor
        private Pull(FactMemento memento)
        {
            InitializeResults();
            _order = new PredecessorObj<Order>(this, RoleOrder, memento);
            _kitchen = new PredecessorObj<Kitchen>(this, RoleKitchen, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _currentCommitments = new Result<Commitment>(this, QueryCurrentCommitments);
        _isCompleted = new Result<CorrespondenceFact>(this, QueryIsCompleted);
        }

        // Predecessor access
        public Order Order
        {
            get { return _order.Fact; }
        }
        public Kitchen Kitchen
        {
            get { return _kitchen.Fact; }
        }

        // Field access

        // Query result access
        public IEnumerable<Commitment> CurrentCommitments
        {
            get { return _currentCommitments; }
        }

        // Predicate result access
        public bool IsCompleted
        {
            get { return _isCompleted.Any(); }
        }
    }
    
    [CorrespondenceType]
    public partial class Commitment : CorrespondenceFact
    {
        // Roles
        public static Role<Pull> RolePull = new Role<Pull>("pull");
        public static Role<CakeDetail> RoleCakeDetail = new Role<CakeDetail>("cakeDetail");
        public static Role<Commitment> RolePrior = new Role<Commitment>("prior");

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Commitment.RolePrior)
            ;

        // Predecessors
        private PredecessorObj<Pull> _pull;
        private PredecessorList<CakeDetail> _cakeDetail;
        private PredecessorList<Commitment> _prior;

        // Fields

        // Results
        private Result<CorrespondenceFact> _isCurrent;

        // Business constructor
        public Commitment(
            Pull pull
            ,IEnumerable<CakeDetail> cakeDetail
            ,IEnumerable<Commitment> prior
            )
        {
            InitializeResults();
            _pull = new PredecessorObj<Pull>(this, RolePull, pull);
            _cakeDetail = new PredecessorList<CakeDetail>(this, RoleCakeDetail, cakeDetail);
            _prior = new PredecessorList<Commitment>(this, RolePrior, prior);
        }

        // Hydration constructor
        private Commitment(FactMemento memento)
        {
            InitializeResults();
            _pull = new PredecessorObj<Pull>(this, RolePull, memento);
            _cakeDetail = new PredecessorList<CakeDetail>(this, RoleCakeDetail, memento);
            _prior = new PredecessorList<Commitment>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        _isCurrent = new Result<CorrespondenceFact>(this, QueryIsCurrent);
        }

        // Predecessor access
        public Pull Pull
        {
            get { return _pull.Fact; }
        }
        public IEnumerable<CakeDetail> CakeDetail
        {
            get { return _cakeDetail; }
        }
             public IEnumerable<Commitment> Prior
        {
            get { return _prior; }
        }
     
        // Field access

        // Query result access

        // Predicate result access
        public bool IsCurrent
        {
            get { return !_isCurrent.Any(); }
        }
    }
    
    [CorrespondenceType]
    public partial class Completed : CorrespondenceFact
    {
        // Roles
        public static Role<Commitment> RoleCommitment = new Role<Commitment>("commitment");

        // Queries
        public static Query QueryIsDelivered = new Query()
            .JoinSuccessors(Delivered.RoleCompleted)
            ;

        // Predecessors
        private PredecessorObj<Commitment> _commitment;

        // Fields

        // Results
        private Result<CorrespondenceFact> _isDelivered;

        // Business constructor
        public Completed(
            Commitment commitment
            )
        {
            InitializeResults();
            _commitment = new PredecessorObj<Commitment>(this, RoleCommitment, commitment);
        }

        // Hydration constructor
        private Completed(FactMemento memento)
        {
            InitializeResults();
            _commitment = new PredecessorObj<Commitment>(this, RoleCommitment, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        _isDelivered = new Result<CorrespondenceFact>(this, QueryIsDelivered);
        }

        // Predecessor access
        public Commitment Commitment
        {
            get { return _commitment.Fact; }
        }

        // Field access

        // Query result access

        // Predicate result access
        public bool IsDelivered
        {
            get { return _isDelivered.Any(); }
        }
    }
    
    [CorrespondenceType]
    public partial class Delivered : CorrespondenceFact
    {
        // Roles
        public static Role<Completed> RoleCompleted = new Role<Completed>("completed");
        public static Role<DeliveryDetail> RoleDeliveryDetail = new Role<DeliveryDetail>("deliveryDetail");

        // Queries

        // Predecessors
        private PredecessorObj<Completed> _completed;
        private PredecessorObj<DeliveryDetail> _deliveryDetail;

        // Fields

        // Results

        // Business constructor
        public Delivered(
            Completed completed
            ,DeliveryDetail deliveryDetail
            )
        {
            InitializeResults();
            _completed = new PredecessorObj<Completed>(this, RoleCompleted, completed);
            _deliveryDetail = new PredecessorObj<DeliveryDetail>(this, RoleDeliveryDetail, deliveryDetail);
        }

        // Hydration constructor
        private Delivered(FactMemento memento)
        {
            InitializeResults();
            _completed = new PredecessorObj<Completed>(this, RoleCompleted, memento);
            _deliveryDetail = new PredecessorObj<DeliveryDetail>(this, RoleDeliveryDetail, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Completed Completed
        {
            get { return _completed.Fact; }
        }
        public DeliveryDetail DeliveryDetail
        {
            get { return _deliveryDetail.Fact; }
        }

        // Field access

        // Query result access

        // Predicate result access
    }
    
    [CorrespondenceType]
    public partial class Closed : CorrespondenceFact
    {
        // Roles
        public static Role<Delivered> RoleDelivered = new Role<Delivered>("delivered");

        // Queries

        // Predecessors
        private PredecessorObj<Delivered> _delivered;

        // Fields

        // Results

        // Business constructor
        public Closed(
            Delivered delivered
            )
        {
            InitializeResults();
            _delivered = new PredecessorObj<Delivered>(this, RoleDelivered, delivered);
        }

        // Hydration constructor
        private Closed(FactMemento memento)
        {
            InitializeResults();
            _delivered = new PredecessorObj<Delivered>(this, RoleDelivered, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Delivered Delivered
        {
            get { return _delivered.Fact; }
        }

        // Field access

        // Query result access

        // Predicate result access
    }
    
}
