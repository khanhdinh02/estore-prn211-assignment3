using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EStore.Controllers
{
    public class OrdersController : Controller
    {
        IOrderRepository orderRepository = new OrderRepository();
        IMemberRepository memberRepository = new MemberRepository();

        // GET: OrdersController
        public ActionResult Index()
        {
            return View(orderRepository.GetAll());
        }

        // GET: OrdersController/Details/5
        public ActionResult Details(int id)
        {
            var order = orderRepository.GetById(id);
            if (order == null)
                return NotFound();
            return View(order);
        }

        // GET: OrdersController/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = getMemberList();
            return View();
        }

        // POST: OrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            try
            {
                orderRepository.Insert(order);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.MemberId = getMemberList();
                ViewBag.Message = ex.Message;
                return View(order);
            }
        }

        // GET: OrdersController/Edit/5
        public ActionResult Edit(int id)
        {
            var order = orderRepository.GetById(id);
            if (order == null)
                return NotFound();

            ViewBag.MemberId = getMemberList();
            return View(order);
        }

        // POST: OrdersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order order)
        {
            try
            {
                order.OrderId = id;
                orderRepository.Update(order);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.MemberId = getMemberList();
                ViewBag.Message = ex.Message;
                return View(order);
            }
        }

        // GET: OrdersController/Delete/5
        public ActionResult Delete(int id)
        {
            var order = orderRepository.GetById(id);
            if (order == null)
                return NotFound();
            return View(order);
        }

        // POST: OrdersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Order order)
        {
            try
            {
                orderRepository.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(order);
            }
        }

        private IEnumerable<SelectListItem> getMemberList()
        {
            return memberRepository.GetAll()
                .Select(m => new SelectListItem(m.Email, m.MemberId.ToString()));
        }
    }
}
