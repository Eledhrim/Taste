using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Models.ViewModels;
using Taste.Utility;

namespace Taste
{
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManageOrderModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public List<OrderDetailsVM> orderListVM { get; set; }

        public void OnGet()
        {
            orderListVM = new List<OrderDetailsVM>();

            List<OrderHeader> orderHeaderList = _unitOfWork.OrderHeader.GetAll(o => o.Status == SD.StatusSubmitted || o.Status == SD.StatusInProcess).OrderByDescending(u => u.PickUpTime).ToList();

            foreach (OrderHeader item in orderHeaderList)
            {
                OrderDetailsVM individual = new OrderDetailsVM
                {
                    OrderHeader = item,
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(p => p.OrderId == item.Id).ToList()
                };

                orderListVM.Add(individual);
            }

        }

        public IActionResult OnPostOrderPrepare(int orderID)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderID);

            orderHeader.Status = SD.StatusInProcess;

            _unitOfWork.Save();

            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderReady(int orderID)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderID);

            orderHeader.Status = SD.StatusReady;

            _unitOfWork.Save();

            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderCancel(int orderID)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderID);

            orderHeader.Status = SD.StatusCancelled;

            _unitOfWork.Save();

            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderRefund(int orderID)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderID);

            //refund amount
            var options = new RefundCreateOptions
            {
                Amount = Convert.ToInt32(orderHeader.OrderTotal*100),
                Reason = RefundReasons.RequestedByCustomer,
                Charge=orderHeader.TransactionId
            };
            var service = new RefundService();
            Refund refund = service.Create(options);

            orderHeader.Status = SD.StatusRefunded;

            _unitOfWork.Save();

            return RedirectToPage("ManageOrder");
        }
    }
}