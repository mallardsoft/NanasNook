using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using NanasNook.Model;
using UpdateControls.XAML;
using System;

namespace NanasNook.ViewModel.KitchenScreen
{
    public class KitchenViewModel
    {
        private Kitchen _kitchen;
        private KitchenNavigationModel _navigation;

        public KitchenViewModel(Kitchen kitchen, KitchenNavigationModel navigation)
        {
            _kitchen = kitchen;
            _navigation = navigation;
        }

        public IEnumerable<OrderSummaryViewModel> BackloggedOrders
        {
            get
            {
                return _kitchen.Company
                    .BackloggedOrders
                    .Select(o => new OrderSummaryViewModel(o));
            }
        }

        public IEnumerable<PullSummaryViewModel> OrdersInProgress
        {
            get
            {
                return _kitchen
                    .PullsInProgress
                    .Select(p => new PullSummaryViewModel(p));
            }
        }

        public IEnumerable<CompletedSummaryViewModel> OrdersCompleted
        {
            get
            {
                return _kitchen
                    .PullsCompleted
                    .Select(c => new CompletedSummaryViewModel(c));
            }
        }

        public OrderSummaryViewModel SelectedBackloggedOrder
        {
            get
            {
                return _navigation.SelectedBackloggedOrder == null
                    ? null
                    : new OrderSummaryViewModel(_navigation.SelectedBackloggedOrder);
            }
            set
            {
                _navigation.SelectedBackloggedOrder = value == null ? null : value.Order;
            }
        }

        public PullSummaryViewModel SelectedInProgressOrder
        {
            get
            {
                return _navigation.SelectedInProgressPull == null
                    ? null
                    : new PullSummaryViewModel(_navigation.SelectedInProgressPull);
            }
            set
            {
            	_navigation.SelectedInProgressPull = value == null ? null : value.Pull;
            }
        }

        public CompletedSummaryViewModel SelectedCompleteOrder
        {
            get
            {
                return _navigation.SelectedCompletion == null
                    ? null
                    : new CompletedSummaryViewModel(_navigation.SelectedCompletion);
            }
            set
            {
                _navigation.SelectedCompletion = value == null ? null : value.Completed;
            }
        }

        public OrderDetailsViewModel SelectedOrderDetails
        {
            get
            {
                if (_navigation.SelectedBackloggedOrder != null)
                    return new KitchenOrderDetailsViewModel(_navigation.SelectedBackloggedOrder);
                else if (_navigation.SelectedInProgressPull != null)
                    return new KitchenPullDetailsViewModel(_navigation.SelectedInProgressPull);
                else if (_navigation.SelectedCompletion != null)
                    return new KitchenCompletedDetailsViewModel(_navigation.SelectedCompletion);
                else
                    return null;
            }
        }

        public ICommand Pull
        {
            get
            {
                return MakeCommand
                    .When(() => _navigation.SelectedBackloggedOrder != null)
                    .Do(() =>
                    {
                        Pull pull = _kitchen.PullOrder(_navigation.SelectedBackloggedOrder);
                        _navigation.SelectedInProgressPull = pull;
                    });
            }
        }

        public ICommand Complete
        {
            get
            {
                return MakeCommand
                    .When(() => _navigation.SelectedInProgressPull != null)
                    .Do(() =>
                    {
                        Pull pull = _navigation.SelectedInProgressPull;
                        foreach (Commitment commitment in pull.CurrentCommitments)
                        {
                            _navigation.SelectedCompletion = commitment.Complete();
                        }
                    });
            }
        }

        public ICommand Deliver
        {
            get
            {
                return MakeCommand
                    .When(() => _navigation.SelectedCompletion != null)
                    .Do(() =>
                    {
                        Completed completion = _navigation.SelectedCompletion;
                        completion.Deliver();
                        _navigation.SelectedCompletion = null;
                    });
            }
        }
    }
}
